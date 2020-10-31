using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Core.Data;
using Hydra.Catalog.Entities.Models;

namespace Hydra.Catalog.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        Task<IEnumerable<Product>> GetProductByCategory(int code);
        Task<IEnumerable<Category>> GetCategories();

        Task<Category> GetCategoryById(Guid id);

        void Insert(Product product);
        void Update(Product product);
        
        void Insert(Category category);
        void Upddate(Category category);
        Task<List<Product>> GetProductsById(string ids);
    }
}
