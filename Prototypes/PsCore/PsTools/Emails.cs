using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace PsTools
{
    public class Emails
    {
        public static void SendHtmlEmail(string receiverEmail, string subject, string body, string adminMail, string AdminPassword, string adminHost, int adminPort, bool ssl = false)
        {
            //Those are read it from webconfig or appconfig
            var client = new SmtpClient(adminHost,
                Convert.ToInt16(adminPort))
            {
                Credentials = new NetworkCredential(adminMail,
                Strings.DecodeKrypt(AdminPassword)),
                EnableSsl = ssl
            };
            //var message = new MailMessage();
            //message.From = new MailAddress(ConfigurationManager.AppSettings["MailSender"]);
            var message = new MailMessage { From = new MailAddress(adminMail) };
            message.To.Add(receiverEmail);
            // message.To.Add("sgermosen@praysoft.net");
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            client.Send(message);
        }

        public static SmtpClient GetMailCredentials(string adminMail, string AdminPassword, string adminHost, int adminPort, bool ssl = false)
        {
            //Those are read it from webconfig or appconfig
            var client = new SmtpClient(adminHost,
                Convert.ToInt16(adminPort))
            {
                Credentials = new NetworkCredential(adminMail,
                   Strings.DecodeKrypt(AdminPassword)),
                EnableSsl = ssl
            };
            // client.Send(message);
            //return Ok();
            return client;
        }

        public static SmtpClient SendSmtpEmail(string receiverEmail, string subject, string body, string adminMail, string AdminPassword, string adminHost, int adminPort, bool ssl = false)
        {
            //Those are read it from webconfig or appconfig
            var client = new SmtpClient(adminHost,
                Convert.ToInt16(adminPort))
            {
                Credentials = new NetworkCredential(adminMail,
                    Strings.DecodeKrypt(AdminPassword)),
                EnableSsl = ssl
            };
            //var message = new MailMessage();
            //message.From = new MailAddress(ConfigurationManager.AppSettings["MailSender"]);
            var message = new MailMessage { From = new MailAddress(adminMail) };
            message.To.Add(receiverEmail);
            // message.To.Add("sgermosen@praysoft.net");
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            // client.Send(message);
            //return Ok();
            return client;
        }

        public static async Task SendMail(string to, string subject, string body, string adminMail, string AdminPassword, string adminHost, int adminPort)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(adminMail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = adminMail,
                    Password = Strings.DecodeKrypt(AdminPassword)
                };

                smtp.Credentials = credential;
                smtp.Host = adminHost;
                smtp.Port = adminPort;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }

        public static async Task SendMail(List<string> mails, string subject, string body, string adminMail, string AdminPassword, string adminHost, int adminPort)
        {
            var message = new MailMessage();

            foreach (var to in mails)
            {
                message.To.Add(new MailAddress(to));
            }

            message.From = new MailAddress(adminMail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = adminMail,
                    Password = Strings.DecodeKrypt(AdminPassword)
                };

                smtp.Credentials = credential;
                smtp.Host = adminHost;
                smtp.Port = adminPort;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }

        //aplications using config
        public static void SendHtmlEmail(string receiverEmail, string subject, string body, bool ssl = false)
    {
        //Those are read it from webconfig or appconfig
        var client = new SmtpClient(ConfigurationManager.AppSettings["MailServer"],
            Convert.ToInt16(ConfigurationManager.AppSettings["MailPort"]))
        {
            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailSender"],
            ConfigurationManager.AppSettings["MailSenderPassword"]),
            EnableSsl = ssl
        };
        //var message = new MailMessage();
        //message.From = new MailAddress(ConfigurationManager.AppSettings["MailSender"]);
        var message = new MailMessage { From = new MailAddress(ConfigurationManager.AppSettings["MailSender"]) };
        message.To.Add(receiverEmail);
        // message.To.Add("sgermosen@praysoft.net");
        message.Subject = subject;
        message.IsBodyHtml = true;
        message.Body = body;
        client.Send(message);
    }

    public static SmtpClient GetMailCredentials(bool ssl = false)
    {
        //Those are read it from webconfig or appconfig
        var client = new SmtpClient(ConfigurationManager.AppSettings["MailServer"],
            Convert.ToInt16(ConfigurationManager.AppSettings["MailPort"]))
        {
            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailSender"],
                ConfigurationManager.AppSettings["MailSenderPassword"]),
            EnableSsl = ssl
        };
        // client.Send(message);
        //return Ok();
        return client;
    }

    public static SmtpClient SendSmtpEmail(string receiverEmail, string subject, string body, bool ssl = false)
    {
        //Those are read it from webconfig or appconfig
        var client = new SmtpClient(ConfigurationManager.AppSettings["MailServer"],
            Convert.ToInt16(ConfigurationManager.AppSettings["MailPort"]))
        {
            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailSender"],
                ConfigurationManager.AppSettings["MailSenderPassword"]),
            EnableSsl = ssl
        };
        //var message = new MailMessage();
        //message.From = new MailAddress(ConfigurationManager.AppSettings["MailSender"]);
        var message = new MailMessage { From = new MailAddress(ConfigurationManager.AppSettings["MailSender"]) };
        message.To.Add(receiverEmail);
        // message.To.Add("sgermosen@praysoft.net");
        message.Subject = subject;
        message.IsBodyHtml = true;
        message.Body = body;

        // client.Send(message);
        //return Ok();
        return client;
    }

    public static async Task SendMail(string to, string subject, string body)
    {
        var message = new MailMessage();
        message.To.Add(new MailAddress(to));
        message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;

        using (var smtp = new SmtpClient())
        {
            var credential = new NetworkCredential
            {
                UserName = WebConfigurationManager.AppSettings["AdminUser"],
                Password = WebConfigurationManager.AppSettings["AdminPassWord"]
            };

            smtp.Credentials = credential;
            smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
            smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(message);
        }
    }

    public static async Task SendMail(List<string> mails, string subject, string body)
    {
        var message = new MailMessage();

        foreach (var to in mails)
        {
            message.To.Add(new MailAddress(to));
        }

        message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;

        using (var smtp = new SmtpClient())
        {
            var credential = new NetworkCredential
            {
                UserName = WebConfigurationManager.AppSettings["AdminUser"],
                Password = WebConfigurationManager.AppSettings["AdminPassWord"]
            };

            smtp.Credentials = credential;
            smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
            smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(message);
        }
    }

}
}
