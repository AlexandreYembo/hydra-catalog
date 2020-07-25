using System.Collections.Generic;
using Hydra.Catalog.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hydra.Catalog.Api.Controllers
{
    [ApiController]
    [Route("catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger){
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CatalogModel> Get() => new List<CatalogModel>{
            new CatalogModel{
                Id = 1, Name="TV", Category = new List<CategoryModel>{
                    new CategoryModel{ Id = 10, Name = "Eletronic"}
                }
            },
            new CatalogModel{
                Id = 2, Name="Computer", Category = new List<CategoryModel>{
                    new CategoryModel{ Id = 10, Name = "Eletronic"},
                    new CategoryModel{ Id = 11, Name = "Technology"}
                }
            }
        };
    }
}