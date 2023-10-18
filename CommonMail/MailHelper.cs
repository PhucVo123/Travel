using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
namespace CommonMail
{
    public class MailHelper
    {
        public void sendMail(string toEmail, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings
        }
    }
}
