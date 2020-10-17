using NUnit.Framework;
using System;

namespace LocalNoSql_CSharp_NUnitTest.DB
{
    public class Nu_Database
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("C:\\testdb", "")]
        [TestCase("", "testDBName")]
        [TestCase("C:\\testdb\\", "testDBNa????me")]
        [TestCase("C:\\tes???tdb\\", "testDBName")]
        [TestCase("C:\\testdb", "fld\\testDBName")]
        [TestCase("C:\\", "testDBName")]
        [TestCase("C:\\testdb\\", "testDBName")]
        public void Constructor(string rootPath, string databaseName)
        {
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);

                Assert.Pass
                (
                    string.Format
                    (
                        "{0}: {1}{2}{3}: {4}{5}{6}: {7}",
                        nameof(database.RootPath), database.RootPath, Environment.NewLine,
                        nameof(database.DatabaseName), database.DatabaseName, Environment.NewLine,
                        nameof(database.FullDatabasePath), database.FullDatabasePath
                    )
                );
            }
            catch (NUnit.Framework.SuccessException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", nameof(rootPath), rootPath));
                System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", nameof(databaseName), databaseName));
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [TestCase("C:\\testdb\\", "testDBName")]
        [TestCase("C:\\testdb\\", "ExistentDB")]
        public void Exists(string rootPath, string databaseName)
        {
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(string.Format("{0}: {1} => exists: {2}", nameof(database.FullDatabasePath), database.FullDatabasePath, database.Exists().ToString()));
            }
            catch (NUnit.Framework.SuccessException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", nameof(rootPath), rootPath));
                System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", nameof(databaseName), databaseName));
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }
    }
}
