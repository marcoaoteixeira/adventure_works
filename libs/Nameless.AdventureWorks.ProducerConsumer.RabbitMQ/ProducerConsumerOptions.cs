﻿using System.ComponentModel;

namespace Nameless.AdventureWorks.ProducerConsumer {
    public sealed class ProducerConsumerOptions {

        #region Private Fields

        private IList<ExchangeOptions>? _exchanges;

        #endregion

        #region Public Static Read-Only Properties

        public static ProducerConsumerOptions Default => new();

        #endregion

        #region Public Properties

        public ServerOptions Server { get; set; } = new();
        public ConnectionOptions Connection { get; set; } = new();
        public IList<ExchangeOptions> Exchanges {
            get { return _exchanges ??= new List<ExchangeOptions>(); }
            set { _exchanges = value; }
        }

        #endregion
    }

    public sealed class ConnectionOptions {
        #region Public Properties

        /// <summary>
        /// Gets or sets the maximum number of connection attempts. If -1 is specified, that means INFINITE attempts.
        /// Default is 5.
        /// </summary>
        public int MaxConnectionAttempts { get; set; } = 5;

        #endregion
    }

    public sealed class ServerOptions {

        #region Public Properties

        public bool UseSsl { get; set; } = false;
        public string? ServerName { get; set; }
        public string? CertificatePath { get; set; }
        public string Hostname { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string? Username { get; set; } = "guest";
        public string? Password { get; set; } = "guest";

        #endregion

        #region Public Override Methods

        public override string ToString() {
            return string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password)
                ? $"{(UseSsl ? "amqps" : "amqp")}://{Hostname}:{Port}/"
                : $"{(UseSsl ? "amqps" : "amqp")}://{Username}:{Password}@{Hostname}:{Port}/";
        }

        #endregion
    }

    public enum ExchangeType {
        [Description("direct")]
        Direct = 0,
        [Description("topic")]
        Topic = 1,
        [Description("queue")]
        Queue = 2,
        [Description("fanout")]
        Fanout = 4,
        [Description("headers")]
        Headers = 8
    }

    public sealed class ExchangeOptions {

        #region Private Fields

        private IDictionary<string, object>? _arguments;
        private IList<QueueOptions>? _queues;

        #endregion

        #region Public Properties

        public string Name { get; set; }
        public ExchangeType Type { get; set; }
        public bool Durable { get; set; } = true;
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments {
            get { return _arguments ??= new Dictionary<string, object>(); }
            set { _arguments = value; }
        }
        public IList<QueueOptions> Queues {
            get { return _queues ??= new List<QueueOptions>(); }
            set { _queues = value; }
        }

        #endregion

        #region Public Constructors

        public ExchangeOptions() {
            Name = Constants.DEFAULT_EXCHANGE_NAME;
        }

        #endregion
    }

    public sealed class QueueOptions {

        #region Private Fields

        private IDictionary<string, object>? _arguments;
        private IDictionary<string, object>? _bindings;

        #endregion

        #region Public Properties

        public string? Name { get; set; }
        public bool Durable { get; set; } = true;
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public string? RoutingKey { get; set; }
        public IDictionary<string, object> Arguments {
            get { return _arguments ??= new Dictionary<string, object>(); }
            set { _arguments = value; }
        }
        public IDictionary<string, object> Bindings {
            get { return _bindings ??= new Dictionary<string, object>(); }
            set { _bindings = value; }
        }

        #endregion

        #region Public Constructors

        public QueueOptions() {
            Name = Constants.DEFAULT_QUEUE_NAME;
        }

        #endregion
    }
}
