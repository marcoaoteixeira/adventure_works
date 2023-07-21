namespace Nameless.AdventureWorks.CommandQuery {
    public interface ICommandHandler<in TCommand> where TCommand : ICommand {
        #region Methods

        Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);

        #endregion
    }
}
