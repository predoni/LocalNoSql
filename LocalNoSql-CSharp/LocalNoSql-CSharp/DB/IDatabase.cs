using LocalNoSql_CSharp.Common;
using LocalNoSql_CSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.DB
{
    public interface IDatabase
    {
        #region Properties
        /// <summary>
        /// The database is a directory!
        /// "RootPath" is the full directory path, where all databases will be created as subdirectories.
        /// "RootPath" will implement the security from the operating system configuration.
        /// </summary>
        string RootPath { get; }

        /// <summary>
        /// The "DatabaseName" is the directory name where all its collections reside.
        /// </summary>
        string DatabaseName { get; }

        /// <summary>
        /// "FullDatabasePath" is the combination between "RootPath" and "DatabaseName".
        /// </summary>
        string FullDatabasePath { get; }

        /// <summary>
        /// The last error object
        /// </summary>
        IError LastError { get; set; }

        /// <summary>
        /// The log level that has been set
        /// </summary>
        LogLevel LogLevel { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Verifies if the current database exists.
        /// </summary>
        /// <returns>Returns true if the database exists, otherwise false.</returns>
        bool Exists();

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
        bool Create();

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
        bool Drop();

        /// <summary>
        /// Returns the version of this library.
        /// </summary>
        /// <returns>Returns the version of this library.</returns>
        string Version();

        #region Collection
        /// <summary>
        /// Builds a collection file path.
        /// </summary>
        /// <param name="name">The collection name</param>
        /// <param name="fileType">The type of the file is collection or index: <see cref="CollectionFileType"/></param>
        /// <returns>The collection file path.</returns>
        string GetCollectionPath(string name, CollectionFileType fileType);

        /// <summary>
        /// Verifies if a collection is already created.
        /// </summary>
        /// <param name="name">The collection name</param>
        /// <returns>true if exists, otherwise false.</returns>
        bool CollectionExists(string name);

        /// <summary>
        /// Creates a new collection file.
        /// </summary>
        /// <param name="name">The collection name.</param>
        /// <returns>true on success, otherwise false.</returns>
        bool CreateCollection(string name);

        /// <summary>
        /// Delete a collection.
        /// </summary>
        /// <param name="name">Collection name</param>
        /// <returns>true on success, otherwise false.</returns>
        bool DropCollection(string name);

        /// <summary>
        /// Rename a collection.
        /// </summary>
        /// <param name="currentName">The current name</param>
        /// <param name="newName">The new name.</param>
        /// <returns>true on success, othewise false.</returns>
        bool RenameCollection(string currentName, string newName);

        /// <summary>
        /// Lists all collections in the current database.
        /// </summary>
        /// <returns>Returns a string array with collection name.</returns>
        string[] GetCollections();

        /// <summary>
        /// Selects a collection from current database.
        /// </summary>
        /// <param name="name">The collection name.</param>
        /// <returns>Returns a collection with the specified name from the current database.</returns>
        IDBCollection GetCollection(string name);
        #endregion

        #region View
        /// <summary>
        /// Creates a view.
        /// </summary>
        /// <returns>IViewInfo: the created view info.</returns>
        IViewInfo CreateView();

        /// <summary>
        /// Delete a view.
        /// </summary>
        /// <param name="name">View name</param>
        /// <returns>true on success, otherwise false.</returns>
        bool DropView(string name);

        /// <summary>
        /// Rename a view.
        /// </summary>
        /// <param name="currentName">The current name</param>
        /// <param name="newName">The new name.</param>
        /// <returns>true on success, othewise false.</returns>
        bool RenameView(string currentName, string newName);

        /// <summary>
        /// Select a view based on its name.
        /// </summary>
        /// <param name="name">View name</param>
        /// <exception cref="EViewNotFound">View has not been found.</exception>
        /// <returns>IView: if exists, otherwise throws an exception.</returns>
        IView GetView(string name);

        /// <summary>
        /// Returns view information for a given view name.
        /// </summary>
        /// <param name="name">The view name.</param>
        /// <exception cref="EViewNotFound">View has not been found.</exception>
        /// <returns>IViewInfo: the view info</returns>
        IViewInfo GetViewInfo(string name);

        /// <summary>
        /// Lists all views in the current database.
        /// </summary>
        /// <returns>Returns a string array with collection name.</returns>
        string[] GetViews();
        #endregion

        #endregion
    }
}
