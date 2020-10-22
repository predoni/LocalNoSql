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
            base(Resource.Exceptions.Empty_array_for_parameter)
        {
            
        }

        public IdColumnMissingException(Exception innerException) :
            base(Resource.Exceptions.InvalidDocumentId, innerException)
        {
        }
    }
}
