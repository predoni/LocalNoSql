using LocalNoSql_CSharp.Common;
using LocalNoSql_CSharp.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.DB
{
    [Serializable]
    public class Database : Object, LocalNoSql_CSharp.DB.IDatabase
    {
        #region Properties
        const string CollectionFileExtension = "cll";

        /// <summary>
        /// The database is a directory!
        /// "RootPath" is the full directory path, where all databases will be created as subdirectories.
        /// "RootPath" will implement the security from the operating system configuration.
        /// </summary>
        private string _RootPath;
        public string RootPath { get => this._RootPath; }

        /// <summary>
        /// The "DatabaseName" is the directory name where all its collections reside.
        /// </summary>
        private string _DatabaseName;
        public string DatabaseName { get => this._DatabaseName; }

        /// <summary>
        /// "FullDatabasePath" is the combination between "RootPath" and "DatabaseName".
        /// </summary>
        private string _FullDatabasePath;
        public string FullDatabasePath { get => this._FullDatabasePath; }

        /// <summary>
        /// The last error object
        /// </summary>
        public IError LastError { get; set; }

        /// <summary>
        /// The log level that has been set
        /// </summary>
        public LogLevel LogLevel { get; set; }
        #endregion

        #region Constructors and Destructor
        /// <summary>
        /// The constructor for database object
        /// </summary>
        /// <param name="rootPath">Is the full directory path, where all databases will be created as subdirectories.</param>
        /// <param name="databaseName">Is the directory name where all its collections reside.</param>
        public Database(string rootPath, string databaseName) : base()
        {
            if (string.IsNullOrEmpty(rootPath))
                throw new Exception(Resource.Exceptions.Empty_string_for_parameter + nameof(rootPath));

            if (string.IsNullOrEmpty(databaseName))
                throw new Exception(Resource.Exceptions.Empty_string_for_parameter + nameof(databaseName));

            if (!System.IO.Directory.Exists(rootPath))
                throw new DirectoryNotFoundException(Resource.Exceptions.Directory_not_found + rootPath);

            if (databaseName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
                throw new Exception(
                    string.Format(
                        "{0} {1} = {2}", 
                        Resource.Exceptions.Invalid_characters_for_parameter,
                        nameof(databaseName),
                        databaseName
                    )
                );

            if (databaseName.IndexOf(System.IO.Path.DirectorySeparatorChar) != -1)
                throw new Exception(Resource.Exceptions.Invalid_characters_for_parameter + Environment.NewLine + databaseName);

            if (System.IO.Directory.GetDirectoryRoot(rootPath).Equals(rootPath))
                throw new Exception(Resource.Exceptions.Root_directory_not_allowed + Environment.NewLine + nameof(rootPath));

            this._RootPath = rootPath.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) 
                             ? rootPath.Substring(0, rootPath.Length - 1)
                             : rootPath;
            this._DatabaseName = databaseName;
            this._FullDatabasePath = System.IO.Path.Combine(this.RootPath, this.DatabaseName);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Verifies if the current database exists.
        /// </summary>
        /// <returns>Returns true if the database exists, otherwise false.</returns>
        public bool Exists()
        {
            return System.IO.Directory.Exists(this.FullDatabasePath);
        }

        /// <summary>
        /// Creates the database if it does not exists.
        /// </summary>
        /// <exception cref="IOException">
        /// The directory specified by path is a file.
        /// -or-
        /// The network name is not known.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">
        /// path is a zero-length string, contains only white space, or contains one or more invalid characters.You can query for invalid characters by using the GetInvalidPathChars() method.
        /// -or-
        /// path is prefixed with, or contains, only a colon character(:).
        /// </exception>
        /// <exception cref="ArgumentNullException">path is null.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid(for example, it is on an unmapped drive).</exception>
        /// <exception cref="NotSupportedException">path contains a colon character(:) that is not part of a drive label("C:\").</exception>
        /// <returns>Returns true if the database was created successfully or if already exists, otherwise false.</returns>
        public bool Create()
        {
            if (this.Exists())
                // Is already created.
                return true;

            // Discard the returned value.
            _ = System.IO.Directory.CreateDirectory(this.FullDatabasePath);

            return true;
        }

        /// <summary>
        /// Delete a database directory with its content.
        /// </summary>
        /// <exception cref="IOException">
        /// A file with the same name and location specified by path exists.
        /// -or-
        /// The directory is the application's current working directory.
        /// -or-
        /// The directory specified by path is not empty.
        /// -or-
        /// The directory is read-only or contains a read-only file.
        /// -or-
        /// The directory is being used by another process.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">path is a zero-length string, contains only white space, or contains one or more invalid characters.You can query for invalid characters by using the GetInvalidPathChars() method.</exception>
        /// <exception cref="ArgumentNullException">path is null.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="DirectoryNotFoundException">
        /// path does not exist or could not be found.
        /// -or-
        /// The specified path is invalid(for example, it is on an unmapped drive).</exception>
        /// <returns>Returns true on success, otherwise false.</returns>
        public bool Drop()
        {
            if (!this.Exists())
                // Has already been deleted.
                return true;

            System.IO.Directory.Delete(this.FullDatabasePath, true);

            return true;
        }

        /// <summary>
        /// Returns the version of this library.
        /// </summary>
        /// <returns>Returns the version of this library.</returns>
        public string Version()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        #region Collection
        /// <summary>
        /// Builds a collection file path.
        /// </summary>
        /// <param name="name">The collection name</param>
        /// <returns>The collection file path.</returns>
        public string GetCollectionPath(string name)
        {
            if (name.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
                throw new Exception(
                    string.Format(
                        "{0} {1} = {2}",
                        Resource.Exceptions.Invalid_characters_for_parameter,
                        nameof(name),
                        name
                    )
                );

            if (name.IndexOf(System.IO.Path.DirectorySeparatorChar) != -1)
                throw new Exception(Resource.Exceptions.Invalid_characters_for_parameter + Environment.NewLine + name);

            return System.IO.Path.Combine(this.FullDatabasePath, name + "." + Database.CollectionFileExtension);
        }

        /// <summary>
        /// Verifies if a collection is already created.
        /// </summary>
        /// <param name="name">The collection name</param>
        /// <returns>true if exists, otherwise false.</returns>
        public bool CollectionExists(string name)
        {
            return System.IO.File.Exists(this.GetCollectionPath(name));
        }

        /// <summary>
        /// Creates a new collection file.
        /// </summary>
        /// <param name="name">The collection name.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission.
        /// -or-
        /// path specified a file that is read-only.
        /// -or-
        /// path specified a file that is hidden.
        /// </exception>
        /// <exception cref="ArgumentException">path is a zero-length string, contains only white space, or contains one or more invalid characters as defined by InvalidPathChars.</exception>
        /// <exception cref="ArgumentNullException">path is null.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="IOException">An I/O error occurred while creating the file.</exception>
        /// <exception cref="NotSupportedException">path is in an invalid format.</exception>
        /// <returns>true on success, otherwise false.</returns>
        public bool CreateCollection(string name)
        {
            if (this.CollectionExists(name))
                throw new Exception(Resource.Exceptions.This_collection_already_exists + Environment.NewLine + name);

            FileStream fs = System.IO.File.Create(this.GetCollectionPath(name));
            fs.Close();

            return true;
        }

        /// <summary>
        /// Delete a collection.
        /// </summary>
        /// <param name="name">Collection name</param>
        /// <exception cref="ArgumentException">path is a zero-length string, contains only white space, or contains one or more invalid characters as defined by InvalidPathChars.</exception>
        /// <exception cref="ArgumentNullException">path is null.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid(for example, it is on an unmapped drive).</exception>
        /// <exception cref="IOException">
        /// The specified file is in use.
        /// -or-
        /// There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.
        /// </exception>
        /// <exception cref="NotSupportedException">path is in an invalid format.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission.
        /// -or-
        /// The file is an executable file that is in use.
        /// -or-
        /// path is a directory.
        /// -or-
        /// path specified a read-only file.
        /// </exception>
        /// <returns>true on success, otherwise false.</returns>
        public bool DropCollection(string name)
        {
            if (!this.CollectionExists(name))
                return true;

            System.IO.File.Delete(this.GetCollectionPath(name));

            return true;
        }

        /// <summary>
        /// Rename a collection.
        /// </summary>
        /// <param name="currentName">The current name</param>
        /// <param name="newName">The new name.</param>
        /// <exception cref="IOException">destFileName already exists.</exception>
        /// <exception cref="FileNotFoundException">sourceFileName was not found.</exception>
        /// <exception cref="ArgumentNullException">sourceFileName or destFileName is null.</exception>
        /// <exception cref="ArgumentException">sourceFileName or destFileName is a zero-length string, contains only white space, or contains invalid characters as defined in InvalidPathChars.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="DirectoryNotFoundException">The path specified in sourceFileName or destFileName is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="NotSupportedException">sourceFileName or destFileName is in an invalid format.</exception>
        /// <returns>true on success, othewise false.</returns>
        public bool RenameCollection(string currentName, string newName)
        {
            if (this.CollectionExists(newName))
                throw new Exception(Resource.Exceptions.This_collection_already_exists + Environment.NewLine + newName);

            if (!this.CollectionExists(currentName))
                throw new Exception(Resource.Exceptions.This_collection_does_not_exists + Environment.NewLine + currentName);

            System.IO.File.Move(this.GetCollectionPath(currentName), this.GetCollectionPath(newName));

            return true;
        }

        /// <summary>
        /// Lists all collections in the current database.
        /// </summary>
        /// <returns>Returns a string array with collection name.</returns>
        public string[] GetCollections()
        {
            return 
                System.IO.Directory
                .GetFiles(this.FullDatabasePath, "*." + Database.CollectionFileExtension)
                .Select(
                    o => o.Replace(this.FullDatabasePath, "")
                          .Replace(Path.DirectorySeparatorChar.ToString(), "")
                          .Replace("." + Database.CollectionFileExtension, "")
                ).ToArray();
        }

        /// <summary>
        /// Selects a collection from current database.
        /// </summary>
        /// <param name="name">The collection name.</param>
        /// <returns>Returns a collection with the specified name from the current database.</returns>
        public IDBCollection GetCollection(string name)
        {
            if (!this.CollectionExists(name))
                throw new Exception(Resource.Exceptions.This_collection_does_not_exists + Environment.NewLine + name);

            return new DBCollection(name, this.GetCollectionPath(name));
        }
        #endregion

        #region View
        /// <summary>
        /// Creates a view.
        /// </summary>
        /// <returns>IViewInfo: the created view info.</returns>
        public IViewInfo CreateView()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a view.
        /// </summary>
        /// <param name="name">View name</param>
        /// <returns>true on success, otherwise false.</returns>
        public bool DropView(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Rename a view.
        /// </summary>
        /// <param name="currentName">The current name</param>
        /// <param name="newName">The new name.</param>
        /// <returns>true on success, othewise false.</returns>
        public bool RenameView(string currentName, string newName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select a view based on its name.
        /// </summary>
        /// <param name="name">View name</param>
        /// <exception cref="EViewNotFound">View has not been found.</exception>
        /// <returns>IView: if exists, otherwise throws an exception.</returns>
        public IView GetView(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns view information for a given view name.
        /// </summary>
        /// <param name="name">The view name.</param>
        /// <exception cref="EViewNotFound">View has not been found.</exception>
        /// <returns>IViewInfo: the view info</returns>
        public IViewInfo GetViewInfo(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lists all views in the current database.
        /// </summary>
        /// <returns>Returns a string array with collection name.</returns>
        public string[] GetViews()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}
