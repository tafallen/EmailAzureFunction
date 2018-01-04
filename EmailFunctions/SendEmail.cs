using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Collections.Generic;

namespace EmailFunctions
{
    public static class SendEmail
    {
        [FunctionName("SendEmail")]
        public static async Task<object> Run([HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"Webhook was triggered!");

            string jsonContent = await req.Content.ReadAsStringAsync();
            var message = JsonConvert.DeserializeObject<EmailMessage>(jsonContent);

            var apiKey = System.Environment.GetEnvironmentVariable("ApiKey", System.EnvironmentVariableTarget.Process);
            var client = new SendGridClient(apiKey);
            var msg = BuildEmailMessage(message);

            var response = client.SendEmailAsync(msg).Result;

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return req.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private static SendGridMessage BuildEmailMessage(EmailMessage message)
        {
            var mimeType = message.IsHtml ? MimeType.Html : MimeType.Text;
            var receipients = new List<EmailAddress>();

            foreach (var toAddress in message.ToEmailAddresses)
            {
                receipients.Add(new EmailAddress(toAddress));
            }

            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress(message.FromEmailAddress, message.FromName));
            msg.AddTos(receipients);
            msg.SetSubject(message.Subject);
            msg.AddContent(mimeType, message.Body);

            return msg;
        }
    }
}
