﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LocalNoSql_CSharp.Resource {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Exceptions {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Exceptions() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LocalNoSql_CSharp.Resource.Exceptions", typeof(Exceptions).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Directory not found: .
        /// </summary>
        internal static string Directory_not_found {
            get {
                return ResourceManager.GetString("Directory_not_found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Empty string for parameter: .
        /// </summary>
        internal static string Empty_string_for_parameter {
            get {
                return ResourceManager.GetString("Empty_string_for_parameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid database name..
        /// </summary>
        internal static string Invalid_database_name {
            get {
                return ResourceManager.GetString("Invalid_database_name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid path characters for parameter: .
        /// </summary>
        internal static string Invalid_path_characters_for_parameter {
            get {
                return ResourceManager.GetString("Invalid_path_characters_for_parameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Using root directory to store databases is not allowed. Please create at least one subdirectory in your tree of directories..
        /// </summary>
        internal static string Root_directory_not_allowed {
            get {
                return ResourceManager.GetString("Root_directory_not_allowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This collection already exists..
        /// </summary>
        internal static string This_collection_already_exists {
            get {
                return ResourceManager.GetString("This_collection_already_exists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This collection does not exists..
        /// </summary>
        internal static string This_collection_does_not_exists {
            get {
                return ResourceManager.GetString("This_collection_does_not_exists", resourceCulture);
            }
        }
    }
}
