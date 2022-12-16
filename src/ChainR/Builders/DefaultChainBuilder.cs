using ChainR.Abstractions;
using ChainR.Internal;
using System.Collections.Generic;

namespace ChainR.Builders
{
    public sealed class DefaultChainBuilder<TContext> : IChainBuilder<TContext>
    {
        public readonly List<IChainLinkHandler<TContext>> _chain;

        public DefaultChainBuilder()
        {
            _chain = new List<IChainLinkHandler<TContext>>();
        }

        public IChainBuilder<TContext> SetNext(IChainLinkHandler<TContext> handler)
        {
            if (handler is null)
                return this;

            _chain.Add(handler);

            return this;
        }

        public IChain<TContext> Build()
        {
            return new ChainOfT<TContext>(_chain);
        }
    }
}
