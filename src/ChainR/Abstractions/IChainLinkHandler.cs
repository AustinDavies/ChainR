using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChainR.Abstractions
{
    public interface IChainLinkHandler<TContext>
    {
        Task Handle(TContext context, Func<Task> next, CancellationToken cancellationToken = default);
    }
}
