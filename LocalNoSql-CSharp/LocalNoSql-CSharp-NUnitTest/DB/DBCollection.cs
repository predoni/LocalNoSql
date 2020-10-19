using LocalNoSql_CSharp.DB;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalNoSql_CSharp_NUnitTest.DB
{
    public class DBCollection
    {
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
    }
}
