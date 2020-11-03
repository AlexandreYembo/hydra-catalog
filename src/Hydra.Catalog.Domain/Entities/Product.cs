using System;
using Hydra.Core.DomainObjects;
using Hydra.Catalog.Domain.ValueObjects;

namespace Hydra.Catalog.Entities.Models
{
    public class Product : Entity, IAggregateRoot 
    {
        public Guid CategoryId { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string Image { get; private set; }
        public int Qty { get; private set; }

        public Category Category { get; private set; }

        public Dimensions Dimensions { get; private set; }


        protected Product() {  }
        public Product(string name, string description, bool active, decimal price, Guid categoryId, DateTime createdDate, string image, Dimensions dimensions)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            CreatedDate = DateTime.Now;
            Image = image;
            Dimensions = dimensions;

            IsValid();
        }

        /// <summary>
        /// Adhoc setter to enable the product
        /// </summary>
        public void Enable() => Active = true;

        /// <summary>
        /// Adhoc setter to disable the product
        /// </summary>
        public void Disable() => Active = false;

        /// <summary>
        /// Adhoc setter to change the category of product
        /// </summary>
        /// <param name="category"></param>
        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        /// <summary>
        /// Adhoc setter to change the product's description
        /// </summary>
        /// <param name="description"></param>
        public void ChangeDescription(string description) 
        {
            AssertionConcern.ValidateEmpty(description, "The field product description can't be empty");
            Description = description;  
        } 

        public void AddStock(int qty) => Qty += qty;

        public void RemoveStock(int qty)
        {
            if (Qty >= qty)
                Qty -= qty;
        }

        public bool HasStock(int qty) => Qty >= qty;

        public bool IsAvailable(int quantity) => Active && HasStock(quantity);
        public void IsValid()
        {
            AssertionConcern.ValidateEmpty(Name, "The field product name can't be empty");
            AssertionConcern.ValidateEmpty(Description, "The field product description can't be empty");
            AssertionConcern.ValidateEqual(CategoryId, Guid.Empty, "The field category Id can't be empty");
            AssertionConcern.ValidateNotEqOrLessThanMin(Price, 0, "The field price can't be equal or less than 0");
            AssertionConcern.ValidateEmpty(Image, "The field image can't be empty");
        }
    }
}
