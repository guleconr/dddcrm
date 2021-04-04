using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using TBBProject.Core.Common;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TBBProject.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message, bool isHtml = false);
    }

    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _from;

        private IHostingEnvironment _hostingEnvironment;
        public EmailService(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _apiKey = configuration.GetValue<string>("SendGrid:Email:ApiKey");
            _from = configuration.GetValue<string>("SendGrid:Email:From");
            _hostingEnvironment = hostingEnvironment;

        }
        public async Task SendEmailAsync(string email, string subject, string message, bool isHtml = false)
        {
            var webRoot = _hostingEnvironment.WebRootPath;
            var filePath = System.IO.Path.Combine(webRoot, "email/template.html");

            var html = System.IO.File.ReadAllText(filePath);

            html = html.Replace("|BODY|", message);
            html = html.Replace("|TITLE|", subject);

            Ensure.That("email", email).Is.NotNullOrEmpty();
            Ensure.That("subject", subject).Is.NotNullOrEmpty();
            Ensure.That("messages", message).Is.NotNullOrEmpty();

            var sendGridMessage = new SendGridMessage();

            sendGridMessage.From = new EmailAddress(_from);
            sendGridMessage.AddTo(new EmailAddress(email));
            sendGridMessage.Subject = subject;
            sendGridMessage.HtmlContent = isHtml ? html : "";
            sendGridMessage.PlainTextContent = isHtml ? Regex.Replace(message, "<[^>]*>", "") : message;

            var client = new SendGridClient(_apiKey);
            var response = await client.SendEmailAsync(sendGridMessage);
        }
    }
}
