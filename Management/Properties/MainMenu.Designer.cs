﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Management.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MainMenu {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MainMenu() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Management.Properties.MainMenu", typeof(MainMenu).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   取引先情報 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string ClientInfo {
            get {
                return ResourceManager.GetString("ClientInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   ファイル(_F) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string File {
            get {
                return ResourceManager.GetString("File", resourceCulture);
            }
        }
        
        /// <summary>
        ///   マスタ情報 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string MasterInfo {
            get {
                return ResourceManager.GetString("MasterInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   マスタ管理(_M) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string MasterOption {
            get {
                return ResourceManager.GetString("MasterOption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   保存パスの設定(_S) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string Path {
            get {
                return ResourceManager.GetString("Path", resourceCulture);
            }
        }
        
        /// <summary>
        ///   常用処理(_O) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string Process {
            get {
                return ResourceManager.GetString("Process", resourceCulture);
            }
        }
        
        /// <summary>
        ///   終了(_X) に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string Quit {
            get {
                return ResourceManager.GetString("Quit", resourceCulture);
            }
        }
    }
}