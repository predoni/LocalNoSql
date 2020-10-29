using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.Common
{
    public static class JsonUtil
    {
        public static string GetJsonLine(string jsonString)
        {
            return Newtonsoft.Json.Linq.JObject.Parse(jsonString).ToString(Newtonsoft.Json.Formatting.None, null);
        }
    }
}
