using Hydra.Catalog.Api.Hubs;
using Hydra.Catalog.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Hydra.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController : ControllerBase
    {
        private IHubContext<PriceHub> _hub;

        public PriceController(IHubContext<PriceHub> hub){
            _hub = hub;
        }

        public IActionResult Get(){
            _hub.Clients.All.SendAsync("transferchartdata", new CatalogModel(){ Id = 123, Name="Catalog from SignalR"});
            return Ok(new { Message = "Request Completed" });
        }
    }
}