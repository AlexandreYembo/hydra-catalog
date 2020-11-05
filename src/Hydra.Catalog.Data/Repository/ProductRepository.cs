using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hydra.Core.Data;
using Hydra.Catalog.Domain.Interfaces;
using Hydra.Catalog.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Hydra.Catalog.Domain;
using Dapper;

namespace Hydra.Catalog.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context){
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
           _context?.Dispose();
        }

        //AsNoTracking -> use to less consume of EF resource

        public async Task<PagedResult<Product>> GetAllProducts(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT * FROM Products 
                        WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')
                        ORDER BY [Name]
                        OFFSET {pageSize * (pageIndex -1)} ROWS
                        FETCH NEXT {pageSize} ROWS ONLY
                        SELECT COUNT(Id) FROM Products
                        WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')";
            
            var multi = await _context.Database.GetDbConnection()
                                               .QueryMultipleAsync(sql, new { Name = query});
            var products = multi.Read<Product>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Product>()
            {
                List = products,
                TotalResult = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(int code)
        {
            return await _context.Products.AsNoTracking()
                                          .Include( p => p.Category)
                                          .Where(c => c.Category.Code == code)
                                          .ToListAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            //return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            var product = await _context.Products.FindAsync(id);
              if(product != null)
                await _context.Entry(product)
                              .Reference(c => c.Category)
                              .LoadAsync();

            return product;
        }

        public async Task<List<Product>> GetProductsById(string ids)
        {
            var idsGuid = ids.Split(',')
                            .Select(id => (IsValid: Guid.TryParse(id, out var x), Value: x));

            if (!idsGuid.All(nid => nid.IsValid)) return new List<Product>();

            var idsValue = idsGuid.Select(id => id.Value);

            return await _context.Products.AsNoTracking()
                            .Where(p => idsValue.Contains(p.Id) && p.Active)
                            .ToListAsync();
        }

        public void Insert(Product product)
        {
            _context.Products.Add(product);
        }

        public void Insert(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Upddate(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
