using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TBBProject.Core.Services
{
    public interface ISmsService
    {
        Task SendSmsAsync(string number, string message);
    }

    public class SmsSender : ISmsService
    {
        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
