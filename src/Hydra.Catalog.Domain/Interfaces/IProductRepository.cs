using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Core.Data;
using Hydra.Catalog.Entities.Models;

namespace Hydra.Catalog.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResult<Product>> GetAllProducts(int pageSize, int pageIndex, string query = null);
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
