using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hydra.Catalog.PriceUpdate.Models
{
    public class Request
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("products")]
        public List<ProductDetail> Products { get; set; }
    }
}