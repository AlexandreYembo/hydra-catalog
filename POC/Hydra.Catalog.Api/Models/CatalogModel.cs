using System.Collections.Generic;

namespace Hydra.Catalog.Api.Models
{
    public class CatalogModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryModel> Category {get;set;}
    }
}