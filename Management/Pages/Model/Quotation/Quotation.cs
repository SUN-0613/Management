using AYam.Common.Controls.Print;
using Management.Data.File;
using Management.Data.Info;
using View = Management.Pages.View.Quotation;
using System.Collections.ObjectModel;

namespace Management.Pages.Model.Quotation
{

    /// <summary>
    /// 見積書.Model
    /// </summary>
    public class Quotation : PrintPageBase
    {

        #region File

        /// <summary>
        /// 見積書ファイル
        /// </summary>
        public QuotationFile File;

        /// <summary>
        /// 見積情報
        /// </summary>
        public DataFileInfo DataFile { get; private set; }

        #endregion

        #region ViewModel.Property

        /// <summary>
        /// 印刷モード
        /// </summary>
        public bool IsPrintMode = false;

        /// <summary>
        /// 1ページ毎の表示摘要件数
        /// </summary>
        private const int PageLineCount = 10;

        /// <summary>
        /// 現在ページ
        /// </summary>
        public int NowPage;

        /// <summary>
        /// 最大ページ数
        /// </summary>
        public int MaxPage;

        /// <summary>
        /// 印刷用摘要一覧
        /// </summary>
        public ObservableCollection<QuoteSummary> PrintSummaries { get; set; }

        #endregion

        /// <summary>
        /// 見積書.Model
        /// </summary>
        /// <param name="dataFile">見積情報</param>
        public Quotation(DataFileInfo dataFile)
        {

            DataFile = dataFile;
            File = new QuotationFile(DataFile);

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Dispose()
        {

            base.Dispose();

            if (PrintSummaries != null)
            {

                foreach (var summary in PrintSummaries)
                {
                    summary.Dispose();
                }

                PrintSummaries.Clear();
                PrintSummaries = null;

            }

            File.Dispose();
            File = null;

        }

        /// <summary>
        /// 見積書ファイルを保存
        /// </summary>
        public void Save()
        {
            File.Save();
        }

        /// <summary>
        /// 見積書ファイルを削除
        /// </summary>
        public void Delete()
        {
            File.Delete();
        }

        /// <summary>
        /// 取引先担当者の登録
        /// </summary>
        /// <param name="staffs">取引先担当者一覧</param>
        public void SetClientStaffs(ObservableCollection<Staff> staffs)
        {

            File.ClientStaffs.Clear();

            foreach (var staff in staffs)
            {
                File.ClientStaffs.Add((Staff)staff.Clone());
            }

        }

        /// <summary>
        /// 納期情報を設定
        /// </summary>
        /// <param name="no">数日、週数、月数</param>
        /// <param name="unit">
        /// 単位指定
        /// 日、週、月
        /// </param>
        public void SetDeliveryDate(double no, DeliveryUnit unit)
        {

            File.DeliveryDate.No = no;
            File.DeliveryDate.Unit = unit;
            
        }

        /// <summary>
        /// 印刷範囲の摘要を抽出
        /// </summary>
        public void SetPrintSummaries()
        {

            if (IsPrintMode)
            {

                PrintSummaries = new ObservableCollection<QuoteSummary>();

                for (int iLoop = (NowPage - 1) * PageLineCount; iLoop < NowPage * PageLineCount; iLoop++)
                {

                    if (iLoop >= File.Summaries.Count)
                    {
                        PrintSummaries.Add(new QuoteSummary());
                    }
                    else
                    {
                        PrintSummaries.Add(File.Summaries[iLoop]);

                    }

                }

            }

        }

        /// <summary>
        /// 印刷実行
        /// </summary>
        public async void ExecutePrint()
        {

            // ページ数の計算
            if (File.Summaries.Count % PageLineCount == 0)
            {
                MaxPage = File.Summaries.Count / PageLineCount;
            }
            else
            {
                MaxPage = File.Summaries.Count / PageLineCount + 1;
            }

            // ページ毎に印刷データを作成
            for (int pageNo = 1; pageNo <= MaxPage; pageNo++)
            {

                PrintPages.Add(new View::Quotation(DataFile, pageNo, MaxPage));

            }

            // 印刷実行
            await Print();

        }

    }
}
