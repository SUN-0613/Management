using AYam.Common.Controls.Interface;
using AYam.Common.MVVM;
using AYam.Common.MVVM.Custom;
using Management.Data.Info;
using Model = Management.Forms.Model.Clients;
using System;
using System.Collections.ObjectModel;

namespace Management.Forms.ViewModel.Clients
{

    /// <summary>
    /// 取引先管理.ViewModel
    /// </summary>
    public class ClientInfo : EditedViewModelBase, IDisposable, IClosing
    {

        #region Model

        /// <summary>
        /// 一覧.Model
        /// </summary>
        private Model::ClientList _ListModel;

        /// <summary>
        /// 詳細.Model
        /// </summary>
        private Model::ClientDetail _DetailModel;

        #endregion

        #region Property

        /// <summary>
        /// 取引先一覧
        /// </summary>
        public ObservableCollection<Client> Clients
        {
            get { return _ListModel.Clients; }
            set { _ListModel.Clients = value; }
        }

        /// <summary>
        /// 取引先一覧における選択中の取引先
        /// </summary>
        private Client _SelectedClient;

        /// <summary>
        /// 取引先一覧における選択中の取引先
        /// </summary>
        public Client SelectedClient
        {
            get { return _SelectedClient; }
            set
            {
                if (_SelectedClient == null || !_SelectedClient.Equals(value))
                {

                    _SelectedClient = value;
                    CallPropertyChanged();

                    ChangeSelectedClient();

                }
            }
        }

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName
        {
            get
            {
                if (_DetailModel != null)
                {
                    return _DetailModel.CompanyName;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (_DetailModel != null && !_DetailModel.CompanyName.Equals(value))
                {
                    _DetailModel.CompanyName = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 会社名：よみがな
        /// </summary>
        public string CompanyKana
        {
            get
            {
                if (_DetailModel != null)
                {
                    return _DetailModel.CompanyKana;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (_DetailModel != null && !_DetailModel.CompanyKana.Equals(value))
                {
                    _DetailModel.CompanyKana = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 郵便番号を"-"で分割
        /// </summary>
        public string[] PostalCode
        {
            get
            {
                if (_DetailModel != null)
                {
                    return _DetailModel.PostalCode;
                }
                else
                {
                    return new string[] { "", "" };
                }
            }
            set
            {
                if (_DetailModel != null && !_DetailModel.PostalCode.Equals(value))
                {
                    _DetailModel.PostalCode = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 住所
        /// </summary>
        public string Address
        {
            get
            {
                if (_DetailModel != null)
                {
                    return _DetailModel.Address;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (_DetailModel != null && !_DetailModel.Address.Equals(value))
                {
                    _DetailModel.Address = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 電話番号を"-"で分割
        /// 市外局番-市内局番-加入者番号
        /// </summary>
        public string[] PhoneNo
        {
            get
            {
                if (_DetailModel != null)
                {
                    return _DetailModel.PhoneNo;
                }
                else
                {
                    return new string[] { "", "", "" };
                }
            }
            set
            {
                if (_DetailModel != null && !_DetailModel.PhoneNo.Equals(value))
                {
                    _DetailModel.PhoneNo = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// FAX番号を"-"で分割
        /// 市外局番-市内局番-加入者番号
        /// </summary>
        public string[] FaxNo
        {
            get
            {
                if (_DetailModel != null)
                {
                    return _DetailModel.FaxNo;
                }
                else
                {
                    return new string[] { "", "", "" };
                }
            }
            set
            {
                if (_DetailModel != null && !_DetailModel.FaxNo.Equals(value))
                {
                    _DetailModel.FaxNo = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 銀行口座
        /// </summary>
        public string BankAccount
        {
            get
            {
                if (_DetailModel != null)
                {
                    return _DetailModel.BankAccount;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (_DetailModel != null && !_DetailModel.BankAccount.Equals(value))
                {
                    _DetailModel.BankAccount = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 担当者一覧
        /// </summary>
        public ObservableCollection<Staff> Staffs
        {
            get
            {
                if (_DetailModel != null)
                {
                    return _DetailModel.Staffs;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (_DetailModel != null)
                {
                    _DetailModel.Staffs = value;
                }
            }
        }

        /// <summary>
        /// メモ
        /// </summary>
        public string Remarks
        {
            get
            {
                if (_DetailModel != null)
                {
                    return _DetailModel.Remarks;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (_DetailModel != null && !_DetailModel.Remarks.Equals(value))
                {
                    _DetailModel.Remarks = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 担当者一覧における選択中の担当者
        /// </summary>
        private Staff _SelectedStaff;

        public Staff SelectedStaff
        {
            get { return _SelectedStaff; }
            set
            {
                if (_SelectedStaff == null || !_SelectedStaff.Equals(value))
                {
                    _SelectedStaff = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 取引先一覧の操作コマンド
        /// </summary>
        private DelegateCommand<string> _ListCommand;

        /// <summary>
        /// 取引先一覧の操作コマンド
        /// </summary>
        public DelegateCommand<string> ListCommand
        {
            get
            {

                if (_ListCommand == null)
                {

                    _ListCommand = new DelegateCommand<string>
                        (
                            (parameter) => 
                            {

                                switch (parameter)
                                {

                                    case "add":
                                        CallPropertyChanged("CallListAdd");
                                        break;

                                    case "remove":

                                        if (SelectedClient != null)
                                        {
                                            CallPropertyChanged("CallListRemove");
                                        }

                                        break;

                                    default:
                                        break;

                                }

                                _ListModel.Save();

                            }
                            ,() => true
                        );

                }

                return _ListCommand;

            }
        }

        /// <summary>
        /// 担当一覧の操作コマンド
        /// </summary>
        private DelegateCommand<string> _StaffCommand;

        /// <summary>
        /// 取引先一覧の操作コマンド
        /// </summary>
        public DelegateCommand<string> StaffCommand
        {
            get
            {

                if (_StaffCommand == null)
                {

                    _StaffCommand = new DelegateCommand<string>
                        (
                            (parameter) =>
                            {

                                switch (parameter)
                                {

                                    case "add":
                                        CallPropertyChanged("CallStaffAdd");
                                        break;

                                    case "edit":

                                        if (SelectedStaff != null)
                                        {
                                            CallPropertyChanged("CallStaffEdit");
                                        }
                                        
                                        break;

                                    case "remove":

                                        if (SelectedStaff != null)
                                        {
                                            CallPropertyChanged("CallStaffRemove");
                                        }

                                        break;

                                    default:
                                        break;

                                }

                                _ListModel.Save();

                            }
                            , () => true
                        );

                }

                return _StaffCommand;

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

        /// <summary>
        /// 詳細入力可否
        /// </summary>
        public bool IsEnabled { get { return SelectedClient != null; } }

        #endregion

        /// <summary>
        /// 閉じる確認メッセージ.タイトル
        /// </summary>
        public string ClosingTitle { get { return Properties.Title.ClientInfo; } }

        /// <summary>
        /// 閉じる確認メッセージ.文章
        /// </summary>
        public string ClosingMessage { get { return Properties.ClientInfo.MessageClose; } }

        /// <summary>
        /// 取引先管理.ViewModel
        /// </summary>
        public ClientInfo()
        {

            _ListModel = new Model::ClientList();

        }

        /// <summary>
        /// 閉じる処理
        /// </summary>
        /// <returns>
        /// True:閉じる処理続行
        /// False:閉じる処理中止
        /// </returns>
        bool IClosing.OnClosing()
        {

            return !IsEdited;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_ListModel != null)
            {
                _ListModel.Dispose();
                _ListModel = null;
            }

            if (_DetailModel != null)
            {
                _DetailModel.Dispose();
                _DetailModel = null;
            }

        }

        /// <summary>
        /// 詳細保存
        /// </summary>
        public void Save()
        {

            _DetailModel.Save();

            if (!SelectedClient.Name.Equals(CompanyName))
            {

                int index = Clients.IndexOf(SelectedClient);

                if (index > -1)
                {

                    Clients[index].Name = SelectedClient.Name;
                    _ListModel.Save();

                }

            }

            ResetEditFlg();

        }

        /// <summary>
        /// 詳細表示する取引先の変更
        /// </summary>
        private void ChangeSelectedClient()
        {

            if (_DetailModel != null)
            {
                _DetailModel.Dispose();
                _DetailModel = null;
            }

            if (SelectedClient != null)
            {

                _DetailModel = new Model::ClientDetail(SelectedClient);

                // 名称未入力の場合は一覧名称をセット
                if (string.IsNullOrEmpty(CompanyName) || CompanyName.Length.Equals(0))
                {
                    CompanyName = SelectedClient.Name;
                }

            }
            else
            {

                CompanyName = "";
                CompanyKana = "";
                PostalCode = new string[] { "", "" };
                Address = "";
                PhoneNo = new string[] { "", "", "" };
                FaxNo = new string[] { "", "", "" };
                BankAccount = "";

                if (Staffs != null)
                {
                    Staffs.Clear();
                }
                
                Remarks = "";

            }

            CallPropertyChanged(nameof(CompanyName));
            CallPropertyChanged(nameof(CompanyKana));
            CallPropertyChanged(nameof(PostalCode));
            CallPropertyChanged(nameof(Address));
            CallPropertyChanged(nameof(PhoneNo));
            CallPropertyChanged(nameof(FaxNo));
            CallPropertyChanged(nameof(BankAccount));
            CallPropertyChanged(nameof(Staffs));
            CallPropertyChanged(nameof(Remarks));

            CallPropertyChanged(nameof(IsEnabled));

            ResetEditFlg();

        }

        /// <summary>
        /// 選択中の取引先を取引先一覧より削除
        /// </summary>
        public void RemoveSelectedClient()
        {

            // 詳細削除
            _DetailModel.Delete();

            // 一覧より削除
            Clients.Remove(SelectedClient);
            _ListModel.Save();

            // 初期化
            SelectedClient = null;

        }

        /// <summary>
        /// 新規取引先を追加
        /// </summary>
        /// <param name="companyName">会社名</param>
        public void AddClient(string companyName)
        {

            Clients.Add(new Client(companyName, DateTime.Now.ToString("yyyyMMddHHmmss")));
            _ListModel.Save();

        }

        /// <summary>
        /// 選択中の担当者を担当者一覧より削除
        /// </summary>
        public void RemoveSelectedStaff()
        {

            // 一覧より削除
            Staffs.Remove(SelectedStaff);

            // 初期化
            SelectedStaff = null;

        }

        /// <summary>
        /// 新規担当者を追加
        /// </summary>
        /// <param name="staff">担当者情報</param>
        public void AddStaff(Staff staff)
        {

            Staffs.Add(staff);

        }

        /// <summary>
        /// 担当者情報の編集
        /// </summary>
        /// <param name="staff">担当者情報</param>
        public void EditStaff(Staff staff)
        {

            int index = Staffs.IndexOf(SelectedStaff);

            if (index > -1)
            {

                Staffs.RemoveAt(index);
                Staffs.Insert(index, staff);

            }

        }

    }

}
