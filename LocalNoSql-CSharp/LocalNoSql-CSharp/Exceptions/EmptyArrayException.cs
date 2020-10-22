using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.Exceptions
{
    public class EmptyArrayException : Exception
    {
        public EmptyArrayException(string parameterName) : 
            base(Resource.Exceptions.Empty_array_for_parameter + parameterName)
        {
            
        }

        public EmptyArrayException(string parameterName, Exception innerException) :
            base(Resource.Exceptions.Empty_array_for_parameter + parameterName, innerException)
        {
        }
    }
}
