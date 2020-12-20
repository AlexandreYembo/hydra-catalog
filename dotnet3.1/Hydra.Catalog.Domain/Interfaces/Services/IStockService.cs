using System;
using System.Threading.Tasks;

namespace Hydra.Catalog.Domain.Interfaces.Services
{
    public interface IStockService : IDisposable
    {
        Task<bool> AddStock(Guid productId, int qty);
        Task<bool> RemoveStock(Guid productId, int qty);
    }
}
