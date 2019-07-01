using Management.Data.Info;
using System;
using System.Collections.ObjectModel;

namespace Management.Pages.Model.JobList
{

    /// <summary>
    /// ジョブ.Model
    /// </summary>
    public class JobDetail : IDisposable
    {

        /// <summary>
        /// ジョブファイル
        /// </summary>
        private Job _File;

        #region ViewModel.Property

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDate
        {
            get { return _File.CreateDate; }
            set { _File.CreateDate = value; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _File.Name; }
            set { _File.Name = value; }
        }

        /// <summary>
        /// 取引先
        /// </summary>
        public string Client
        {
            get { return _File.Client; }
            set { _File.Client = value; }
        }

        /// <summary>
        /// 見積書
        /// </summary>
        public ObservableCollection<DataFileInfo> Quotations
        {
            get { return _File.Quotations; }
            set { _File.Quotations = value; }
        }

        /// <summary>
        /// 納品書
        /// </summary>
        public ObservableCollection<DataFileInfo> Deliveries
        {
            get { return _File.Deliveries; }
            set { _File.Deliveries = value; }
        }

        /// <summary>
        /// 請求書
        /// </summary>
        public ObservableCollection<DataFileInfo> Invoices
        {
            get { return _File.Invoices; }
            set { _File.Invoices = value; }
        }

        /// <summary>
        /// 封筒・送付状
        /// </summary>
        public DataFileInfo CoverLetter
        {
            get { return _File.CoverLetter; }
            set { _File.CoverLetter = value; }
        }

        /// <summary>
        /// ステータス
        /// </summary>
        public JobStatus Status
        {
            get { return _File.Status; }
            set { _File.Status = value; }
        }

        /// <summary>
        /// 納期
        /// </summary>
        public DateTime DeliveryDate
        {
            get { return _File.DeliveryDate; }
            set { _File.DeliveryDate = value; }
        }

        /// <summary>
        /// 価格
        /// </summary>
        public decimal Price
        {
            get { return _File.Price; }
            set { _File.Price = value; }
        }

        #endregion

        /// <summary>
        /// ジョブ.Model
        /// </summary>
        /// <param name="job">ジョブファイル</param>
        public JobDetail(Job job)
        {
            _File = job;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            _File.Dispose();
            _File = null;

        }

    }

}
