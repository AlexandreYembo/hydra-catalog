using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hydra.Catalog.API.Models;
using Hydra.Core.DomainObjects;
using Hydra.Catalog.Domain.Interfaces;
using Hydra.Catalog.Domain.Interfaces.Services;
using Hydra.Catalog.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hydra.WebAPI.Core.Identity;
using Hydra.WebAPI.Core.Controllers;

namespace Hydra.Catalog.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ProductController : MainController, IDisposable
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper, IStockService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockService = stockService;
        }

        [ClaimsAuthorize("catalog", "read")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ProductDto> GetProductById(Guid id) => 
            _mapper.Map<ProductDto>(await _productRepository.GetProductById(id));

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAllProducts() =>
            _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAllProducts());

        [HttpGet]
        [Route("category")]
        public async Task<IEnumerable<ProductDto>> GetProductByCategory(int code) => 
            _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetProductByCategory(code));

        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProductDto productDto) 
        {
            var product = _mapper.Map<Product>(productDto);
            _productRepository.Insert(product);

            await _productRepository.UnitOfWork.Commit();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ProductDto productDto) 
        {
            var product = _mapper.Map<Product>(productDto);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();

            return Ok();
        }

        [HttpPut]
        [Route("{productId}/{qty}/remove")]
        public async Task<ProductDto> RemoveStock(Guid productId, int quantity) 
        {
            if(!_stockService.RemoveStock(productId, quantity).Result)
                throw new DomainException("Erro to remove this product from stock"); //TODO: Implement this in another way.
           
           return _mapper.Map<ProductDto>(await _productRepository.GetProductById(productId));
        }

        [HttpPut]
        [Route("{productId}/{qty}/add")]
        public async Task<ProductDto> AddStock(Guid productId, int quantity) 
        {
            if(!_stockService.AddStock(productId, quantity).Result)
                throw new DomainException("Erro to add this product to stock"); //TODO: Implement this in another way.
           
           return _mapper.Map<ProductDto>(await _productRepository.GetProductById(productId));
        }

        public void Dispose()
        {
            _productRepository.Dispose();
            _stockService.Dispose();
        }
    }
}
 