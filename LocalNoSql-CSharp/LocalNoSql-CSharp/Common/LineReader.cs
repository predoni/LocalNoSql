using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.Common
{
    /// <summary>
    /// With this class, you can read line by line using FileStream.
    /// In other words, it makes it easier to read the lines in a file and maintains a secure lock.
    /// </summary>
    public class LineReader : IEnumerable<string>, IDisposable
    {
        private FileStream _fs;
        public LineReader(FileStream fs)
        {
            _fs = fs;
        }

        public IEnumerator<string> GetEnumerator()
        {
            string line;
            while ((line = this.ReadLine()) != null)
            {
                yield return line;
            }
        }

        private string ReadLine()
        {
            int b;
            string retStr = string.Empty;

            while(true)
            {
                b = this._fs.ReadByte();
                if (b == -1)
                    break;
                
                retStr += Convert.ToString((char)b);
                
                if (retStr.EndsWith(Environment.NewLine))
                    break;
            }

            return retStr;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            _fs.Dispose();
        }
    }
}
