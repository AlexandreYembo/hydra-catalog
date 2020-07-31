using System;
using System.Collections.Generic;
using Hydra.Catalog.Core.DomainObjects;

namespace Hydra.Catalog.Entities.Models
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code{ get; private set; }
        
        // EF Relation
        public ICollection<Product> Products { get; set; }

        //EF has a problem  to populate a class that does not have an opened constructor (without parameters)
        protected Category() { }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;

            IsValid();
        }

        public override string ToString() => $"{Name} - {Code}";

        public void IsValid()
        {
            AssertionConcern.ValidateEmpty(Name, "The field category name can't be empty");
            AssertionConcern.ValidateEqual(Code, 0, "The field code can't be 0");
        }
    }
}
