namespace Nameless.AdventureWorks.CommandQuery.Fixtures {
    public class FindUserByNameQuery : IQuery<User> {
        public string? Name { get; set; }
    }

    public class FindUserByNameQueryHandler : IQueryHandler<FindUserByNameQuery, User> {
        public Task<User> HandleAsync(FindUserByNameQuery query, CancellationToken cancellationToken = default) {
            return Task.FromResult(new User("1", query.Name));
        }
    }
}
