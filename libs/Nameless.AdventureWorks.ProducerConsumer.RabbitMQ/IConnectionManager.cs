using RabbitMQ.Client;

namespace Nameless.AdventureWorks.ProducerConsumer {
    public interface IConnectionManager {
        #region Methods

        IConnection GetConnection();

        #endregion
    }
}
