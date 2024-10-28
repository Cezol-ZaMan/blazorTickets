using blazorTickets.Data;
using blazorTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace blazorTickets.Services
{
    public class TicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSender _emailSender;
        private readonly TechnicianEmailSettings _technicianEmailSettings;

        public TicketService(ApplicationDbContext context, EmailSender emailSender, IOptions<TechnicianEmailSettings> technicianSettings)
        {
            _context = context;
            _emailSender = emailSender;
            _technicianEmailSettings = technicianSettings.Value;
        }

        public async Task<List<Ticket>> GetUserOpenTicketsAsync(ApplicationUser user)
        {
            return await _context.Tickets
                .Where(t => !t.IsClosed && t.CreatedById == user.Id)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetUserClosedTicketsAsync(ApplicationUser user)
        {
            return await _context.Tickets
                .Where(t => t.IsClosed && t.CreatedById == user.Id) 
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetOpenTicketsAsync()
        {
            return await _context.Tickets
                .Where(t => !t.IsClosed)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetClosedTicketsAsync()
        {
            return await _context.Tickets
                .Where(t => t.IsClosed)
                .ToListAsync();
        }


        public async Task AddTicket(Ticket ticket, ApplicationUser? appUser, ApplicationDbContext ctx, IOptions<TechnicianEmailSettings> technicianEmails, EmailSender emailSender)
        {
            if (appUser == null)
            {
                return;
            }

            ticket.IsClosed = false;
            ticket.CreatedById = appUser.Id;
            ticket.CreatedByName = appUser.UserName;
            ticket.CreatedAt = DateTime.Now;

           
            await ctx.AddAsync(ticket);
            await ctx.SaveChangesAsync();
          
            var message = $"<p>Ticket <b>{ticket.Id}</b> was added</p>" +
                          $"<p><b>Title:</b><br><i>{ticket.Title}</i></p>" +
                          $"<p><b>Description:</b><br/><i>{ticket.Description}</i></p>" +
                          $"<p><b>Severity:</b><br/><i>{ticket.Severity}</i>";
         
            foreach (var technician in technicianEmails.Value.Emails)
            {
                await _emailSender.SendEmailAsync(technician, $"New ticket from {appUser.UserName}", message);
            }
            
        }
    }
}
