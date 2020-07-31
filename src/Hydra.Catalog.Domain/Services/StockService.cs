using System;
using System.Threading.Tasks;
using Hydra.Catalog.Domain.Interfaces;
using Hydra.Catalog.Domain.Interfaces.Services;

namespace Hydra.Catalog.Domain.Services
{
    /// <summary>
    /// ******** DDD Concept *******
    /// Domain service: Is a cross aggregate service, because it will work with 2 or more entities
    /// Or when the business rule does not apply for application layer nor entity.
    /// Should be approved to the Domain Expert - Ubiquitous language
    /// </summary>
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;

        public StockService(IProductRepository productRepository)
        {
            _productRepository = productRepository;    
        }

        public async Task<bool> AddStock(Guid productId, int qty)
        {
            var product = await _productRepository.GetProductById(productId);

            if(product == null) return false;   //You can throw an specific exception here if you prefer.

            product.AddStock(qty);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> RemoveStock(Guid productId, int qty)
        {
            var product = await _productRepository.GetProductById(productId);

            if(product == null) return false;   //You can throw an specific exception here if you prefer.

            if(!product.HasStock(qty)) return false;

            product.RemoveStock(qty);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

         public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
