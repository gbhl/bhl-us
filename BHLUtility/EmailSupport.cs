using System;
using System.Text.RegularExpressions;
using System.Net.Mail;
using MailMessage = System.Net.Mail.MailMessage;
using MailPriority = System.Net.Mail.MailPriority;

namespace MOBOT.BHL.Utility
{
    /// <summary>
    /// Supports Sending Email Using SMTP
    /// </summary>
    public class EmailSupport
    {
        #region Constructors

        /// <summary>
        /// Syntaxically validate a specified server name.
        /// </summary>
        /// <param name="server"></param>
        public EmailSupport(string server)
        {
            if (IsServerNameValid(server))
            {
                MailServer = server;
            }
            else
            {
                throw new Exception(string.Format("The server name specified ({0}) is syntaxically incorrect.", server));
            }
        }

        public EmailSupport()
        {
            MailServer = string.Empty;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public static bool IsServerNameValid(string server)
        {
            const string expression = @"^[a-zA-Z^@][\w\.-]*[a-zA-Z0-9^@]$";
            try
            {
                Regex regex = new Regex(expression);
                if (regex.IsMatch(server))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #region Mail Server

        /// <summary>
        /// 
        /// </summary>
        private string _MailServer = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private string MailServer
        {
            get
            {
                return _MailServer;
            }
            set
            {
                _MailServer = value;
            }
        }

        #endregion

        #region Send Email overloaded

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="replyTo"></param>
        /// <param name="bcc"></param>
        /// <param name="cc"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public bool Send(MailAddress[] to, MailAddress from, string subject, string body, bool isBodyHtml, MailAddress replyTo, MailAddress[] bcc, MailAddress[] cc, MailPriority priority)
        {
            return Send(to, from, subject, body, isBodyHtml, replyTo, bcc, cc, priority, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="replyTo"></param>
        /// <param name="bcc"></param>
        /// <param name="cc"></param>
        /// <returns></returns>
        public bool Send(MailAddress[] to, MailAddress from, string subject, string body, bool isBodyHtml, MailAddress replyTo, MailAddress[] bcc, MailAddress[] cc)
        {
            return Send(to, from, subject, body, isBodyHtml, replyTo, bcc, cc, MailPriority.Normal, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="replyTo"></param>
        /// <returns></returns>
        public bool Send(MailAddress[] to, MailAddress from, string subject, string body, bool isBodyHtml, MailAddress replyTo)
        {
            return Send(to, from, subject, body, isBodyHtml, replyTo, null, null, MailPriority.Normal, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public bool Send(MailAddress[] to, MailAddress from, string subject, string body, bool isBodyHtml)
        {
            return Send(to, from, subject, body, isBodyHtml, null, null, null, MailPriority.Normal, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="replyTo"></param>
        /// <param name="bcc"></param>
        /// <param name="cc"></param>
        /// <param name="priority"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public bool Send(MailAddress[] to, MailAddress from, string subject, string body, bool isBodyHtml, MailAddress replyTo, MailAddress[] bcc, MailAddress[] cc, MailPriority priority, Attachment[] attachments)
        {
            return sendMail(to, from, subject, body, isBodyHtml, replyTo, bcc, cc, priority, attachments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <param name="replyTo"></param>
        /// <param name="bcc"></param>
        /// <param name="cc"></param>
        /// <param name="priority"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        private bool sendMail(MailAddress[] to, MailAddress from, string subject, string body, bool isBodyHtml, MailAddress replyTo, MailAddress[] bcc, MailAddress[] cc, MailPriority priority, Attachment[] attachments)
        {
            MailMessage mail = new MailMessage();

            if (to != null)
            {
                for (int i = 0; i < to.Length; i++)
                {
                    mail.To.Add(to[i]);
                }
            }
            mail.IsBodyHtml = isBodyHtml;
            mail.From = from;
            mail.Subject = subject;
            mail.Body = body;
            if (replyTo != null)
            {
                mail.ReplyToList.Add(replyTo);
            }
            if (bcc != null)
            {
                for (int i = 0; i < bcc.Length; i++)
                {
                    mail.Bcc.Add(bcc[i]);
                }
            }
            if (cc != null)
            {
                for (int i = 0; i < cc.Length; i++)
                {
                    mail.CC.Add(cc[i]);
                }
            }
            mail.Priority = priority;
            if (attachments != null)
            {
                for (int i = 0; i < attachments.Length; i++)
                {
                    mail.Attachments.Add(attachments[i]);
                }
            }

            SmtpClient smtpMail = new SmtpClient();
            smtpMail.Host = MailServer;
            try
            {
                smtpMail.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                mail = null;
                smtpMail = null;
            }
        }

        #endregion
    }
}
