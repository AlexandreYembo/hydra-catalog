using System;
using Hydra.Catalog.Core.Messages;

namespace Hydra.Catalog.Core.DomainObjects
{
    /// <summary>
    /// Super class of Domain Event
    /// </summary>
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
