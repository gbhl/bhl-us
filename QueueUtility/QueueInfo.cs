using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHL.QueueUtility
{
    public class QueueInfo : IDisposable
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

        public QueueInfo(string hostName = null, int? port = null, string username = null, string password = null)
        {
            if (hostName != null) Host = hostName;
            if (port != null) Port = (int)port;
            if (username != null) UserName = username;
            if (password != null) Password = password;
            InitQueue();
        }

        ~QueueInfo()
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

        /// <summary>
        /// Return the number of messages on the specified queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public uint GetMessageCount(string queueName)
        {
            return _channel.MessageCount(queueName);
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
