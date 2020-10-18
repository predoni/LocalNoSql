using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp.DB
{
    [Serializable]
    public class Database : Object, IDatabase
    {
        #region Properties
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
                        Resource.Exceptions.Invalid_path_characters_for_parameter,
                        nameof(databaseName),
                        databaseName
                    )
                );

            if (databaseName.IndexOf(System.IO.Path.DirectorySeparatorChar) != -1)
                throw new Exception(Resource.Exceptions.Invalid_database_name + Environment.NewLine + databaseName);

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
        public bool Delete()
        {
            if (!this.Exists())
                // Has already been deleted.
                return true;

            System.IO.Directory.Delete(this.FullDatabasePath, true);

            return true;
        }
        #endregion
    }
}
