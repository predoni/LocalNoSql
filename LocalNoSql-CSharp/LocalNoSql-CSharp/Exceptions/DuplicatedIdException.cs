using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.Exceptions
{
    public class DuplicatedIdException : Exception
    {
        public DuplicatedIdException() : 
            base(Resource.Exceptions.DuplicatedId)
        {
        }

        public DuplicatedIdException(Exception innerException) :
            base(Resource.Exceptions.DuplicatedId, innerException)
        {
        }
    }
}
