using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public interface IEmailService
    {
        void SendEmail(string receiver, string subject, string body);
        public void SendFileEmail(string receiver, string subject, string body, byte[] bytes,string fileName);

    }
}
