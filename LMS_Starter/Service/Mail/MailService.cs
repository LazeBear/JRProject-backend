using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LMS_Starter.Service
{
    public class MailService:IMailService
    {
        private ILogger<MailService> _LOGGER;
        private string API = "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";
        public MailService(ILogger<MailService> logger)
        {
            _LOGGER = logger;
        }
        public async Task Send(string address, string name, string subject, string content)
        {
            var client = new SendGridClient(API);
            var from = new EmailAddress("SuperDev@maxon.com", "10086 Studio");

            var to = new EmailAddress(address, name);
            var plainTextContent = content;
            var htmlContent = "<strong>"+content+"</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            _LOGGER.LogInformation($"Send email to {address} with response: "+response.StatusCode);
        }

    }
}
