using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.Common
{
    class LineReader : IEnumerable<string>, IDisposable
    {
        TextReader _reader;
        public LineReader(TextReader reader)
        {
            _reader = reader;
        }

        public IEnumerator<string> GetEnumerator()
        {
            string line;
            while ((line = _reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public void Example()
        {
            // path is string
            int skip = 300;
            StreamReader sr = new StreamReader("Some path");
            using (var lineReader = new LineReader(sr))
            {
                IEnumerable<string> lines = lineReader.Skip(skip);
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
