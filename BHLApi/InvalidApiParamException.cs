using System;

namespace MOBOT.BHL.API.BHLApi
{
    public class InvalidApiParamException : Exception
    {
        public InvalidApiParamException()
        { }

        public InvalidApiParamException(string message) : base(message)
        { }

        public InvalidApiParamException(string message, Exception inner) : base(message, inner) { }
    }
}
