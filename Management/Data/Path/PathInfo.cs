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
            /// フォルダ一覧
            /// </summary>
            private readonly DirectoryInfo _Directories;

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
            /// ジョブ一覧情報
            /// </summary>
            public readonly string Jobs;

            /// <summary>
            /// 見積書情報
            /// </summary>
            public readonly string Quotation;

            /// <summary>
            /// 納品書情報
            /// </summary>
            public readonly string Delivery;

            /// <summary>
            /// 請求書情報
            /// </summary>
            public readonly string Invoice;

            /// <summary>
            /// 封筒・送付状情報
            /// </summary>
            public readonly string CoverLetter;

            /// <summary>
            /// ファイル情報
            /// </summary>
            public FileInfo()
            {

                _Directories = new DirectoryInfo();

                Master = ComMethod::Path.GetFullPath(_Directories.MasterPath, "Master.xml");

                Clients = ComMethod::Path.GetFullPath(_Directories.ClientPath, "Clients.xml");
                ClientDetail = ComMethod::Path.GetFullPath(_Directories.ClientPath, "Client_" + Wild + ".xml");

                Jobs = ComMethod::Path.GetFullPath(_Directories.JobPath, "Jobs.xml");
                Quotation = ComMethod::Path.GetFullPath(_Directories.QuotationPath, "Quotation_" + Wild + ".xml");
                Delivery = ComMethod::Path.GetFullPath(_Directories.DeliveryPath, "Delivery_" + Wild + ".xml");
                Invoice = ComMethod::Path.GetFullPath(_Directories.InvoicePath, "Invoice_" + Wild + ".xml");
                CoverLetter = ComMethod::Path.GetFullPath(_Directories.CoverLetterPath, "CoverLetter_" + Wild + ".xml");

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
            /// ジョブ情報フォルダ
            /// </summary>
            public readonly string JobPath;

            /// <summary>
            /// 見積書フォルダ
            /// </summary>
            public readonly string QuotationPath;

            /// <summary>
            /// 納品書フォルダ
            /// </summary>
            public readonly string DeliveryPath;

            /// <summary>
            /// 請求書フォルダ
            /// </summary>
            public readonly string InvoicePath;

            /// <summary>
            /// 封筒・送付状フォルダ
            /// </summary>
            public readonly string CoverLetterPath;

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

                string jobPath = RootPath + @"\JobData";
                JobPath = ComMethod::Path.MakeDirectories(jobPath);
                QuotationPath = ComMethod::Path.MakeDirectories(jobPath + @"\Quotation");
                DeliveryPath = ComMethod::Path.MakeDirectories(jobPath + @"\Delivery");
                InvoicePath = ComMethod::Path.MakeDirectories(jobPath + @"\Invoice");
                CoverLetterPath = ComMethod::Path.MakeDirectories(jobPath + @"\CoverLetter");

            }

        }

        #endregion

    }

}
