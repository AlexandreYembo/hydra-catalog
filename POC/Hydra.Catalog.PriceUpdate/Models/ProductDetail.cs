namespace Hydra.Catalog.PriceUpdate.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
    }
}