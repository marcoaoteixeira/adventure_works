namespace Nameless.AdventureWorks.CommandQuery {
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> {
        #region Methods

        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);

        #endregion
    }
}
