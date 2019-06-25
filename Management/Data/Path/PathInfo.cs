using ComMethod = AYam.Common.Method;
using System;

namespace Management.Data.Path
{

    /// <summary>
    /// パス情報
    /// </summary>
    public class PathInfo
    {

        /// <summary>
        /// ワイルド文字
        /// </summary>
        public static readonly string Wild = "*";

        /// <summary>
        /// ファイル一覧
        /// </summary>
        public readonly FileInfo Files;

        /// <summary>
        /// パス情報
        /// </summary>
        public PathInfo()
        {

            Files = new FileInfo();

        }

        #region Class

        /// <summary>
        /// ファイル情報
        /// </summary>
        public class FileInfo
        {

            /// <summary>
            /// マスタ情報
            /// </summary>
            public readonly string Master;

            /// <summary>
            /// 取引先一覧情報
            /// </summary>
            public readonly string Clients;

            /// <summary>
            /// 取引先一覧から選択した取引先の詳細情報
            /// </summary>
            public readonly string ClientDetail;

            /// <summary>
            /// フォルダ一覧
            /// </summary>
            private readonly DirectoryInfo _Directories;

            /// <summary>
            /// ファイル情報
            /// </summary>
            public FileInfo()
            {

                _Directories = new DirectoryInfo();

                Master = ComMethod::Path.GetFullPath(_Directories.MasterPath, "Master.xml");
                Clients = ComMethod::Path.GetFullPath(_Directories.ClientPath, "Clients.xml");
                ClientDetail = ComMethod::Path.GetFullPath(_Directories.ClientPath, "Client_" + Wild + ".xml");

            }

        }

        /// <summary>
        /// フォルダ情報
        /// </summary>
        public class DirectoryInfo
        {

            /// <summary>
            /// ルートフォルダ
            /// </summary>
            public readonly string RootPath;

            /// <summary>
            /// マスタ情報フォルダ
            /// </summary>
            public readonly string MasterPath;

            /// <summary>
            /// 取引先情報フォルダ
            /// </summary>
            public readonly string ClientPath;

            /// <summary>
            /// フォルダ情報
            /// </summary>
            public DirectoryInfo()
            {

                const string parameter = "Parameter";

                // Settingファイルより取得できない場合はexeパス
                if (Properties.Settings.Default.RootPath.Length.Equals(0))
                {
                    RootPath = ComMethod::Path.MakeDirectories(Environment.CurrentDirectory) + parameter;
                }
                else
                {
                    RootPath = ComMethod::Path.MakeDirectories(Properties.Settings.Default.RootPath) + parameter;
                }

                MasterPath = ComMethod::Path.MakeDirectories(RootPath + @"\MasterData");
                ClientPath = ComMethod::Path.MakeDirectories(RootPath + @"\ClientData");

            }

        }

        #endregion

    }

}
