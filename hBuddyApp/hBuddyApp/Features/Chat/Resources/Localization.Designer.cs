﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hBuddyApp.Features.Chat.Resources {
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
    internal class Localization {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Localization() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("hBuddyApp.Features.Chat.Resources.Localization", typeof(Localization).Assembly);
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
        ///   Looks up a localized string similar to Ok.
        /// </summary>
        internal static string Chat_Dialog_AcceptBtn {
            get {
                return ResourceManager.GetString("Chat_Dialog_AcceptBtn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to At first select country/program..
        /// </summary>
        internal static string Chat_Dialog_DescriptionText {
            get {
                return ResourceManager.GetString("Chat_Dialog_DescriptionText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Info.
        /// </summary>
        internal static string Chat_Dialog_TitleText {
            get {
                return ResourceManager.GetString("Chat_Dialog_TitleText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter text here....
        /// </summary>
        internal static string Chat_Input_Placeholder_Text {
            get {
                return ResourceManager.GetString("Chat_Input_Placeholder_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CHATBOT.
        /// </summary>
        internal static string Chat_Title_Text {
            get {
                return ResourceManager.GetString("Chat_Title_Text", resourceCulture);
            }
        }
    }
}