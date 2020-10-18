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
        bool Delete();
        #endregion
    }
}
