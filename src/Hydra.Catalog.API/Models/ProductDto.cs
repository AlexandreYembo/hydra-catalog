using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hydra.Catalog.API.Models
{
    public class ProductDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage="The field {0} is required")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage="The field {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage="The field {0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage="The field {0} is required")]
        public bool Active { get; set; }

        [Required(ErrorMessage="The field {0} is required")]
        public decimal Price { get; set; }
       
        [Required(ErrorMessage="The field {0} is required")]
        public DateTime CreatedDate { get; set; }
       
        [Required(ErrorMessage="The field {0} is required")]
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The value for the field {0} has to be greater than {1}")]
        [Required(ErrorMessage="The field {0} is required")]
        public int Qty { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The value for the field {0} has to be greater than {1}")]
        [Required(ErrorMessage="The field {0} is required")]
        public decimal Height { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The value for the field {0} has to be greater than {1}")]
        [Required(ErrorMessage="The field {0} is required")]
        public decimal Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The value for the field {0} has to be greater than {1}")]
        [Required(ErrorMessage="The field {0} is required")]
        public decimal Length { get; set; }

        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
