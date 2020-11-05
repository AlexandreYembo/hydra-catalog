using System;
using AutoMapper;
using Hydra.Catalog.API.Models;
using Hydra.Catalog.Domain;
using Hydra.Catalog.Entities.Models;

namespace Hydra.Catalog.API.AutoMapper
{
    /// <summary>
    /// This class is a profile use to the Domain model to DTO
    /// </summary>
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Length, o => o.MapFrom(s => s.Dimensions.Length)) //belong to the child model (Dimensions)
                .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimensions.Height))
                .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimensions.Width));

            CreateMap<Category, CategoryDto>();
        }
    }
}
