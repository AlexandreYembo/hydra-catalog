using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Hydra.Catalog.Api.Hubs
{
    public class PriceHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await.Clients.user("userId").SendAsync("priceUpdated", user, message); //Send to especific user
         //   await Clients.All.SendAsync("priceUpdated", user, message);
        }
    }
}