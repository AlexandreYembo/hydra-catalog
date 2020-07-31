using System;
using Hydra.Catalog.Core.DomainObjects;

namespace Hydra.Catalog.Domain.ValueObjects
{
    /// <summary>
    /// This class is an object Value
    /// This class is immutable.static It means it will never have method that change the status. The object binds on the constructor, and only have
    /// methods that returns the object formated.
    /// </summary>
    public class Dimensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Length { get; private set; }

        public Dimensions(decimal height, decimal width, decimal length)
        {
             Height = height;
             Width = width;
             Length = length;

             IsValid();
        }

        public void IsValid()
        {
            AssertionConcern.ValidateNotEqOrLessThanMin(Height, 0, "The field height can't be equal or less than 0");
            AssertionConcern.ValidateNotEqOrLessThanMin(Width, 0, "The field width can't be equal or less than 0");
            AssertionConcern.ValidateNotEqOrLessThanMin(Length, 0, "The field length can't be equal or less than 0");
        }

        public string Description() => $"{Width} x {Height} x {Length}";

        public override string ToString() => Description();
    }
}
