using BHL.QueueUtility;
using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using Serilog;
using System;
using System.Collections.Generic;
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
        private static string _mqExchange = string.Empty;
        private static string _mqErrorExchange = string.Empty;
        private static string _mqErrorQueue = string.Empty;
        private static string _mqQueueNames = string.Empty;
        private static string _mqErrorExchangeNames = string.Empty;
        private static string _mqErrorQueueNames = string.Empty;
        private static string _mqQueuePdf = string.Empty;
        private static string _mqErrorExchangePdf = string.Empty;
        private static string _mqErrorQueuePdf = string.Empty;

        private static string _emailFromAddress = string.Empty;
        private static string _emailToAddresses = string.Empty;
        private static bool _emailOnError = true;

        private static string _bhlwsurl = string.Empty;

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
                                                    exchangeName: _mqExchange,
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
                                    if (change.Queue.ToLower() == "doi")
                                    {
                                        dataAccess.InsertDOIQueue(
                                            dOIEntityTypeID: DBLookups.DOIEntityTypeID[change.IndexEntity.ToLower()], 
                                            entityID: Convert.ToInt32(change.Id), 
                                            creationUserID: 1, 
                                            lastModifiedUserID: 1);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string errMsg = string.Format(
                                        "Error adding a message to the {0} queue: {1}", change.Queue, queueMsg);
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

                string message;
                string serviceName = "BHLSearchIndexQueueLoad";
                if (isError)
                {
                    message = "An error occurred while adding messages to the index queue. See the BHLSearchIndexQueueLoad logs for detailed information.";
                    // Send email notification of errors
                    SendServiceLog(serviceName, message, true);
                    SendEmailErrorNotification(serviceName, message);                    
                }
                else if (numQueued > 0)
                {
                    message = string.Format("{0} messages queued successfully", numQueued);
                    SendServiceLog(serviceName, message, false);
                }
            }
        }

        /// <summary>
        /// Send an email with the specified message
        /// </summary>
        /// <param name="message"></param>
        private static void SendEmailErrorNotification(string serviceName, string message)
        {
            try
            {
                if (_emailOnError)
                {
                    MailRequestModel mailRequest = new MailRequestModel();
                    mailRequest.Subject = serviceName + ": Process on " + Environment.MachineName + " completed with errors"; ;
                    mailRequest.Body = message;
                    mailRequest.From = _emailFromAddress;

                    List<string> recipients = new List<string>();
                    foreach (string recipient in _emailToAddresses.Split(',')) recipients.Add(recipient);
                    mailRequest.To = recipients;

                    EmailClient restClient = new EmailClient(_bhlwsurl);
                    restClient.SendEmail(mailRequest);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Could not send Email");
            }
        }

        /// <summary>
        /// Send the specified message to the log table in the database
        /// </summary>
        /// <param name="serviceName">Name of the service being logged</param>
        /// <param name="message">Body of the message to be sent</param>
        private static void SendServiceLog(string serviceName, string message, bool isError)
        {
            try
            {
                ServiceLogModel serviceLog = new ServiceLogModel();
                serviceLog.Servicename = serviceName;
                serviceLog.Logdate = DateTime.Now;
                serviceLog.Severityname = isError ? "Error" : "Information";
                serviceLog.Message = string.Format("Processing on {0} completed.\n\r{1}", Environment.MachineName, message);

                ServiceLogsClient restClient = new ServiceLogsClient(_bhlwsurl);
                restClient.InsertServiceLog(serviceLog);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Service Log Exception: ");
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
            _mqExchange = new ConfigurationManager(_configFile).AppSettings("MQExchange");
            _mqErrorExchange = new ConfigurationManager(_configFile).AppSettings("MQErrorExchange");
            _mqErrorQueue = new ConfigurationManager(_configFile).AppSettings("MQErrorQueue");
            _mqQueueNames = new ConfigurationManager(_configFile).AppSettings("MQQueueNames");
            _mqErrorExchangeNames = new ConfigurationManager(_configFile).AppSettings("MQErrorExchangeNames");
            _mqErrorQueueNames = new ConfigurationManager(_configFile).AppSettings("MQErrorQueueNames");
            _mqQueuePdf = new ConfigurationManager(_configFile).AppSettings("MQQueuePDF");
            _mqErrorExchangePdf = new ConfigurationManager(_configFile).AppSettings("MQErrorExchangePDF");
            _mqErrorQueuePdf = new ConfigurationManager(_configFile).AppSettings("MQErrorQueuePDF");

            _emailFromAddress = new ConfigurationManager(_configFile).AppSettings("EmailFromAddress");
            _emailToAddresses = new ConfigurationManager(_configFile).AppSettings("EmailToAddresses");
            _emailOnError = new ConfigurationManager(_configFile).AppSettings("EmailOnError").ToLower() == "true";

            _bhlwsurl = new ConfigurationManager(_configFile).AppSettings("BHLWSUrl");

            _connectionKey = new ConfigurationManager(_configFile).AppSettings("ConnectionKey");
        }
    }
}
