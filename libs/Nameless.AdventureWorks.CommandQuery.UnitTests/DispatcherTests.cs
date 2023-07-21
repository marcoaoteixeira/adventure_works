using Autofac;
using Nameless.AdventureWorks.CommandQuery.Fixtures;

namespace Nameless.AdventureWorks.CommandQuery {
    public class DispatcherTests {

        private IContainer _container;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            var builder = new ContainerBuilder();

            builder
                .RegisterModule(new CommandQueryModule(GetType().Assembly));

            _container = builder.Build();
        }

        [Test]
        public void Can_Dispatch_Command() {
            // arrange
            var command = new CreateUserCommand();
            var dispatcher = _container.Resolve<IDispatcher>();

            // act & assert
            Assert.DoesNotThrowAsync(async () => await dispatcher.ExecuteAsync(command));
        }

        [Test]
        public async Task Can_Dispatch_Query() {
            // arrange
            var query = new FindUserByNameQuery {
                Name = "Test1"
            };
            var dispatcher = _container.Resolve<IDispatcher>();

            // act
            var user = await dispatcher.ExecuteAsync(query);

            // assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Name, Is.EqualTo(query.Name));
        }
    }
}
