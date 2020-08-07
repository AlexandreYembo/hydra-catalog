using System;
using Hydra.Core.Messages.CommonMessages.DomainEvents;

namespace Hydra.Catalog.Domain.Events
{
    //Domain event --> references from Hydra.Core -> CommonMessages -> DomainEvent.cs
    public class ProductLowStockEvent : DomainEvent
    {
        public int QtyInStock { get; set; }
        public ProductLowStockEvent(Guid aggregateId, int qtyInStock) : base(aggregateId)
        {
            QtyInStock = qtyInStock;
        }
    }
}
