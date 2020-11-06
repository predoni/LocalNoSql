using LocalNoSql_CSharp.Exceptions;
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

        /// <summary>
        /// "FullCollectionIndexPath" it is the path to the index file for the current collection.
        /// </summary>
        private string _FullCollectionIndexPath;
        public string FullCollectionIndexPath { get => this._FullCollectionIndexPath; }

        /// <summary>
        /// The period in seconds until the lock action is declared a failure.
        /// </summary>
        const int LockTimeout = 30;

        /// <summary>
        /// The current collection opened using a file stream.
        /// </summary>
        private FileStream FSCollection { get; set; }

        /// <summary>
        /// The current index opened using a file stream.
        /// </summary>
        private FileStream FSIndex { get; set; }
        #endregion

        #region Constructors and Destructor
        /// <summary>
        /// The constructor for collection object
        /// </summary>
        /// <param name="name">The collection name where all its documents reside.</param>
        /// <param name="fullCollectionPath">The path of the file for the current collection.</param>
        public DBCollection(string name, string fullCollectionPath, string fullCollectionIndexPath) : base()
        {
            if (string.IsNullOrEmpty(name))
                throw new EmptyStringForParameterException(nameof(name));

            if (string.IsNullOrEmpty(fullCollectionPath))
                throw new EmptyStringForParameterException(nameof(fullCollectionPath));

            if (string.IsNullOrEmpty(fullCollectionIndexPath))
                throw new EmptyStringForParameterException(nameof(fullCollectionIndexPath));

            this._Name = name;
            this._FullCollectionPath = fullCollectionPath;
            this._FullCollectionIndexPath = fullCollectionIndexPath;
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
        /// Returns the size in bytes of the index.
        /// </summary>
        /// <returns>double: the size in bytes of the index</returns>
        public double IndexSize()
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(this.FullCollectionIndexPath))
            {
                return fs.Length;
            }
        }

        /// <summary>
        /// Lock a collection and do not allow working on this collection until it releases the lock.
        /// </summary>
        /// <returns>true on success, false otherwise</returns>
#if TEST_LOCK
        public
#else
        private
# endif
        bool Lock()
        {
            DateTime deEnd = DateTime.Now.AddSeconds(DBCollection.LockTimeout);

            while (deEnd > DateTime.Now)
            {
                if (this.IsLocked())
                    System.Threading.Thread.Sleep(100);
                else
                {
                    try
                    {
                        this.FSCollection = System.IO.File.Open(this.FullCollectionPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                        this.FSIndex = System.IO.File.Open(this.FullCollectionIndexPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

                        return true;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            
            return false;
        }

        /// <summary>
        /// Unlocks a collection.
        /// </summary>
        /// <returns>true on success, false otherwise</returns>
#if TEST_LOCK
        public
#else
        private
#endif
        bool Unlock()
        {
            this.FSCollection.Close();
            this.FSIndex.Close();

            return true;
        }

        /// <summary>
        /// Verifies if the collection is locked.
        /// </summary>
        /// <returns>true if is locked, false otherwise</returns>
        public bool IsLocked()
        {
            try
            {
                FileStream fs = System.IO.File.Open(this.FullCollectionPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                fs.Close();

                return false;
            }
            catch
            {
                return true;
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

        /// <summary>
        /// Inserts documents in a collection.
        /// As parameter accepts only JSON array.
        /// </summary>
        /// <param name="jsonDocumentString">The json string that represents an array of documents.</param>
        /// <exception cref="Exceptions.EmptyStringForParameterException">Empty json string.</exception>
        /// <exception cref="Exceptions.EmptyArrayException">Empty json array."</exception>
        /// <exception cref="Exceptions.DuplicatedIdException">This "Id" already exists.</exception>
        /// <returns>total number of inserted documents</returns>
        public int Insert(string jsonDocumentString)
        {
            if (string.IsNullOrEmpty(jsonDocumentString))
                throw new EmptyStringForParameterException(nameof(jsonDocumentString));

            Newtonsoft.Json.Linq.JArray jarr = Newtonsoft.Json.Linq.JArray.Parse(jsonDocumentString);
            if (jarr.Count <= 0)
                throw new EmptyArrayException(nameof(jsonDocumentString));

            int docsNum = 0;
            string jsonLine;
            
            if (this.Lock())
            {
                using (StreamWriter swCollection = new StreamWriter(this.FSCollection))
                {
                    using (StreamWriter swIndex = new StreamWriter(this.FSIndex))
                    {
                        long startPosition;
                        long endPosition;
                        long lengthTotal;

                        for (int i = 0; i < jarr.Count; i++)
                        {
                            jsonLine = Common.JsonUtil.GetJsonLine(jarr[i].ToString());
                            System.Diagnostics.Debug.WriteLine(jsonLine);

                            Model.DocumentObject<string> documentObject =
                                Newtonsoft.Json.JsonConvert.DeserializeObject<Model.DocumentObject<string>>(jsonLine);

                            if (this.IdExists(documentObject.Id))
                                throw new Exceptions.DuplicatedIdException();
                            else
                            {
                                // The position from where to start insert the document.
                                startPosition = this.GetPositionToInsertIntoCollection();
                                this.FSCollection.Seek(startPosition, SeekOrigin.Begin);
                                swCollection.WriteLine(jsonLine);
                                swCollection.Flush();
                                endPosition = this.FSCollection.Position;

                                // This value contains the NewLine and CarriageReturn character
                                lengthTotal = endPosition - startPosition;

                                // insert into index
                                string indexLine = documentObject.Id + "," + startPosition.ToString() + "," + lengthTotal.ToString();
                                startPosition = this.GetPositionToInsertIntoIndex();
                                this.FSIndex.Seek(startPosition, SeekOrigin.Begin);
                                swIndex.WriteLine(indexLine);
                                swIndex.Flush();

                                docsNum++;
                            }
                        }
                    }
                }

                return docsNum;
            }
            else
                throw new TimeoutException(Resource.Exceptions.TimeoutExpired);
        }

        public Guid Int2Guid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 12);
            return new Guid(bytes);
        }

        public bool IdExists(string id)
        {
            
            return false;
        }

        public long GetPositionToInsertIntoCollection()
        {
            return this.FSCollection.Length;
        }

        public long GetPositionToInsertIntoIndex()
        {
            return this.FSIndex.Length;
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
