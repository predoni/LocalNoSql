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
        /// <returns>Returns true if the database was created successfully or if already exists, otherwise false.</returns>
        bool Create();

        /// <summary>
        /// Delete a database.
        /// </summary>
        /// <returns>Returns true on success, otherwise false.</returns>
        bool Delete();
        #endregion
    }
}
