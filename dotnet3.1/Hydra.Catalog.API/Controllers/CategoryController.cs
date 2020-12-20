using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hydra.Catalog.API.Models;
using Hydra.Catalog.Domain.Interfaces;
using Hydra.Catalog.Entities.Models;
using Hydra.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Catalog.API.Controllers
{
    [Route("[controller]")]
    public class CategoryController : MainController
    {
         private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CategoryDto categoryDto) 
        {
            var category = _mapper.Map<Category>(categoryDto);
            _productRepository.Insert(category);

            await _productRepository.UnitOfWork.Commit();

            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetCategories() => 
            _mapper.Map<IEnumerable<CategoryDto>>(await _productRepository.GetCategories());

        [HttpGet]
        [Route("{id}")]
        public async Task<CategoryDto> GetCategory(Guid id) => 
            _mapper.Map<CategoryDto>(await _productRepository.GetCategoryById(id));
    }
}
