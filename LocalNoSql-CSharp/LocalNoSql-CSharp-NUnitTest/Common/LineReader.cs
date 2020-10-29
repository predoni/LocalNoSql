using LocalNoSql_CSharp.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LocalNoSql_CSharp_NUnitTest.Common
{
    public class LineReader
    {
        public class Database
        {
            [SetUp]
            public void Setup()
            {
            }

            [Test, Order(1)] // You can see the order in debug. Is correct!
            [TestCase(@"C:\testdb\ExistentDB\Collection01.cll")]
            public void Example(string filePath)
            {
                System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

                try
                {
                    // path is string
                    int skip = 2;
                    FileStream fs = System.IO.File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    
                    using (var lineReader = new LocalNoSql_CSharp.Common.LineReader(fs))
                    {
                        System.Diagnostics.Debug.WriteLine("Test 1 ============================================");
                        IEnumerable<string> lines = lineReader.Skip(skip);
                        foreach (string line in lines)
                        {
                            System.Diagnostics.Debug.WriteLine(line);
                        }

                        System.Diagnostics.Debug.WriteLine("Test 2 ============================================");

                        lineReader.DiscardBufferedData();
                        if (fs.Seek(0, SeekOrigin.Begin) != 0)
                            throw new FailureException("Cursor position has not been set to the beginning or the file.");

                        lines = lineReader.Skip(1).Take(2);
                        foreach (string line in lines)
                        {
                            System.Diagnostics.Debug.WriteLine(line);
                        }

                        System.Diagnostics.Debug.WriteLine("Test 3 ============================================");

                        lineReader.DiscardBufferedData();
                        if (fs.Seek(0, SeekOrigin.Begin) != 0)
                            throw new FailureException("Cursor position has not been set to the beginning or the file.");
                        
                        lines = lineReader.AsEnumerable();
                        foreach (string line in lineReader)
                        {
                            System.Diagnostics.Debug.WriteLine(line);
                        }

                        System.Diagnostics.Debug.WriteLine("END ============================================");
                    }

                    fs.Close();

                    Assert.Pass
                    (
                        "{0}: {1}",
                        nameof(filePath),
                        filePath
                    );
                }
                catch (NUnit.Framework.SuccessException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(filePath), filePath);
                    System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
                }
            }
        }
    }
}
