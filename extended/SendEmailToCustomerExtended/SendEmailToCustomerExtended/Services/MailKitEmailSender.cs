using MailKit.Net.Smtp;
using MimeKit;
using SendEmailToCustomerExtended.Interfaces;

namespace SendEmailToCustomerExtended.Services
{
    public class MailKitEmailSender : IEmailSender
    {
        public async Task SendAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CRM Admin", "qminh.nov@gmail.com"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("qminh.nov@gmail.com", "itbe nwtp ltas rcin");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}