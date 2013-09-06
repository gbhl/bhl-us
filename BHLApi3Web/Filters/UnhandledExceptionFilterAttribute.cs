// Code borrowed from 
// http://stackoverflow.com/questions/12519561/asp-net-web-api-throw-httpresponseexception-or-return-request-createerrorrespon
//
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace BHLApi3Web.Filters
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public UnhandledExceptionFilterAttribute() : base()
        {
        }

        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null || actionExecutedContext.Exception == null) return;

            var type = actionExecutedContext.Exception.GetType();

            Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>> registration = null;

            if (this.Handlers.TryGetValue(type, out registration))
            {
                var statusCode = registration.Item1;
                var handler = registration.Item2;

                var response = handler(actionExecutedContext.Exception.GetBaseException(), actionExecutedContext.Request);

                // Use registered status code if available
                if (statusCode.HasValue) response.StatusCode = statusCode.Value;

                actionExecutedContext.Response = response;
            }
            else
            {
                // If no exception handler registered for the exception type, fallback to default handler
                actionExecutedContext.Response =
                    DefaultHandler(actionExecutedContext.Exception.GetBaseException(), actionExecutedContext.Request);
            }
        }

        /// <summary>
        /// Gets a delegate method that returns an HttpResponseMessage that describes the supplied exception.
        /// </summary>
        /// <value>
        /// A Func{Exception, HttpRequestMessage, HttpResponseMessage} delegate method that returns 
        /// an HttpResponseMessage that describes the supplied exception.
        /// </value>
        private static Func<Exception, HttpRequestMessage, HttpResponseMessage> DefaultHandler = (exception, request) =>
        {
            if (exception == null) return null;
            var response = request.CreateResponse<string>(HttpStatusCode.InternalServerError, GetContentOf(exception));
            response.ReasonPhrase = exception.Message.Replace(Environment.NewLine, String.Empty);
            return response;
        };

        /// <summary>
        /// Gets a delegate method that extracts information from the specified exception.
        /// </summary>
        /// <value>
        /// A Func{Exception, String} delegate method that extracts information 
        /// from the specified exception.
        /// </value>
        private static Func<Exception, string> GetContentOf = (exception) =>
        {
            if (exception == null) return String.Empty;

            var result = new StringBuilder();

            result.AppendLine(exception.Message);
            result.AppendLine();

            Exception innerException = exception.InnerException;
            while (innerException != null)
            {
                result.AppendLine(innerException.Message);
                result.AppendLine();
                innerException = innerException.InnerException;
            }

            #if DEBUG
            result.AppendLine(exception.StackTrace);
            #endif

            return result.ToString();
        };

        #region Exception Handler Registration

        /// <summary>
        /// Gets the exception handlers registered with this filter.
        /// </summary>
        /// <value>
        /// A ConcurrentDictionary{Type, Tuple} collection that contains 
        /// the exception handlers registered with this filter.
        /// </value>
        protected ConcurrentDictionary<Type, Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>> Handlers
        {
            get { return _filterHandlers; }
        }
        private readonly ConcurrentDictionary<Type, Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>> _filterHandlers = new ConcurrentDictionary<Type, Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>>();

        /// <summary>
        /// Registers an exception handler that returns the specified status code for exceptions of type TException.
        /// </summary>
        /// <typeparam name="TException">The type of exception to register a handler for.</typeparam>
        /// <param name="statusCode">The HTTP status code to return for exceptions of type TException.</param>
        /// <returns>
        /// This UnhandledExceptionFilterAttribute after the exception handler has been added.
        /// </returns>
        public UnhandledExceptionFilterAttribute Register<TException>(HttpStatusCode statusCode) where TException : Exception
        {
            var type = typeof(TException);
            var item = new Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>(statusCode, DefaultHandler);

            if (!this.Handlers.TryAdd(type, item))
            {
                Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>> oldItem = null;
                if (this.Handlers.TryRemove(type, out oldItem)) this.Handlers.TryAdd(type, item);
            }

            return this;
        }

        /// <summary>
        /// Registers the specified exception handler for exceptions of type TException.
        /// </summary>
        /// <typeparam name="TException">The type of exception to register the handler for.</typeparam>
        /// <param name="handler">The exception handler responsible for exceptions of type TException.</param>
        /// <returns>
        /// This UnhandledExceptionFilterAttribute after the exception handler has been added.
        /// </returns>
        /// <exception cref="ArgumentNullException">The handler is null.</exception>
        public UnhandledExceptionFilterAttribute Register<TException>(Func<Exception, HttpRequestMessage, HttpResponseMessage> handler)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException("handler");

            var type = typeof(TException);
            var item = new Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>(null, handler);

            if (!this.Handlers.TryAdd(type, item))
            {
                Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>> oldItem = null;
                if (this.Handlers.TryRemove(type, out oldItem)) this.Handlers.TryAdd(type, item);
            }

            return this;
        }

        /// <summary>
        /// Unregisters the exception handler for exceptions of type TException.
        /// </summary>
        /// <typeparam name="TException">The type of exception to unregister handlers for.</typeparam>
        /// <returns>
        /// This UnhandledExceptionFilterAttribute after the exception handler 
        /// for exceptions of type TException has been removed.
        /// </returns>
        public UnhandledExceptionFilterAttribute Unregister<TException>() where TException : Exception
        {
            Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>> item = null;
            this.Handlers.TryRemove(typeof(TException), out item);
            return this;
        }

        #endregion Exception Handler Registration

    }
}