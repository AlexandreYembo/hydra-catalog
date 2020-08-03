using AutoMapper;
using Hydra.Catalog.API.Models;
using Hydra.Catalog.Entities.Models;

namespace Hydra.Catalog.API.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            //This map create the Domain object passing parameters only via constructor (attribute sets is protected), this mapping configuration is different
            CreateMap<ProductDto, Product>()
                .ConstructUsing(p => 
                    new Product(p.Name, p.Description, p.Active, p.Price, p.CategoryId, p.CreatedDate, p.Image, 
                    new Domain.ValueObjects.Dimensions(p.Height, p.Width, p.Length)));


            CreateMap<CategoryDto, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Code));
        }
    }
}  