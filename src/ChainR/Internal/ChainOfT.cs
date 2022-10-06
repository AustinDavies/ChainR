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

        private Task Execute(TContext context, int index, CancellationToken cancellationToken)
        {
            if (index > _chainLinkHandlers.Count - 1)
                return Task.CompletedTask;

            IChainLinkHandler<TContext> nextHandler;
            if (_chainLinkHandlers.Count < index + 1)
                nextHandler = _chainLinkHandlers[index + 1];

            return _chainLinkHandlers[index]
                .Handle(context, 
                    () => { return Execute(context, index + 1, cancellationToken); },
                    cancellationToken);
        }
    }
}
