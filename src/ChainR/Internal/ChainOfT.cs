using ChainR.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChainR.Internal
{
    internal class ChainOfT<TContext> : IChain<TContext>
    {
        private readonly List<IChainLinkHandler<TContext>> _chainLinkHandlers;

        public ChainOfT(List<IChainLinkHandler<TContext>> chainLinkHandlers)
        {
            _chainLinkHandlers = chainLinkHandlers;
        }

        public Task Execute(TContext context, CancellationToken cancellationToken = default)
        {
            return Execute(context, 0, cancellationToken);
        }

        private async Task Execute(TContext context, int index, CancellationToken cancellationToken)
        {
            if (index > _chainLinkHandlers.Count - 1)
                return;

            Task? nextHandlerTask = null;

            await _chainLinkHandlers[index]
                .Handle(context,
                    () => { nextHandlerTask = Execute(context, index + 1, cancellationToken); return nextHandlerTask; },
                    cancellationToken);

            if (!(nextHandlerTask is null) && !nextHandlerTask.IsCompleted)
                await nextHandlerTask;
        }
    }
}
