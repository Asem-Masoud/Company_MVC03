using System.Net;
using System.Net.Mail;

namespace Company_MVC03.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            // Mail Server: Gmail
            // SMtP

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("asemm0000@gmail.com", "kwgrwcukutrbamsv"); //From Google Account Setting -> App Passwords
                client.Send("asemm0000@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
