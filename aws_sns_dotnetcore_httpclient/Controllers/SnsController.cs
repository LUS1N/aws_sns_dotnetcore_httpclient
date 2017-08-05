using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using aws_sns_dotnetcore_httpclient.Models;
using Microsoft.AspNetCore.Antiforgery.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace aws_sns_dotnetcore_httpclient.Controllers
{
    [Route("/[controller]")]
    public class SnsController : Controller
    {
        // POST /[controller]
        [HttpPost]
        public string Post([FromBody] dynamic body)
        {
            switch (Request.Headers["x-amz-sns-message-type"])
            {
                case "SubscriptionConfirmation":
                    Console.WriteLine("Confirming subscription.");
                    string url = body["SubscribeURL"];
                    return $"{ConfirmSubscription(url)}";
                case "Notification":
                    return JsonConvert.SerializeObject(ExtractMessage<Person>(body));
                default:
                    return "Unsupported message type.";
            }
        }

        private static T ExtractMessage<T>(dynamic body)
        {
            // Optional parameter that could've been specified when posting a message to SNS 
            var subject = (string)body["Subject"];
            Console.WriteLine(subject);

            var itemString = (string) body["Message"];
            var item = JsonConvert.DeserializeObject<T>(itemString);
            return item;
        }

        private static int ConfirmSubscription(string subscriptionUrl)
        {
            // Make a GET request to subscription URL and wait for the result
            var resp = ((HttpWebRequest)WebRequest.Create(subscriptionUrl)).GetResponseAsync().Result;

            // return the length of the data at the URL to indicate that the request completed
            return (int)resp.ContentLength;
        }
    }
}
