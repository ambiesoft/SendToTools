﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CreateSizedFile.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CreateSizedFile.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
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
        ///   &quot;{0}&quot; already exists. Do you want to overwrite? に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ALREADY_EXISTS_DO_YOU_WANT_TO_OVERRIDE {
            get {
                return ResourceManager.GetString("ALREADY_EXISTS_DO_YOU_WANT_TO_OVERRIDE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Are you want to create a file with {0} bytes? に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ARE_YOU_GOING_TO_CREATE {
            get {
                return ResourceManager.GetString("ARE_YOU_GOING_TO_CREATE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   File size must not be minus. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string FILESIZE_NOTBE_MINUS {
            get {
                return ResourceManager.GetString("FILESIZE_NOTBE_MINUS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &quot;{0}&quot; does not exist. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string FOLDER_NOT_EXIST {
            get {
                return ResourceManager.GetString("FOLDER_NOT_EXIST", resourceCulture);
            }
        }
        
        /// <summary>
        ///   No arguments. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NO_ARGUMENTS {
            get {
                return ResourceManager.GetString("NO_ARGUMENTS", resourceCulture);
            }
        }
    }
}
