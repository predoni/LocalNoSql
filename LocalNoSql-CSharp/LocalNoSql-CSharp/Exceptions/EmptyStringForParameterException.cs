using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.Exceptions
{
    public class EmptyStringForParameterException : Exception
    {
        public EmptyStringForParameterException(string parameterName) : 
            base(Resource.Exceptions.Empty_string_for_parameter + parameterName)
        {
            
        }

        public EmptyStringForParameterException(string parameterName, Exception innerException) :
            base(Resource.Exceptions.Empty_string_for_parameter + parameterName, innerException)
        {
        }
    }
}
