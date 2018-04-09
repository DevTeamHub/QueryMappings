﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevTeam.QueryMappings.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DevTeam.QueryMappings.Properties.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception has happened on attempt to apply mapping. See the Inner Exception for more details.
        /// </summary>
        public static string ApplyMappingException {
            get {
                return ResourceManager.GetString("ApplyMappingException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Arguments can&apos;t be null for this overload of AsQuery method.
        /// </summary>
        public static string ArgumentsAreRequiredException {
            get {
                return ResourceManager.GetString("ArgumentsAreRequiredException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception has happened during mappings initialization. Please see Inner Exception to find more details..
        /// </summary>
        public static string GeneralInitializationException {
            get {
                return ResourceManager.GetString("GeneralInitializationException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to General exception has happened on attempt to create IMappingStorage instance of type {0}.
        /// </summary>
        public static string GeneralMappingStorageException {
            get {
                return ResourceManager.GetString("GeneralMappingStorageException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid AsQuery method overload used. See Inner Exception for more details.
        /// </summary>
        public static string InvaidMappingCastException {
            get {
                return ResourceManager.GetString("InvaidMappingCastException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mapping from {0} to {1} was not found.
        /// </summary>
        public static string MappingNotFoundException {
            get {
                return ResourceManager.GetString("MappingNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception has happened inside of Setup method invokation on IMappingStorage of type {0}.
        /// </summary>
        public static string MappingStorageSetupException {
            get {
                return ResourceManager.GetString("MappingStorageSetupException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to More than one mapping from {0} to {1} was found. If you have more than one mapping for the same model you can use Named Mappings (see documentation) or create different models for different mappings.
        /// </summary>
        public static string MoreThanOneMappingFoundException {
            get {
                return ResourceManager.GetString("MoreThanOneMappingFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A few mappings from {0} to {1} were found, but &apos;name&apos; argument wasn&apos;t passed into method. Can&apos;t choose correct one. Please pass &apos;name&apos; argument explicitly.
        /// </summary>
        public static string NameIsNullWhenSearchForNamedMappingException {
            get {
                return ResourceManager.GetString("NameIsNullWhenSearchForNamedMappingException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Couldn&apos;t find empty constructor for IMappingStorage implementation: {0}.
        /// </summary>
        public static string NoEmptyConstructorInitializationException {
            get {
                return ResourceManager.GetString("NoEmptyConstructorInitializationException", resourceCulture);
            }
        }
    }
}
