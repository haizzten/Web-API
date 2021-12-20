using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System;


namespace f7.Services
{
    public class EmailSender : IEmailSender
    {
        private string _senderName, _senderEmail, _senderPassword;
        public EmailSender(IOptions<EmailSenderOptions> opt)
        {
            EmailSenderOptions options = opt.Value;
            _senderName = options.sender;
            _senderEmail = options.senderEmail;
            _senderPassword = options.password;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage(_senderEmail, email, subject, htmlMessage);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Sender = new MailAddress(_senderEmail);

            using (SmtpClient client = new SmtpClient("smtp.googlemail.com", 587))
            {
                client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                client.EnableSsl = true;
                try
                {
                    await client.SendMailAsync(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.CompletedTask;
                }
            }
        }

    }
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailSenderOptions
    {
        public string sender { get; set; }
        public string senderEmail { get; set; }
        public string password { get; set; }
    }
}
