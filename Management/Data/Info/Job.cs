using System;
using System.Collections.ObjectModel;

namespace Management.Data.Info
{

    /// <summary>
    /// Job情報
    /// </summary>
    public class Job : IDisposable
    {

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 取引先
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// 見積書
        /// </summary>
        public ObservableCollection<DataFileInfo> Quotations { get; set; }

        /// <summary>
        /// 納品書
        /// </summary>
        public ObservableCollection<DataFileInfo> Deliveries { get; set; }

        /// <summary>
        /// 請求書
        /// </summary>
        public ObservableCollection<DataFileInfo> Invoices { get; set; }

        /// <summary>
        /// 封筒・送付状
        /// </summary>
        public DataFileInfo CoverLetter { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        public JobStatus Status { get; set; }

        /// <summary>
        /// 納期
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// 価格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Job情報
        /// </summary>
        public Job()
        {

            Quotations = new ObservableCollection<DataFileInfo>();
            Deliveries = new ObservableCollection<DataFileInfo>();
            Invoices = new ObservableCollection<DataFileInfo>();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Quotations != null)
            {
                Quotations.Clear();
                Quotations = null;
            }

            if (Deliveries != null)
            {
                Deliveries.Clear();
                Deliveries = null;
            }

            if (Invoices != null)
            {
                Invoices.Clear();
                Invoices = null;
            }

        }

        /// <summary>
        /// 引数データが同一かチェック
        /// </summary>
        /// <param name="obj">引数データ</param>
        public override bool Equals(object obj)
        {
            
            if (obj is Job job)
            {
                return CreateDate.Equals(job.CreateDate);
            }
            else
            {
                return false;
            }

        }

    }

}
