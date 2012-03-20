using System;
using System.Net;
using System.Net.Mail;

namespace RightRecruit.Mvc.Infrastructure.Emailer
{
    public interface IEmailer
    {
        void SendEmail(string toAddresses, string subject, string body, bool isHtml);
    }

    public class Emailer : IEmailer
    {
        private SmtpClient _client;

        public Emailer()
        {
            _client = new SmtpClient("smtp.gmail.com", 587)
                          {
                              Credentials = new NetworkCredential("ganesh.shivshankar@gmail.com", "dealmaker123"),
                              EnableSsl = true
                          };
        }

        public void SendEmail(string toAddresses, string subject, string body, bool isHtml)
        {
            var mailMessage = new MailMessage("", toAddresses, subject, body);
            mailMessage.IsBodyHtml = isHtml;

            _client.Send(mailMessage);
        }
    }
}