using Management.Data.File;
using Management.Data.Info;
using System;
using System.Collections.ObjectModel;

namespace Management.Pages.Model.JobList
{

    /// <summary>
    /// ジョブ詳細.Model
    /// </summary>
    public class JobDetail : IDisposable
    {

        #region File

        /// <summary>
        /// ジョブファイル
        /// </summary>
        private Job _JobFile;

        /// <summary>
        /// 取引先一覧
        /// </summary>
        private ClientsFile _ClientsFile;

        #endregion

        #region ViewModel.Property

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDate
        {
            get { return _JobFile.CreateDate; }
            set { _JobFile.CreateDate = value; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _JobFile.Name; }
            set { _JobFile.Name = value; }
        }

        /// <summary>
        /// 取引先一覧
        /// </summary>
        public ObservableCollection<Client> Clients
        {
            get { return _ClientsFile.Clients; }
            set { _ClientsFile.Clients = value; }
        }

        /// <summary>
        /// 選択している取引先
        /// </summary>
        public Client SelectedClient
        {
            get { return _SelectedClient; }
            set
            {
                _SelectedClient = value;
                _JobFile.Client = _SelectedClient.Name;
            }
        }

        /// <summary>
        /// 見積書
        /// </summary>
        public ObservableCollection<DataFileInfo> Quotations
        {
            get { return _JobFile.Quotations; }
            set { _JobFile.Quotations = value; }
        }

        /// <summary>
        /// 納品書
        /// </summary>
        public ObservableCollection<DataFileInfo> Deliveries
        {
            get { return _JobFile.Deliveries; }
            set { _JobFile.Deliveries = value; }
        }

        /// <summary>
        /// 請求書
        /// </summary>
        public ObservableCollection<DataFileInfo> Invoices
        {
            get { return _JobFile.Invoices; }
            set { _JobFile.Invoices = value; }
        }

        /// <summary>
        /// 封筒・送付状
        /// </summary>
        public DataFileInfo CoverLetter
        {
            get { return _JobFile.CoverLetter; }
            set { _JobFile.CoverLetter = value; }
        }

        /// <summary>
        /// ステータス
        /// </summary>
        public JobStatus Status
        {
            get { return _JobFile.Status; }
            set { _JobFile.Status = value; }
        }

        /// <summary>
        /// 納期
        /// </summary>
        public DateTime DeliveryDate
        {
            get { return _JobFile.DeliveryDate; }
            set { _JobFile.DeliveryDate = value; }
        }

        /// <summary>
        /// 価格
        /// </summary>
        public decimal Price
        {
            get { return _JobFile.Price; }
            set { _JobFile.Price = value; }
        }

        #endregion

        /// <summary>
        /// 選択している取引先
        /// </summary>
        private Client _SelectedClient = null;

        /// <summary>
        /// ジョブ.Model
        /// </summary>
        /// <param name="job">ジョブファイル</param>
        public JobDetail(Job job)
        {

            _JobFile = job;
            _ClientsFile = new ClientsFile();

            // 保存されている取引先を取得し、選択中にする
            for (int iLoop = 0; iLoop < _ClientsFile.Clients.Count; iLoop++)
            {
                if (_ClientsFile.Clients[iLoop].Name.Equals(_JobFile.Client))
                {
                    _SelectedClient = _ClientsFile.Clients[iLoop];
                    break;
                }
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            _JobFile.Dispose();
            _JobFile = null;

            if (_ClientsFile != null)
            {
                _ClientsFile.Dispose();
                _ClientsFile = null;
            }

        }

        /// <summary>
        /// ObservableCollection<DataFileInfo>()のカウントにてファイル作成状態を取得
        /// </summary>
        /// <param name="listCount">
        /// ObservableCollection<DataFileInfo>()のカウント
        /// </param>
        /// <returns>"作成済"または"未作成"</returns>
        public string FileExist(int listCount)
        {
            if (listCount.Equals(0))
            {
                return Properties.JobList.FileNoExist;
            }
            else
            {
                return Properties.JobList.FileExist;
            }
        }

    }

}
