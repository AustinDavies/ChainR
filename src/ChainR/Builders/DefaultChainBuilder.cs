using ChainR.Abstractions;
using ChainR.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainR.Builders
{
    public sealed class DefaultChainBuilder<TContext> : IChainBuilder<TContext>
    {
        public readonly IEnumerable<IChainLinkHandler<TContext>> _chainLinkHandlers;

        public readonly List<IChainLinkHandler<TContext>> _chain;

        public DefaultChainBuilder(IEnumerable<IChainLinkHandler<TContext>> chainLinkHandlers)
        {
            _chainLinkHandlers = chainLinkHandlers;
            _chain = new List<IChainLinkHandler<TContext>>();
        }

        public IChainBuilder<TContext> SetNext<TChainLinkHandler>()
            where TChainLinkHandler : IChainLinkHandler<TContext>
        {
            var compareType = typeof(TChainLinkHandler);
            var chainLinkHandler = _chainLinkHandlers
                .Where(x => x.GetType() == compareType)
                .FirstOrDefault();

            if (chainLinkHandler is null)
                throw new ArgumentOutOfRangeException(nameof(TChainLinkHandler));

            return SetNext(chainLinkHandler);
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
