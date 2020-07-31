using System;
using System.Threading.Tasks;

namespace Hydra.Catalog.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
