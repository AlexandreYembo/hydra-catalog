using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Hydra.Catalog.PriceUpdate.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hydra.PriceChanged
{
    public static class HttpPriceChanged
    {
       private static List<ProductDetail> _products = new List<ProductDetail> {
            new ProductDetail { Id = 1, OldPrice = 10, NewPrice = 10.2m},
            new ProductDetail { Id = 2, OldPrice = 16, NewPrice = 15.8m},
        };

        [FunctionName("getUpdatePrice")]
        public static async Task<ActionResult> GetUpdatePrice(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
            [SignalR(HubName = "chat")]IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
           // var message = await req.ReadAsStringAsync();

            // if (string.IsNullOrEmpty(message))
            // {
            //     return new BadRequestObjectResult("Please pass a payload to broadcast in the request body.");
            // }

           // var model = JsonConvert.DeserializeObject<Request>(message);
            await signalRMessages.AddAsync(
            new SignalRMessage
            {
                Target = "price_updated",
                Arguments = new[] { JsonConvert.SerializeObject(_products) }
            });

            return new NoContentResult();
        }

        // [FunctionName("SendMessage")]
        // public static async Task<ActionResult> SendMessage(
        //     [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
        //     [SignalR(HubName = "chat")]IAsyncCollector<SignalRMessage> signalRMessages,
        //     ILogger log)
        // {
        //     var message = await req.ReadAsStringAsync();

        //     if (string.IsNullOrEmpty(message))
        //     {
        //         return new BadRequestObjectResult("Please pass a payload to broadcast in the request body.");
        //     }

        //     var model = JsonConvert.DeserializeObject<Request>(message);

        //     model.Products = _products;

        //     await signalRMessages.AddAsync(
        //     new SignalRMessage
        //     {
        //         Target = "price_updated",
        //         Arguments = new[] { JsonConvert.SerializeObject(model) }
        //     });

        //     return new NoContentResult();
        // }
    }
}
