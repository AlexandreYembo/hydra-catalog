using System;
using Hydra.Catalog.Core.DomainObjects;

namespace Hydra.Catalog.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
