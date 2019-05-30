﻿using AYam.Common.Controls.Interface;
using AYam.Common.MVVM;
using AYam.Common.MVVM.Custom;
using Management.Data.File;
using Management.Forms.Model.Clients;
using System;
using System.Collections.ObjectModel;

namespace Management.Forms.ViewModel
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
        private ClientList _ListModel;

        /// <summary>
        /// 詳細.Model
        /// </summary>
        private ClientDetail _DetailModel;

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
            get { return _DetailModel.CompanyName; }
            set
            {
                if (string.IsNullOrEmpty(_DetailModel.CompanyName) || !_DetailModel.CompanyName.Equals(value))
                {
                    _DetailModel.CompanyName = value;
                }
            }
        }

        /// <summary>
        /// 郵便番号を"-"で分割
        /// </summary>
        public string[] PostalCode
        {
            get { return _DetailModel.PostalCode; }
            set
            {
                if (!_DetailModel.PostalCode.Equals(value))
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
            get { return _DetailModel.Address; }
            set
            {
                if (string.IsNullOrEmpty(_DetailModel.Address) || !_DetailModel.Address.Equals(value))
                {
                    _DetailModel.Address = value;
                }
            }
        }

        /// <summary>
        /// 電話番号を"-"で分割
        /// 市外局番-市内局番-加入者番号
        /// </summary>
        public string[] PhoneNo
        {
            get { return _DetailModel.PhoneNo; }
            set
            {
                if (!_DetailModel.PhoneNo.Equals(value))
                {
                    _DetailModel.PhoneNo = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 銀行口座
        /// </summary>
        public string BankAccount
        {
            get { return _DetailModel.BankAccount; }
            set
            {
                if (string.IsNullOrEmpty(_DetailModel.BankAccount) || !_DetailModel.BankAccount.Equals(value))
                {
                    _DetailModel.BankAccount = value;
                }
            }
        }

        /// <summary>
        /// 担当者一覧
        /// </summary>
        public ObservableCollection<Staff> Staffs
        {
            get { return _DetailModel.Staffs; }
            set { _DetailModel.Staffs = value; }
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
                    () => { return IsEdited; });

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

            _ListModel = new ClientList();

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

            if (SelectedClient != null)
            {

                if (_DetailModel != null)
                {
                    _DetailModel.Dispose();
                    _DetailModel = null;
                }

                _DetailModel = new ClientDetail(SelectedClient.FileWildName);

                // 名称未入力の場合は一覧名称をセット
                if (string.IsNullOrEmpty(CompanyName) || CompanyName.Length.Equals(0))
                {
                    CompanyName = SelectedClient.Name;
                }

                CallPropertyChanged(nameof(CompanyName));

                for (int iLoop = 0; iLoop < PostalCode.Length; iLoop++)
                {
                    CallPropertyChanged(String.Format(nameof(PostalCode) + "[{0}]", iLoop));
                }

                CallPropertyChanged(nameof(Address));

                for (int iLoop = 0; iLoop < PhoneNo.Length; iLoop++)
                {
                    CallPropertyChanged(String.Format(nameof(PhoneNo) + "[{0}]", iLoop));
                }

                CallPropertyChanged(nameof(BankAccount));
                CallPropertyChanged(nameof(Staffs));

            }
            else
            {

                if (_DetailModel != null)
                {
                    _DetailModel.Dispose();
                    _DetailModel = null;
                }

                CompanyName = "";
                CallPropertyChanged(nameof(CompanyName));

                for (int iLoop = 0; iLoop < PostalCode.Length; iLoop++)
                {
                    PostalCode[iLoop] = "";
                    CallPropertyChanged(String.Format(nameof(PostalCode) + "[{0}]", iLoop));
                }

                Address = "";
                CallPropertyChanged(nameof(Address));

                for (int iLoop = 0; iLoop < PhoneNo.Length; iLoop++)
                {
                    PhoneNo[iLoop] = "";
                    CallPropertyChanged(String.Format(nameof(PhoneNo) + "[{0}]", iLoop));
                }

                BankAccount = "";
                CallPropertyChanged(nameof(BankAccount));

                Staffs.Clear();
                CallPropertyChanged(nameof(Staffs));

                ResetEditFlg();

            }

        }

        /// <summary>
        /// 選択中の取引先を取引先一覧より削除
        /// </summary>
        public void RemoveSelectedClient()
        {

            // 一覧より削除
            Clients.Remove(SelectedClient);
            _ListModel.Save();

            // 詳細削除
            _DetailModel.Delete();

            // 初期化
            SelectedClient = null;
            ResetEditFlg();

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

            int index = Staffs.IndexOf(staff);

            if (index > -1)
            {

                Staffs.RemoveAt(index);
                Staffs.Insert(index, staff);

            }

        }

    }

}