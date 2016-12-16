using System;
using System.Runtime.Serialization;

namespace EasyDapper.Data.Dapper.Helpers
{
    [Serializable]
    internal class InvalidMemberException : Exception
    {
        public InvalidMemberException()
        {
        }

        public InvalidMemberException(string message) : base(message)
        {
        }

        public InvalidMemberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}