﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ed.Steamflix.Common {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Settings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Settings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Ed.Steamflix.Common.Settings", typeof(Settings).GetTypeInfo().Assembly);
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
        ///   Looks up a localized string similar to 8FAC8288FB26E59C1468DAD0DFED2683.
        /// </summary>
        internal static string ApiKey {
            get {
                return ResourceManager.GetString("ApiKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.steampowered.com.
        /// </summary>
        internal static string ApiUrl {
            get {
                return ResourceManager.GetString("ApiUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://steamcommunity.com/app/{0}/broadcasts/.
        /// </summary>
        internal static string BroadcastUrlFormat {
            get {
                return ResourceManager.GetString("BroadcastUrlFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://media.steampowered.com/steamcommunity/public/images/apps/{0}/{1}.jpg.
        /// </summary>
        internal static string ImageUrlFormat {
            get {
                return ResourceManager.GetString("ImageUrlFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://store.steampowered.com/stats/.
        /// </summary>
        internal static string StatsUrl {
            get {
                return ResourceManager.GetString("StatsUrl", resourceCulture);
            }
        }
    }
}