using RabbitMQ.Client;

namespace Nameless.AdventureWorks.ProducerConsumer {
    public interface IChannelManager {
        #region Members

        IModel GetChannel();

        #endregion
    }
}
