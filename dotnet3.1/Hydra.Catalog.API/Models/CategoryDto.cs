using System;
using System.ComponentModel.DataAnnotations;

namespace Hydra.Catalog.API.Models
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
     
        [Required(ErrorMessage="The field {0} is required")]
        public string Name { get; set; }
       
        [Required(ErrorMessage="The field {0} is required")]
        public int Code{ get;  set; }
    }
}
