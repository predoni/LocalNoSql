using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.Exceptions
{
    public class IdColumnMissingException : Exception
    {
        public IdColumnMissingException() : 
            base(Resource.Exceptions.IdColumnMissing)
        {
            
        }

        public IdColumnMissingException(Exception innerException) :
            base(Resource.Exceptions.IdColumnMissing, innerException)
        {
        }
    }
}
