namespace Nameless.AdventureWorks.CommandQuery.Fixtures {
    public class CreateUserCommand : ICommand {
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand> {
        public Task HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default) {
            return Task.CompletedTask;
        }
    }
}
