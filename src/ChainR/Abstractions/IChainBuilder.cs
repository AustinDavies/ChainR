namespace ChainR.Abstractions
{
    public interface IChainBuilder<TContext> : IChainBuilder
    {
        IChainBuilder<TContext> SetNext(IChainLinkHandler<TContext> handler);

        IChain<TContext> Build();
    }

    public interface IChainBuilder { }
}
