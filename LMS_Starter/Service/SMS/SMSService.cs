using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LMS_Starter.Service
{
    public class SMSService : ISMSService
    {
        private string CONSUMER_KEY = "IOrKIiAolznfTA9VkwY5LTSZHH5NKOu5";
        private string CONSUMER_SECRET = "YVjSRW2dEqOrqgw2";
        private static readonly HttpClient client = new HttpClient();
        private static string Token;
        private static DateTime ExpireTime;
        private ILogger<SMSService> _LOGGER;
        private int retry; // retry 3 times
        public SMSService(ILogger<SMSService> logger)
        {
            _LOGGER = logger;
        }

        private async Task GetToken()
        {
            retry = 0;
            var values = new Dictionary<string, string>
                {
                    { "client_id", CONSUMER_KEY },
                    { "client_secret", CONSUMER_SECRET },
                    { "grant_type", "client_credentials"}
                };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://sapi.telstra.com/v1/oauth/token", content);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var obj = JObject.Parse(responseString);
                double time = Convert.ToDouble(obj["expires_in"].ToString());
                _LOGGER.LogCritical(Token);
                _LOGGER.LogCritical("" + time);
                ExpireTime = DateTime.Now.AddSeconds(time);
                Token = obj["access_token"].ToString();
            }
        }

        private async Task GetProvisionNumber()
        {
            string values = "{\"activeDays\":30}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Cache-Control", "no-cache");

            var content = new StringContent(values, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://tapi.telstra.com/v2/messages/provisioning/subscriptions", content);
            _LOGGER.LogInformation("" + response.StatusCode);
        }

        public async Task SendSMS(string number, string body)
        {
            if (DateTime.Compare(ExpireTime, DateTime.Now) < 0)
            {
                _LOGGER.LogCritical(ExpireTime.ToString()+"    :    "+DateTime.Now);
                await GetToken();
                if (Token == null)
                {
                    _LOGGER.LogCritical("Token is null!");
                    return;
                }
            }
            _LOGGER.LogCritical("Preparing message");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            string values = "{\"to\":\""+number+"\",\"body\":\""+body+"\"}";
            var content = new StringContent(values, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://tapi.telstra.com/v2/messages/sms", content);
            if (response.IsSuccessStatusCode)
            {
                _LOGGER.LogInformation($"Send sms to {number}");
            }
            else
            {
                _LOGGER.LogCritical($"Send sms to {number}, but got" + response.StatusCode);
                if (++retry < 2)
                {
                    _LOGGER.LogInformation("Retry "+retry);
                    await GetProvisionNumber();
                    await SendSMS(number, body);
                }
            }
        }
    }
}
