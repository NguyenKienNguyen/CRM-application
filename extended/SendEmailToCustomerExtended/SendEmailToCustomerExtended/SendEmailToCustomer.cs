using System.Text.Json;
using SendEmailToCustomerExtended.Interfaces;
using SendEmailToCustomerExtended.Services;

namespace SendEmailToCustomerExtended
{
    public class SendEmailToCustomer
    {
        private readonly IEmailSender _emailSender;

        public SendEmailToCustomer()
        {
            _emailSender = new MailKitEmailSender();
        }

        public void After(string parameter)
        {
            try
            {
                using var doc = JsonDocument.Parse(parameter);
                var root = doc.RootElement;

                foreach (var customer in root.EnumerateArray())
                {
                    string? name = customer.GetProperty("Name").GetString();
                    string? email = customer.GetProperty("Email").GetString();

                    if (string.IsNullOrWhiteSpace(email))
                        continue;

                    string subject = "Your customer record has been updated";
                    string body = $"Hello {name}, your customer record has been changed.";

                    _emailSender.SendAsync(email, subject, body).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email plugin error: {ex.Message}");
            }
        }
    }
}