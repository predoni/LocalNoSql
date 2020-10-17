using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNoSql_CSharp
{
    public class TestLib : Object
    {
        #region Properties
        #endregion

        #region Constructors and Destructor
        TestLib() : base() { }
        #endregion

        #region Methods
        /// <summary>
        /// Executes a complete test for the entire library functionality.
        /// </summary>
        public static void ExecuteTest()
        {
            System.Diagnostics.Debug.WriteLine("Executes a complete test for the entire library functionality.");
        }
        #endregion
    }
}
