using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
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

        private IConnection _connection = null;
        private IModel _channel = null;

        public string Host { get => _host; set => _host = value; }
        public int Port { get => _port; set => _port = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }

        public QueueIO()
        {
            InitQueue();
        }

        public QueueIO(string hostname)
        {
            Host = hostname;
            InitQueue();
        }

        public QueueIO(string hostname, int port)
        {
            Host = hostname;
            Port = port;
            InitQueue();
        }

        public QueueIO(string hostName, int port, string userName, string password)
        {
            Host = hostName;
            Port = port;
            UserName = userName;
            Password = password;

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

        public void PutMessage(string message, string queueName, string exchangeName = "")
        {
            try
            {
                _channel.QueueDeclare(queue: queueName,
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
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

        public void GetMessage(string queueName, IMessageProcessor processor)
        {
            try
            {
                _channel.QueueDeclare(queue: queueName,
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    if (processor.ProcessMessage(message))
                    {
                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    else
                    {
                        _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
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
