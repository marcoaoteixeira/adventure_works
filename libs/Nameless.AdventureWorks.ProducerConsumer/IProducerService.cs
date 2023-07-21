namespace Nameless.AdventureWorks.ProducerConsumer {
    /// <summary>
    /// Defines what a producer should do.
    /// </summary>
    public interface IProducerService {
        #region Methods

        /// <summary>
        /// Produces a message for a specific topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The producer arguments.</param>
        void Produce(string topic, object message, ProducerArgs? args = null);

        #endregion Methods
    }
}