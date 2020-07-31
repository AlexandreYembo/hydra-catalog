using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Catalog.Core.Data;
using Hydra.Catalog.Entities.Models;

namespace Hydra.Catalog.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        Task<IEnumerable<Product>> GetProductByCategory(int code);
        Task<IEnumerable<Category>> GetCategories();

        void Insert(Product product);
        void Update(Product product);
        
        void Insert(Category category);
        void Upddate(Category category);
    }
}
