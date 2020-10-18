using NUnit.Framework;
using System;
using System.Reflection;

namespace LocalNoSql_CSharp_NUnitTest.DB
{
    public class Nu_Database
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb", "")]
        [TestCase("", "testDBName")]
        [TestCase("C:\\testdb\\", "testDBNa????me")]
        [TestCase("C:\\tes???tdb\\", "testDBName")]
        [TestCase("C:\\testdb", "fld\\testDBName")]
        [TestCase("C:\\", "testDBName")]
        [TestCase("C:\\testdb\\", "testDBName")]
        public void Constructor(string rootPath, string databaseName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);

                Assert.Pass
                (
                    "{0}: {1}{2}{3}: {4}{5}{6}: {7}", 
                    nameof(database.RootPath), database.RootPath, Environment.NewLine,
                    nameof(database.DatabaseName), database.DatabaseName, Environment.NewLine,
                    nameof(database.FullDatabasePath), database.FullDatabasePath
                );
            }
            catch (NUnit.Framework.SuccessException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(rootPath), rootPath);
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(databaseName), databaseName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [Test, Order(2)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "testDBName")]
        [TestCase("C:\\testdb\\", "ExistentDB")]
        public void Exists(string rootPath, string databaseName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1} => {2}: {3}", 
                    nameof(database.FullDatabasePath), 
                    database.FullDatabasePath, 
                    nameof(database.Exists), 
                    database.Exists().ToString()
                );
            }
            catch (NUnit.Framework.SuccessException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(rootPath), rootPath);
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(databaseName), databaseName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [Test, Order(3)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "newDBToBeCreated")]
        [TestCase("C:\\testdb\\", "ExistentDB")]
        public void Create(string rootPath, string databaseName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1} => {2}: {3}",
                    nameof(database.FullDatabasePath),
                    database.FullDatabasePath,
                    nameof(database.Create),
                    database.Create().ToString()
                );
            }
            catch (NUnit.Framework.SuccessException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(rootPath), rootPath);
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(databaseName), databaseName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [Test, Order(4)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "newDBToBeCreated")]
        [TestCase("C:\\testdb\\", "NonExistentDB")]
        public void Delete(string rootPath, string databaseName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1} => {2}: {3}",
                    nameof(database.FullDatabasePath),
                    database.FullDatabasePath,
                    nameof(database.Delete),
                    database.Delete().ToString()
                );
            }
            catch (NUnit.Framework.SuccessException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(rootPath), rootPath);
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(databaseName), databaseName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }
    }
}
