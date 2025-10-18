using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace Company_MVC03.PL.Helpers
{
    public class MailServices(IOptions<MailSettings> _options) : IMailServices
    {
        /*
        private readonly MailSettings _options;

        public MailServices(IOptions<MailSettings> options)
        {
            _options = options.Value;
        }
        */

        public void SendEmail(Email email)
        {
            // Build Message
            var mail = new MimeMessage();

            mail.Subject = email.Subject;
            mail.From.Add(new MailboxAddress(_options.Value.DisplayName, _options.Value.Email));
            mail.To.Add(MailboxAddress.Parse(email.To));

            var buldier = new BodyBuilder();
            buldier.TextBody = email.Body;
            mail.Body = buldier.ToMessageBody();

            // Established Connection
            using var smpt = new SmtpClient();
            smpt.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smpt.Authenticate(_options.Value.Email, _options.Value.Password);

            // Send Message
            smpt.Send(mail);

        }
    }
}