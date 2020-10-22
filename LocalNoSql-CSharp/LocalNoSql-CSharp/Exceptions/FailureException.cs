using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.Exceptions
{
    public class FailureException : Exception
    {
        public FailureException() : base()
        {
        }

        public FailureException(string message) : base(message)
        {
        }

        public FailureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FailureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
