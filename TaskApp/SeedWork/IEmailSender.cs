using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApp.SeedWork
{
    public interface IEmailSender
    {
        void SendEmail(string from, string to, string message, string subject);
    }
}
