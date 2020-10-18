using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.DB
{
    public interface IDBCollection
    {
        #region Properties
        /// <summary>
        /// The collection name.
        /// </summary>
        string Name { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Counts the documents in a collection.
        /// </summary>
        /// <returns>The number of documents in a collection</returns>
        uint Count();

        /// <summary>
        /// Returns the size of the collection.
        /// </summary>
        /// <returns>double: the size of the collection</returns>
        double DataSize();

        /// <summary>
        /// Deletes documents from a collection.
        /// </summary>
        /// <returns>Total deleted documents.</returns>
        int Delete();

        /// <summary>
        /// Returns an interator object of documents that have distinct values for the specified field.
        /// </summary>
        /// <returns>Array of documents</returns>
        IEnumerator Distinct();

        /// <summary>
        /// Performs a query on a collection and returns an interator object.
        /// </summary>
        IEnumerator Find();

        /// <summary>
        /// Inserts documents in a collection.
        /// </summary>
        /// <exception cref="Exception">On failure</exception>
        /// <returns>total number of inserted documents</returns>
        int Insert();

        /// <summary>
        /// Performs map-reduce style data aggregation.
        /// </summary>
        /// <returns></returns>
        IEnumerator MapReduce();

        /// <summary>
        /// Replaces a single document in a collection.
        /// </summary>
        /// <returns>true on success, otherwise false.</returns>
        bool Replace();

        /// <summary>
        /// Reports on the state of a collection.
        /// </summary>
        void Stats();

        /// <summary>
        /// Modifies documents in a collection.
        /// </summary>
        /// <returns>The number of updated documents</returns>
        int Update();

        /// <summary>
        /// Performs diagnostic operations on a collection.
        /// </summary>
        /// <returns>true on success, otherwise false.</returns>
        bool Validate();

        #region Index
        /// <summary>
        /// Builds an index on a collection.
        /// </summary>
        /// <returns>IIndex: info about the created index.</returns>
        IIndex CreateIndex();

        /// <summary>
        /// Removes a specified index on a collection.
        /// </summary>
        /// <param name="name">The index name</param>
        /// <returns>true on success, otherwise false.</returns>
        bool DropIndex(string name);

        /// <summary>
        /// Drop and then creates the index.
        /// </summary>
        /// <param name="name">The index name</param>
        /// <returns>true on success, otherwise false.</returns>
        bool RebuildIndex(string name);

        /// <summary>
        /// Returns a collection of indexes from a given collection.
        /// </summary>
        /// <param name="collectionName">The collection name.</param>
        /// <returns>IEnumerator: Returns a collection of indexes from a given collection.</returns>
        IEnumerator GetIndexes(string collectionName);

        /// <summary>
        /// Reports the total size used by the indexes on a collection.Provides a wrapper around the totalIndexSize field of the collStats output.
        /// </summary>
        /// <param name="name">Index name</param>
        /// <returns>The index size.</returns>
        double IndexSize(string name);
        #endregion

        #endregion
    }
}
