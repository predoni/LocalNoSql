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
        private readonly StreamReader _sr;
        public bool EndOfStream { get => this._sr.EndOfStream; }
        
        public LineReader(FileStream fs) =>  this._sr = new StreamReader(fs);

        public IEnumerator<string> GetEnumerator()
        {
            string line;
            while ((line = this._sr.ReadLine()) != null)
            {
                yield return line;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void DiscardBufferedData() => this._sr.DiscardBufferedData();

        public void Dispose() => this._sr.Dispose();
    }
}
