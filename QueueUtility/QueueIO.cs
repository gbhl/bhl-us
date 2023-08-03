using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHL.QueueUtility
{
    public class QueueIO : IDisposable
    {
        private bool _disposed = false;

        private string _host = "localhost";
        private int _port = 5672;
        private string _userName = "guest";
        private string _password = "guest";

        private ushort _prefetchCount = 1;

        private IConnection _connection = null;
        private IModel _channel = null;
        private ILogger _logger;

        public string Host { get => _host; set => _host = value; }
        public int Port { get => _port; set => _port = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public ushort PrefetchCount { get => _prefetchCount; set => _prefetchCount = value; }

        public QueueIO(ILogger logger = null)
        {
            _logger = logger;

            InitQueue();
        }

        public QueueIO(string hostname, ILogger logger = null)
        {
            Host = hostname;
            _logger = logger;

            InitQueue();
        }

        public QueueIO(string hostname, int port, ILogger logger = null)
        {
            Host = hostname;
            Port = port;
            _logger = logger;

            InitQueue();
        }

        public QueueIO(string hostName, int port, string userName, string password, ILogger logger = null)
        {
            Host = hostName;
            Port = port;
            UserName = userName;
            Password = password;
            _logger = logger;

            InitQueue();
        }

        ~QueueIO()
        {
            Dispose(false);
        }

        private void InitQueue()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _host,
                Port = _port,
                UserName = _userName,
                Password = _password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(30)
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PutMessage(string message, string queueName, string exchangeName = "",
            string errorQueueName = "", string errorExchangeName = "")
        {
            try
            {
                // Set up the exchange and queues for handling errors
                _channel.QueueDeclare(queue: errorQueueName,
                    durable: true, exclusive: false, autoDelete: false, arguments: null);
                _channel.ExchangeDeclare(errorExchangeName, "fanout", true, false);
                _channel.QueueBind(errorQueueName, errorExchangeName, "");

                // Set up the queue to which to write, including the exchange to which to route errors
                Dictionary<string, object> args = new Dictionary<string, object>()
                {
                    { "x-dead-letter-exchange", errorExchangeName }
                };
                _channel.QueueDeclare(queue: queueName,
                    durable: true, exclusive: false, autoDelete: false, arguments: args);
                _channel.ConfirmSelect();

                var body = Encoding.UTF8.GetBytes(message);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;

                _channel.BasicPublish(exchange: exchangeName,
                                        routingKey: queueName,
                                        basicProperties: properties,
                                        body: body);

                _channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(60));
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error putting message '{0}' onto queue '{1}'",
                    message, queueName), e);
            }
        }

        public void GetMessage(string queueName, string errorExchangeName, string errorQueueName,
            IMessageProcessor processor)
        {
            try
            {
                // Set up the exchange and queues for handling errors
                _channel.QueueDeclare(queue: errorQueueName,
                    durable: true, exclusive: false, autoDelete: false, arguments: null);
                _channel.ExchangeDeclare(errorExchangeName, "fanout", true, false);
                _channel.QueueBind(errorQueueName, errorExchangeName, "");

                // Set up the queue to read, including the exchange to which to route errors
                Dictionary<string, object> args = new Dictionary<string, object>()
                {
                    { "x-dead-letter-exchange", errorExchangeName }
                };
                _channel.QueueDeclare(queue: queueName,
                    durable: true, exclusive: false, autoDelete: false, arguments: args);

                _channel.BasicQos(prefetchSize: 0, prefetchCount: _prefetchCount, global: false);

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    bool processed = false;
                    try
                    {
                        processed = processor.ProcessMessage(message);
                    }
                    catch (Exception ex)
                    {
                        // Log message processing error
                        _logger?.Error(ex, "Error processing the Search Index Queue message '{Message}'", message);
                    }

                    if (processed)
                    {
                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    else
                    {
                        // requeue = false, so rejected messages will be sent to the error queue
                        _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                    }
                };

                _channel.BasicConsume(queue: queueName,
                                        autoAck: false,
                                        consumer: consumer);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error getting message from queue '{0}'", queueName), e);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_channel != null)
                {
                    _channel.Close();
                    _channel.Dispose();
                }
                if (_connection != null)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
