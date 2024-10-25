using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Models;

namespace Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _settings;

        public EmailSender(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value; 
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_settings.DisplayName, _settings.Username));
            mimeMessage.To.Add(new MailboxAddress("", email));
            mimeMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = message };
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_settings.Host, _settings.Port, _settings.EnableSsl);
                await client.AuthenticateAsync(_settings.Username, _settings.Password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine("Error connecting to socket: ", e.Message);
            }
        }
    }
}
