using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hydra.Catalog.API.Models;
using Hydra.Catalog.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Catalog.API.Controllers
{
    public class CategoryController : ControllerBase
    {
         private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetCategories() => 
            _mapper.Map<IEnumerable<CategoryDto>>(await _productRepository.GetCategories());
    }
}
