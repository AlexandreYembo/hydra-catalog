using System;
using Hydra.Catalog.Core.DomainObjects;

namespace Hydra.Catalog.Domain.Events
{
    //Domain event --> references from Hydra.Core -> Domain objects -> DomainEvent.cs
    public class ProductLowStockEvent : DomainEvent
    {
        public int QtyInStock { get; set; }
        public ProductLowStockEvent(Guid aggregateId, int qtyInStock) : base(aggregateId)
        {
            QtyInStock = qtyInStock;
        }
    }
}
