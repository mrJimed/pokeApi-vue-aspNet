using MailKit.Net.Smtp;
using MimeKit;
using PokeApi.Server.Models;
using PokeApi.Server.Services.Interfaces;

namespace PokeApi.Server.Services.Classes
{
    public class MailService(string fromEmail, string password, string host, int port) : IMailService
    {
        private readonly string fromEmail = fromEmail;
        private readonly string password = password;
        private readonly string host = host;
        private readonly int port = port;

        public async Task SendMessageAsync(Message message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("PokeAPI", fromEmail));
            emailMessage.To.Add(new MailboxAddress("", message.ToEmail));
            emailMessage.Subject = message.SubjectMessage;
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
