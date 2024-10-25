using blazorTickets.Data;
using blazorTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace blazorTickets.Services
{
    public class TicketService
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
