using System.Threading.Tasks;
using Hydra.Catalog.Core.Messages;

namespace Hydra.Catalog.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T tEvent) where T : Event;
    }
}
