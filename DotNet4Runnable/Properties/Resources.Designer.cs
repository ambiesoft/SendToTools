﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DotNet4Runnable.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DotNet4Runnable.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
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
        ///   &lt;configuration&gt;
        ///  &lt;startup useLegacyV2RuntimeActivationPolicy=&quot;true&quot;&gt;
        ///    &lt;supportedRuntime version=&quot;v4.0&quot; /&gt;
        ///    &lt;supportedRuntime version=&quot;v2.0.50727&quot; /&gt;
        ///  &lt;/startup&gt;
        ///&lt;/configuration&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string configstring {
            get {
                return ResourceManager.GetString("configstring", resourceCulture);
            }
        }
        
        /// <summary>
        ///   v4 is already set as the first entry of &lt;startup&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DN4ISSET {
            get {
                return ResourceManager.GetString("DN4ISSET", resourceCulture);
            }
        }
        
        /// <summary>
        ///   More than one arguments or unknown options are provided. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string MORETHAN_ONEARG {
            get {
                return ResourceManager.GetString("MORETHAN_ONEARG", resourceCulture);
            }
        }
        
        /// <summary>
        ///   New supportedRuntime are added. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NEWLYADDED {
            get {
                return ResourceManager.GetString("NEWLYADDED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &apos;{0}&apos; is created. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NEWLYCREATED {
            get {
                return ResourceManager.GetString("NEWLYCREATED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   No Arguments に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NO_ARGUMENTS {
            get {
                return ResourceManager.GetString("NO_ARGUMENTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &apos;{0}&apos; is not a config file. The root node is not &lt;configuration&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NOTCONFIGFILE {
            get {
                return ResourceManager.GetString("NOTCONFIGFILE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &apos;{0}&apos; is not an executable file. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NOTEXECUTABLE {
            get {
                return ResourceManager.GetString("NOTEXECUTABLE", resourceCulture);
            }
        }
    }
}
