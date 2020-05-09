using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Shop.Web.Models;

namespace Shop.Web.Services
{
    public class EmailSender: IEmailSender
    {
        public EmailSender(IOptions<EmailSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailSettings Options { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(subject, message, email);
        }

        public async Task Execute(string subject, string message, string email)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email)
                                 ? Options.ToEmail
                                 : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(Options.UsernameEmail, "Shop Admin")
                };
                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = "Shop - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(Options.SecondayDomain, Options.SecondaryPort))
                {
                    smtp.Credentials = new NetworkCredential(Options.UsernameEmail, Options.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                //do something here
            }
        }
    }
}
