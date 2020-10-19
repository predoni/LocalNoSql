using LocalNoSql_CSharp.DB;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace LocalNoSql_CSharp_NUnitTest.DB
{
    public class Database
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
        public void Drop(string rootPath, string databaseName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1} => {2}: {3}",
                    nameof(database.FullDatabasePath),
                    database.FullDatabasePath,
                    nameof(database.Drop),
                    database.Drop().ToString()
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

        [Test, Order(5)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "ExistentDB")]
        public void Version(string rootPath, string databaseName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1}",
                    nameof(database.Version),
                    database.Version()
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

        #region Collection
        [Test, Order(6)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "ExistentDB", "Collection01")]
        [TestCase("C:\\testdb\\", "ExistentDB", "Collec????tion01")]
        public void GetCollectionPath(string rootPath, string databaseName, string collectionName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1} => {2}",
                    nameof(database.GetCollectionPath),
                    collectionName,
                    database.GetCollectionPath(collectionName)
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
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(collectionName), collectionName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [Test, Order(7)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "ExistentDB", "Collection01")]
        [TestCase("C:\\testdb\\", "ExistentDB", "NonExistentCollection")]
        public void CollectionExists(string rootPath, string databaseName, string collectionName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1} => {2}",
                    nameof(database.CollectionExists),
                    collectionName,
                    database.CollectionExists(collectionName)
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
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(collectionName), collectionName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [Test, Order(8)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "ExistentDB", "Test01")]
        [TestCase("C:\\testdb\\", "ExistentDB", "Test01DeSters")]
        [TestCase("C:\\testdb\\", "ExistentDB", "Test01DeRedenumit")]
        [TestCase("C:\\testdb\\", "ExistentDB", "ExistentCollection")]
        public void CreateCollection(string rootPath, string databaseName, string collectionName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1} => {2}",
                    nameof(database.CreateCollection),
                    collectionName,
                    database.CreateCollection(collectionName)
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
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(collectionName), collectionName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [Test, Order(9)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "ExistentDB", "NonExistentCollection")]
        [TestCase("C:\\testdb\\", "ExistentDB", "Test01DeSters")]
        public void DropCollection(string rootPath, string databaseName, string collectionName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1} => {2}",
                    nameof(database.DropCollection),
                    collectionName,
                    database.DropCollection(collectionName)
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
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(collectionName), collectionName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [Test, Order(10)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "ExistentDB", "NonExistentCollection", "NewCollection")]
        [TestCase("C:\\testdb\\", "ExistentDB", "Test01DeRedenumit", "NewCollection01")]
        public void RenameCollection(string rootPath, string databaseName, string collectionCurrentName, string collectionNewName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                Assert.Pass(
                    "{0}: {1} => {2} = {3}",
                    nameof(database.RenameCollection),
                    collectionCurrentName,
                    collectionNewName,
                    database.RenameCollection(collectionCurrentName, collectionNewName)
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
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(collectionCurrentName), collectionCurrentName);
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(collectionNewName), collectionNewName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [Test, Order(11)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "ExistentDB")]
        public void GetCollections(string rootPath, string databaseName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);

                string[] collections = database.GetCollections();
                string collectionList = string.Empty;

                if (collections != null && collections.Length > 0)
                {
                    collectionList = "Collection list:" + Environment.NewLine;

                    foreach (string collection in collections)
                        collectionList += collection + Environment.NewLine;
                }

                Assert.Pass(
                    "{0}: {1}",
                    nameof(database.GetCollections),
                    collectionList
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

        [Test, Order(12)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb\\", "ExistentDB", "Collection01")]
        public void GetCollection(string rootPath, string databaseName, string collectionName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });
            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);

                IDBCollection collection = database.GetCollection(collectionName);

                Assert.Pass(
                    "{0}: {1}",
                    nameof(database.GetCollection),
                    collection.FullCollectionPath
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
        #endregion
    }
}
