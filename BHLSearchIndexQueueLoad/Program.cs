using BHL.QueueUtility;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;
using System;

namespace BHL.SearchIndexQueueLoad
{
    class Program
    {
        private static string _startDate = string.Empty;
        private static string _endDate = string.Empty;

        private static string _mqAddress = string.Empty;
        private static int _mqPort = 0;
        private static string _mqUser = string.Empty;
        private static string _mqPw = string.Empty;
        private static string _mqQueueName = string.Empty;

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

            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile("logs/BHLSearchIndexer-{Date}.log", shared: true)
                .CreateLogger();
            Log.Information("Queuing Started");

            try
            {
                ReadConfig();

                DataAccess dataAccess = new DataAccess(ConfigurationManager.ConnectionStrings(_connectionKey));

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
                            string queueMsg = string.Format("{0}|||{1}|||{2}",
                                change.Operation, change.IndexEntity, change.Id.ToString());

                            try
                            {
                                queueUtil.PutMessage(queueMsg, _mqQueueName);
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
        /// Read the command line arguments
        /// </summary>
        /// <returns></returns>
        private static void ReadConfig()
        {
            _startDate = ConfigurationManager.AppSettings("StartDate");
            _endDate = ConfigurationManager.AppSettings("EndDate");

            _mqAddress = ConfigurationManager.AppSettings("MQAddress");
            _mqPort = Convert.ToInt32(ConfigurationManager.AppSettings("MQPort"));
            _mqUser = ConfigurationManager.AppSettings("MQUser");
            _mqPw = ConfigurationManager.AppSettings("MQPassword");
            _mqQueueName = ConfigurationManager.AppSettings("MQQueueName");

            _smtpHost = ConfigurationManager.AppSettings("SmtpHost");
            _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings("SmtpPort"));
            _smtpEnableSsl = ConfigurationManager.AppSettings("SmtpEnableSsl") == "true";
            _smtpDefaultCredentials = ConfigurationManager.AppSettings("SmtpDefaultCredentials") == "true";
            _smtpUser = ConfigurationManager.AppSettings("SmtpUsername");
            _smtpPw = ConfigurationManager.AppSettings("SmtpPassword");
            _emailFromAddress = ConfigurationManager.AppSettings("EmailFromAddress");
            _emailToAddresses = ConfigurationManager.AppSettings("EmailToAddresses");

            _connectionKey = ConfigurationManager.AppSettings("ConnectionKey");
        }
    }
}
