﻿using LocalNoSql_CSharp.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.DB
{
    public class DBCollection : Object, LocalNoSql_CSharp.DB.IDBCollection
    {
        #region Properties
        /// <summary>
        /// The collection name.
        /// </summary>
        private string _Name;
        public string Name { get => this._Name; }

        /// <summary>
        /// "FullCollectionPath" is the path of the file for the current collection.
        /// </summary>
        private string _FullCollectionPath;
        public string FullCollectionPath { get => this._FullCollectionPath; }
        #endregion

        #region Constructors and Destructor
        /// <summary>
        /// The constructor for collection object
        /// </summary>
        /// <param name="name">The collection name where all its documents reside.</param>
        /// <param name="fullCollectionPath">The path of the file for the current collection.</param>
        public DBCollection(string name, string fullCollectionPath) : base()
        {
            if (string.IsNullOrEmpty(name))
                throw new EmptyStringForParameterException(nameof(name));

            if (string.IsNullOrEmpty(fullCollectionPath))
                throw new EmptyStringForParameterException(nameof(fullCollectionPath));

            this._Name = name;
            this._FullCollectionPath = fullCollectionPath;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Counts the documents in a collection.
        /// </summary>
        /// <returns>The number of documents in a collection</returns>
        public uint Count()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the size in bytes of the collection.
        /// </summary>
        /// <returns>double: the size in bytes of the collection</returns>
        public double DataSize()
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(this.FullCollectionPath))
            {
                return fs.Length;
            }
        }

        /// <summary>
        /// Deletes documents from a collection.
        /// </summary>
        /// <returns>Total deleted documents.</returns>
        public int Delete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an interator object of documents that have distinct values for the specified field.
        /// </summary>
        /// <returns>Array of documents</returns>
        public IEnumerator Distinct()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs a query on a collection and returns an interator object.
        /// </summary>
        public IEnumerator Find()
        {
            throw new NotImplementedException();
        }

        class ObjId : Object
        {
            public object _id { get; set; }
        }

        /// <summary>
        /// Inserts documents in a collection.
        /// As parameter accepts only JSON array.
        /// </summary>
        /// <param name="jsonDocumentString">The json string that represents an array of documents.</param>
        /// <exception cref="Exceptions.EmptyStringForParameterException">Empty json string.</exception>
        /// <exception cref="Exceptions.EmptyArrayException">Empty json array."</exception>
        /// <exception cref="Exceptions.FailureException">Empty json array."</exception>
        /// <returns>total number of inserted documents</returns>
        public int Insert(string jsonDocumentString)
        {
            if (string.IsNullOrEmpty(jsonDocumentString))
                throw new EmptyStringForParameterException(nameof(jsonDocumentString));

            Newtonsoft.Json.Linq.JArray jarr = Newtonsoft.Json.Linq.JArray.Parse(jsonDocumentString);
            if (jarr.Count <= 0)
                throw new EmptyArrayException(nameof(jsonDocumentString));

            int docsNum = 0;
            for (int i = 0; i < jarr.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(jarr[i].ToString());

                Model.DocumentObject<string> documentObject = 
                    Newtonsoft.Json.JsonConvert.DeserializeObject<Model.DocumentObject<string>>(jarr[i].ToString());

                byte[] document = Encoding.ASCII.GetBytes(Common.JsonUtil.GetFormattedJsonLine(jarr[i].ToString()) + Environment.NewLine);

                using (System.IO.FileStream fs = System.IO.File.Open(this.FullCollectionPath, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    
                    fs.Write(document, 0, document.Length);
                    fs.Flush();

                    docsNum++;
                }
            }

            return docsNum;
        }

        /// <summary>
        /// Performs map-reduce style data aggregation.
        /// </summary>
        /// <returns></returns>
        public IEnumerator MapReduce()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Replaces a single document in a collection.
        /// </summary>
        /// <returns>true on success, otherwise false.</returns>
        public bool Replace()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reports on the state of a collection.
        /// </summary>
        public void Stats()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modifies documents in a collection.
        /// </summary>
        /// <returns>The number of updated documents</returns>
        public int Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs diagnostic operations on a collection.
        /// </summary>
        /// <returns>true on success, otherwise false.</returns>
        public bool Validate()
        {
            throw new NotImplementedException();
        }

        #region Index
        /// <summary>
        /// Builds an index on a collection.
        /// </summary>
        /// <returns>IIndex: info about the created index.</returns>
        public IIndex CreateIndex()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a specified index on a collection.
        /// </summary>
        /// <param name="name">The index name</param>
        /// <returns>true on success, otherwise false.</returns>
        public bool DropIndex(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Drop and then creates the index.
        /// </summary>
        /// <param name="name">The index name</param>
        /// <returns>true on success, otherwise false.</returns>
        public bool RebuildIndex(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a collection of indexes from a given collection.
        /// </summary>
        /// <param name="collectionName">The collection name.</param>
        /// <returns>IEnumerator: Returns a collection of indexes from a given collection.</returns>
        public IEnumerator GetIndexes(string collectionName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reports the total size used by the indexes on a collection.Provides a wrapper around the totalIndexSize field of the collStats output.
        /// </summary>
        /// <param name="name">Index name</param>
        /// <returns>The index size.</returns>
        public double IndexSize(string name)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}