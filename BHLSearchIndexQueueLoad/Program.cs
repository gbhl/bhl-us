using BHL.QueueUtility;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace BHL.SearchIndexQueueLoad
{
    class Program
    {
        private static string _configFile = "AppConfig.xml";

        private static string _startDate = string.Empty;
        private static string _endDate = string.Empty;

        private static string _mqAddress = string.Empty;
        private static int _mqPort = 0;
        private static string _mqUser = string.Empty;
        private static string _mqPw = string.Empty;
        private static string _mqQueue = string.Empty;
        private static string _mqErrorExchange = string.Empty;
        private static string _mqErrorQueue = string.Empty;
        private static string _mqQueueNames = string.Empty;
        private static string _mqErrorExchangeNames = string.Empty;
        private static string _mqErrorQueueNames = string.Empty;
        private static string _mqQueuePdf = string.Empty;
        private static string _mqErrorExchangePdf = string.Empty;
        private static string _mqErrorQueuePdf = string.Empty;

        private static string _smtpHost = string.Empty;
        private static int _smtpPort= 0;
        private static bool _smtpEnableSsl = false;
        private static bool _smtpDefaultCredentials = false;
        private static string _smtpUser = string.Empty;
        private static string _smtpPw = string.Empty;
        private static string _emailFromAddress = string.Empty;
        private static string _emailToAddresses = string.Empty;

        private static string _connectionKey = string.Empty;

        static void Main(string[] args)
        {
            int numQueued = 0;
            bool isError = false;

            if (ReadCommandLineArguments(args))
            {
                try
                {
                    ReadConfig();

                    string logPath = Path.GetDirectoryName(new Uri(Assembly.GetEntryAssembly().Location).LocalPath) +
                        "/logs/BHLSearchIndexQueueLoad-" + _connectionKey + "-{Date}.log";

                    Log.Logger = new LoggerConfiguration()
                        .WriteTo.RollingFile(logPath, shared: true)
                        .CreateLogger();
                    Log.Information("Queuing Started");


                    DataAccess dataAccess = new DataAccess(new ConfigurationManager(_configFile).ConnectionStrings(_connectionKey));

                    // Get changes from DB
                    DbChangeSet changeSet = dataAccess.SelectChangeList(_startDate, _endDate);

                    if (changeSet.Changes.Count > 0)
                    {
                        Log.Information("Retrieved {NumMessages} messages to be queued", changeSet.Changes.Count);
                        int lastAuditID = int.MinValue;
                        DateTime lastAuditDate = DateTime.MinValue;

                        using (QueueIO queueUtil = new QueueIO(_mqAddress, _mqPort, _mqUser, _mqPw))
                        {
                            // Add a message to the queue for each index change
                            foreach (DbChange change in changeSet.Changes)
                            {
                                string queueMsg = string.Format("{0}|{1}|{2}",
                                    change.Operation, change.IndexEntity, change.Id);

                                try
                                {
                                    if (change.Queue.ToLower() == "search")
                                    {
                                        switch (change.IndexEntity)
                                        {
                                            case "nameresolved":
                                                // Name messages belong on a queue separate from the other messages
                                                queueUtil.PutMessage(queueMsg,
                                                    queueName: _mqQueueNames,
                                                    errorQueueName: _mqErrorQueueNames,
                                                    errorExchangeName: _mqErrorExchangeNames);
                                                break;
                                            case "item":
                                            case "segment":
                                            case "author":
                                            case "keyword":
                                            default:
                                                queueUtil.PutMessage(queueMsg,
                                                    queueName: _mqQueue,
                                                    errorQueueName: _mqErrorQueue,
                                                    errorExchangeName: _mqErrorExchange);
                                                break;
                                        }
                                    }
                                    if (change.Queue.ToLower() == "pdf")
                                    {
                                        queueUtil.PutMessage(queueMsg,
                                            queueName: _mqQueuePdf,
                                            errorQueueName: _mqErrorQueuePdf,
                                            errorExchangeName: _mqErrorExchangePdf);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string errMsg = string.Format(
                                        "Error adding a message to the search index queue: {0}", queueMsg);
                                    Log.Error(ex, errMsg);
                                    isError = true;
                                    break;
                                }

                                lastAuditID = change.AuditId;
                                lastAuditDate = change.AuditDate;
                                numQueued++;
                            }
                        }

                        if (!isError)
                        {
                            lastAuditID = changeSet.LastAuditBasicID;
                            lastAuditDate = changeSet.LastAuditDate;
                        }

                        if (lastAuditID != int.MinValue)
                        {
                            // Log progress
                            dataAccess.InsertSearchIndexQueueLog(lastAuditID, lastAuditDate, numQueued);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error executing BHLSearchIndexQueueLoad");
                    isError = true;
                }

                Log.Information("{NumQueued} messages queued successfully", numQueued);
                Log.Information("Queuing Complete");

                if (isError)
                {
                    // Send email notification of errors
                    SendEmailErrorNotification();
                }
            }
        }

        /// <summary>
        /// Send an email with the specified message
        /// </summary>
        /// <param name="message"></param>
        private static void SendEmailErrorNotification()
        {
            try
            {
                string thisComputer = Environment.MachineName;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("", _emailFromAddress));
                string[] toAddresses = _emailToAddresses.Split(',');
                foreach (string toAddress in toAddresses)
                {
                    mimeMessage.To.Add(new MailboxAddress("", toAddress));
                }
                mimeMessage.Subject = "BHLSearchIndexQueueLoad: Index Message Queueing on " + thisComputer + " completed with errors";
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = "An error occurred while adding messages to the index queue.  See the BHLSearchIndexQueueLoad logs for detailed information."
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(_smtpHost, _smtpPort, _smtpEnableSsl);
                    if (!string.IsNullOrWhiteSpace(_smtpUser)) client.Authenticate(_smtpUser, _smtpPw);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Could not send Email");
            }
        }

        /// <summary>
        /// Read the config file name from the command line
        /// </summary>
        /// <returns></returns>
        private static bool ReadCommandLineArguments(string[] args)
        {
            /*
             The name of the config file should be the only argument.  If no file name
             is specified, AppConfig.xml is used.
             */
            if (args.Length > 0) _configFile = args[0];

            if (_configFile.ToLower() == "-h" || _configFile.ToLower() == "--help")
            {
                Console.WriteLine("Syntax:  BHLSearchIndexQueueLoad [<CONFIG FILE NAME>]");
                return false;
            }

            if (!File.Exists(_configFile))
            {
                Console.WriteLine(string.Format("Could not read config file {0}", _configFile));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Read the command line arguments
        /// </summary>
        /// <returns></returns>
        private static void ReadConfig()
        {
            _startDate = new ConfigurationManager(_configFile).AppSettings("StartDate");
            _endDate = new ConfigurationManager(_configFile).AppSettings("EndDate");

            _mqAddress = new ConfigurationManager(_configFile).AppSettings("MQAddress");
            _mqPort = Convert.ToInt32(new ConfigurationManager(_configFile).AppSettings("MQPort"));
            _mqUser = new ConfigurationManager(_configFile).AppSettings("MQUser");
            _mqPw = new ConfigurationManager(_configFile).AppSettings("MQPassword");
            _mqQueue = new ConfigurationManager(_configFile).AppSettings("MQQueue");
            _mqErrorExchange = new ConfigurationManager(_configFile).AppSettings("MQErrorExchange");
            _mqErrorQueue = new ConfigurationManager(_configFile).AppSettings("MQErrorQueue");
            _mqQueueNames = new ConfigurationManager(_configFile).AppSettings("MQQueueNames");
            _mqErrorExchangeNames = new ConfigurationManager(_configFile).AppSettings("MQErrorExchangeNames");
            _mqErrorQueueNames = new ConfigurationManager(_configFile).AppSettings("MQErrorQueueNames");
            _mqQueuePdf = new ConfigurationManager(_configFile).AppSettings("MQQueuePDF");
            _mqErrorExchangePdf = new ConfigurationManager(_configFile).AppSettings("MQErrorExchangePDF");
            _mqErrorQueuePdf = new ConfigurationManager(_configFile).AppSettings("MQErrorQueuePDF");

            _smtpHost = new ConfigurationManager(_configFile).AppSettings("SmtpHost");
            _smtpPort = Convert.ToInt32(new ConfigurationManager(_configFile).AppSettings("SmtpPort"));
            _smtpEnableSsl = new ConfigurationManager(_configFile).AppSettings("SmtpEnableSsl") == "true";
            _smtpDefaultCredentials = new ConfigurationManager(_configFile).AppSettings("SmtpDefaultCredentials") == "true";
            _smtpUser = new ConfigurationManager(_configFile).AppSettings("SmtpUsername");
            _smtpPw = new ConfigurationManager(_configFile).AppSettings("SmtpPassword");
            _emailFromAddress = new ConfigurationManager(_configFile).AppSettings("EmailFromAddress");
            _emailToAddresses = new ConfigurationManager(_configFile).AppSettings("EmailToAddresses");

            _connectionKey = new ConfigurationManager(_configFile).AppSettings("ConnectionKey");
        }
    }
}
