
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Util
{
    public class Mail
    {
        public static string AdminEmail = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];

        public static void SendMail(string subject, string mailTo, string content)
        {
            SendMail(subject, mailTo, string.Empty, content);
        }

        public static void SendMail(string subject, string mailTo, string cc, string content)
        {
            if (!string.IsNullOrEmpty(mailTo))
            {
                using (WCFProxy<IMail> proxy = new WCFProxy<IMail>(""))
                {
                    MailContent mail = new MailContent();

                    mail.IsHTML = true;
                    mail.Subject = string.Format("[{0}]", subject);
                    //mail.To = mailTo;
                    mail.To = "LiuChao@mail.maipu.com";
                    mail.Cc = cc;
                    mail.Body = content;

                    proxy.Operations.SendMail(mail);
                }
            }
        }
    }
}
