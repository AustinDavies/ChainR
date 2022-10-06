using System.Threading;
using System.Threading.Tasks;

namespace ChainR.Abstractions
{
    public interface IChain<TContext>
    {
        Task Execute(TContext context, CancellationToken cancellationToken = default);
    }
}
