using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Hydra.Catalog.PriceUpdate
{
    public class NegotiateFunc
    {
         [FunctionName ("negotiate")]
        public static SignalRConnectionInfo Negotiate (
            [HttpTrigger (AuthorizationLevel.Anonymous)] HttpRequest req, [SignalRConnectionInfo (HubName = "chat")] SignalRConnectionInfo connectionInfo) {
            return connectionInfo;
        }
    }
}