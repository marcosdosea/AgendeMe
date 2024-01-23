using System.Net.Mail;
using System.Net;
using Email.Models;

namespace Email
{
    public class EmailService
    {
        public static async Task<bool> Enviar(EmailModel mailModel)
        {
            SMTPModel smtpEmail = SMTPModel.Instance;

            if (mailModel.To.Count == 0)
            {
                return false;
            }
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(mailModel.From, mailModel.Assunto);
            foreach (var t in mailModel.To)
            {
                mail.To.Add(t);
            }

            mail.Subject = mailModel.Subject;

            //centro do html do email
            string bodyCenter = "<br><br><div style=\"padding-top: 20px;padding-bottom: 20px;background-color: #198754;\">" +
                                    "<h2 style=\"color: white;text-align: center;\">" + mailModel.Assunto + "</h2>" +
                                "</div>" +
                                "<div style=\"color: black; text-align: left;\">" +
                                    "<h3 style=\"text-indent: 10px;\">Olá, "/*+ mailModel.AddresseeName*/ + "</h3>" +
                                    "<p style=\"text-indent: 10px; font-size:12pt;\">" +
                                        mailModel.Body +
                                    "</p>" +
                                "</div><br>" +
                                "<br><br>";

            //mail.Body = mailModel.Body;
            mail.Body = bodyCenter;

            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new())
            {
                smtp.Host = mailModel.Host;
                smtp.Credentials = new NetworkCredential(smtpEmail.UserName, smtpEmail.Password);
                smtp.EnableSsl = true;
                smtp.Port = smtpEmail.Port;


                await smtp.SendMailAsync(mail);

                return true;
            }
        }
    }
}