using MimeKit;
using MailKit.Net.Smtp;
using PokeApiV2.Server.Models;
using PokeApiV2.Server.Services.Interfaces;

namespace PokeApiV2.Server.Services.Classes
{
    public class MailService(string fromEmail, string password, string host,int port) : IMailService
    {
        public async Task SendEmailAsync(Message message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("PokeAPI", fromEmail));
            emailMessage.To.Add(new MailboxAddress("", message.ToEmail));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart("Paint")
            {
                Text = message.Text
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(host, port, true);
            await client.AuthenticateAsync(fromEmail, password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
