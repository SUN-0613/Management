using AYam.Common.MVVM;
using Management.Data.Info;
using Model = Management.Pages.Model.JobList;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Management.Pages.ViewModel.JobList
{

    /// <summary>
    /// ジョブ詳細.ViewModel
    /// </summary>
    public class JobDetail : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// ジョブ詳細.Model
        /// </summary>
        private Model::JobDetail _Model;

        #endregion

        #region Property

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDate
        {
            get { return _Model?.CreateDate ?? DateTime.Now; }
            set
            {
                if (_Model != null && !_Model.CreateDate.Equals(value))
                {
                    _Model.CreateDate = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Model?.Name ?? ""; }
            set
            {
                if (_Model != null && !_Model.Name.Equals(value))
                {
                    _Model.Name = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 取引先一覧
        /// </summary>
        public ObservableCollection<Client> Clients
        {
            get { return _Model?.Clients; }
            set
            {
                if (_Model != null)
                {
                    _Model.Clients = value;
                }
            }
        }

        /// <summary>
        /// 選択している取引先
        /// </summary>
        public Client SelectedClient
        {
            get { return _Model?.SelectedClient; }
            set
            {
                if (_Model != null)
                {
                    _Model.SelectedClient = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 選択している取引先の名前
        /// </summary>
        public string SelectedClientName
        {
            get { return _Model?.SelectedClient.Name ?? ""; }
        }

        /// <summary>
        /// 見積書
        /// </summary>
        public ObservableCollection<DataFileInfo> Quotations
        {
            get { return _Model?.Quotations; }
            set
            {
                if (_Model != null)
                {
                    _Model.Quotations = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 見積書は作成済か
        /// </summary>
        public string FileExistQuotation
        {
            get { return _Model?.FileExist(_Model.Quotations.Count) ?? Properties.JobList.FileNoExist; }
        }

        /// <summary>
        /// 納品書
        /// </summary>
        public ObservableCollection<DataFileInfo> Deliveries
        {
            get { return _Model?.Deliveries; }
            set
            {
                if (_Model != null)
                {
                    _Model.Deliveries = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 納品書は作成済か
        /// </summary>
        public string FileExistDelivery
        {
            get { return _Model?.FileExist(_Model.Deliveries.Count) ?? Properties.JobList.FileNoExist; }
        }

        /// <summary>
        /// 請求書
        /// </summary>
        public ObservableCollection<DataFileInfo> Invoices
        {
            get { return _Model?.Invoices; }
            set
            {
                if (_Model != null)
                {
                    _Model.Invoices = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 請求書は作成済か
        /// </summary>
        public string FileExistInvoice
        {
            get { return _Model?.FileExist(_Model.Invoices.Count) ?? Properties.JobList.FileNoExist; }
        }

        /// <summary>
        /// 封筒・送付状
        /// </summary>
        public DataFileInfo CoverLetter
        {
            get { return _Model?.CoverLetter; }
            set
            {
                if (_Model != null)
                {
                    _Model.CoverLetter = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 封筒・送付状は作成済か
        /// </summary>
        public string FileExistCoverLetter
        {
            get
            {
                if (_Model?.CoverLetter.CreatedFlg ?? true)
                {
                    return Properties.JobList.FileExist;
                }
                else
                {
                    return Properties.JobList.FileNoExist;
                }
            }
        }

        /// <summary>
        /// ステータス
        /// </summary>
        public JobStatus Status
        {
            get { return _Model?.Status; }
            set
            {
                if (_Model != null)
                {
                    _Model.Status = value;
                    CallPropertyChanged();
                    CallPropertyChanged(nameof(DeliveryInfo));
                }
            }
        }

        /// <summary>
        /// 納期
        /// </summary>
        public string DeliveryInfo
        {
            get
            {
                switch (_Model?.Status.Status ?? JobStatus.StatusEnum.None)
                {

                    case JobStatus.StatusEnum.NotOrdered:
                        return _Model.Status.Deadline;

                    case JobStatus.StatusEnum.Ordered:
                        return _Model.Status.DeliveryDate.ToString("yyyy/MM/dd");

                    case JobStatus.StatusEnum.Delivery:
                        return _Model.Status.DepositDate.ToString("yyyy/MM/dd");
                        
                    default:
                        return "-";
                }

            }
        }

        /// <summary>
        /// 価格
        /// </summary>
        public decimal Price
        {
            get { return _Model?.Price ?? 0; }
            set
            {
                if (_Model != null && !_Model.Price.Equals(value))
                {
                    _Model.Price = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 行背景色
        /// </summary>
        public Brush LineBackground
        {
            get
            {
                switch (_Model?.Status.Status ?? JobStatus.StatusEnum.None)
                {

                    case JobStatus.StatusEnum.Ordered:
                        return Brushes.LightYellow;

                    case JobStatus.StatusEnum.Delivery:
                        return Brushes.LightGreen;

                    case JobStatus.StatusEnum.Finished:
                        return Brushes.LightGray;

                    default:
                        return Brushes.White;

                }
            }
        }

        /// <summary>
        /// 詳細コマンド
        /// 一覧ページ用
        /// </summary>
        public DelegateCommand DetailCommand
        {
            get
            {

                return new DelegateCommand(
                    () => { CallPropertyChanged("CallDetail"); },
                    () => true);

            }
        }
        
        /// <summary>
        /// 書類関係コマンド
        /// </summary>
        public DelegateCommand<string> DocumentCommand
        {
            get
            {
                return new DelegateCommand<string>
                    (
                        (parameter) => 
                        {

                            switch (parameter)
                            {

                                case "openQuotation":       // 見積書を開く
                                    CallPropertyChanged("CallOpenQuotation");
                                    break;

                                case "revisionQuotation":   // 見積書の改訂
                                    CallPropertyChanged("CallRevisionQuotation");
                                    break;

                                case "openDelivery":        // 納品書を開く
                                    CallPropertyChanged("CallOpenDelivery");
                                    break;

                                case "revisionDelivery":    // 納品書の改訂
                                    CallPropertyChanged("CallRevisionDelivery");
                                    break;

                                case "openInvoice":         // 請求書を開く
                                    CallPropertyChanged("CallOpenInvoice");
                                    break;

                                case "revisionInvoice":     // 請求書の改訂
                                    CallPropertyChanged("CallRevisionInvoice");
                                    break;

                                case "openCoverLetter":     // 封筒・送付状を開く
                                    CallPropertyChanged("CallOpenCoverLetter");
                                    break;

                                default:
                                    break;

                            }

                        },
                        () => true
                    );
            }
        }

        /// <summary>
        /// 保存コマンド
        /// </summary>
        public DelegateCommand SaveCommand
        {
            get
            {

                return new DelegateCommand(
                    () => { CallPropertyChanged("CallSave"); },
                    () => true);

            }
        }

        /// <summary>
        /// 取消コマンド
        /// </summary>
        public DelegateCommand CancelCommand
        {
            get
            {

                return new DelegateCommand(
                    () =>
                    {
                        CallPropertyChanged("CallClose");
                    },
                    () => true);

            }
        }

        #endregion

        /// <summary>
        /// ジョブ.ViewModel
        /// </summary>
        public JobDetail()
        { }

        /// <summary>
        /// ジョブ.ViewModel
        /// </summary>
        /// <param name="job">ジョブファイル</param>
        public JobDetail(Job job)
        {
            _Model = new Model::JobDetail(job);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Model != null)
            {
                _Model.Dispose();
                _Model = null;
            }

        }

    }

}
