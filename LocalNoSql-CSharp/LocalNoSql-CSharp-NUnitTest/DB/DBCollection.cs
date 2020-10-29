using LocalNoSql_CSharp.DB;
using LocalNoSql_CSharp.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LocalNoSql_CSharp_NUnitTest.DB
{
    public class DBCollection
    {
        private const string JSON_EmptyString = "";
        private const string JSON_EmptyArray = "[]";
        private const string JSON_OneDocumentArray = "[{Id: 1, col1: \"Value 1\"}]";
        private const string JSON_ManyDocumentsArray = "[{Id: 1, col1: \"Value 1\"}, {Id: 2, col1: \"Value 2\"}, {Id: 3, col1: \"Value 3\"}, {Id: 4, col1: \"Value 4\"}, {Id: 5, col1: \"Value 5\"}]";
        private const string JSON_DuplicatedIds = "[{Id: 1, col1: \"Value 1\"}, {Id: 2, col1: \"Value 2\"}, {Id: 2, col1: \"Value 3\"}]";

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb", "ExistentDB", "Collection01")]
        [TestCase("C:\\testdb", "ExistentDB", "NotExistentCollection")]
        public void Constructor(string rootPath, string databaseName, string collectionName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                IDBCollection collection = database.GetCollection(collectionName);

                Assert.Pass
                (
                    "{0}",
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
                System.Diagnostics.Debug.WriteLine("{0}: {1}", nameof(collectionName), collectionName);
                System.Diagnostics.Debug.WriteLine(e.Message + Environment.NewLine);
            }
        }

        [Test, Order(2)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb", "ExistentDB", "Collection01")]
        public void DataSize(string rootPath, string databaseName, string collectionName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                IDBCollection collection = database.GetCollection(collectionName);

                Assert.Pass
                (
                    "{0}: {1} = {2}",
                    collection.FullCollectionPath,
                    nameof(collection.DataSize),
                    collection.DataSize().ToString()
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

        [Test, Order(3)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb", "ExistentDB", "Collection01", DBCollection.JSON_EmptyString)]
        [TestCase("C:\\testdb", "ExistentDB", "Collection01", DBCollection.JSON_EmptyArray)]
        [TestCase("C:\\testdb", "ExistentDB", "Collection01", DBCollection.JSON_ManyDocumentsArray)]
        [TestCase("C:\\testdb", "ExistentDB", "Collection01", DBCollection.JSON_DuplicatedIds)]
        public void Insert(string rootPath, string databaseName, string collectionName, string jsonArray)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                IDBCollection collection = database.GetCollection(collectionName);
                
                Assert.Pass
                (
                    "{0}: {1} = {2}{3}Json array:{4}",
                    collection.FullCollectionPath,
                    nameof(collection.Insert),
                    collection.Insert(jsonArray),
                    Environment.NewLine,
                    jsonArray
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

        [Test, Order(4)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb", "ExistentDB", "Collection01")]
        public void Insert(string rootPath, string databaseName, string collectionName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                IDBCollection collection = database.GetCollection(collectionName);
                string jsonArray = 
                    "[" +
                        "{Id: \"" + Guid.NewGuid().ToString() + "\", col1: \"Value 1\"}, " +
                        "{Id: \"" + Guid.NewGuid().ToString() + "\", col1: \"Value 2\"}, " +
                        "{Id: \"" + Guid.NewGuid().ToString() + "\", col1: \"Value 3\"}" +
                    "]";

                Assert.Pass
                (
                    "{0}: {1} = {2}{3}Json array:{4}",
                    collection.FullCollectionPath,
                    nameof(collection.Insert),
                    collection.Insert(jsonArray),
                    Environment.NewLine,
                    jsonArray
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

        [Test, Order(5)] // You can see the order in debug. Is correct!
        [TestCase("C:\\testdb", "ExistentDB", "Collection01")]
        public void Lock_IsLocked_Unlock(string rootPath, string databaseName, string collectionName)
        {
            System.Diagnostics.Debug.WriteLine("Start testing: {0}", new[] { System.Reflection.MethodInfo.GetCurrentMethod().Name });

            try
            {
#if TEST_LOCK
                LocalNoSql_CSharp.DB.Database database = new LocalNoSql_CSharp.DB.Database(rootPath, databaseName);
                IDBCollection collection = database.GetCollection(collectionName);

                bool lockCll = collection.Lock();
                bool isLocked = collection.IsLocked();
                bool unlock = collection.Unlock();

                Assert.Pass
                (
                    "{0}: Lock: {1}, IsLocked: {2}, Unlock: {3}",
                    collection.FullCollectionPath,
                    Convert.ToString(lockCll),
                    Convert.ToString(lockCll),
                    Convert.ToString(lockCll)
                );
#else
                throw new FailureException(LocalNoSql_CSharp.Resource.Exceptions.TEST_LOCK_Exception);
#endif
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
    }
}
