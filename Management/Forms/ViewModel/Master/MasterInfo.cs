using AYam.Common.Controls.Interface;
using AYam.Common.MVVM;
using AYam.Common.MVVM.Custom;
using System;

namespace Management.Forms.ViewModel.Master
{

    /// <summary>
    /// マスタ管理.ViewModel
    /// </summary>
    public class MasterInfo : EditedViewModelBase, IDisposable, IClosing
    {

        #region Model

        /// <summary>
        /// Model
        /// </summary>
        private Model.Master.MasterInfo _Model;

        #endregion

        #region Property

        /// <summary>
        /// 名前
        /// </summary>
        public string FirstName
        {
            get { return _Model.FirstName; }
            set
            {
                if (!_Model.FirstName.Equals(value))
                {
                    _Model.FirstName = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 名前：よみがな
        /// </summary>
        public string FirstKana
        {
            get { return _Model.FirstKana; }
            set
            {
                if (!_Model.FirstKana.Equals(value))
                {
                    _Model.FirstKana = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string LastName
        {
            get { return _Model.LastName; }
            set
            {
                if (!_Model.LastName.Equals(value))
                {
                    _Model.LastName = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 名字：よみがな
        /// </summary>
        public string LastKana
        {
            get { return _Model.LastKana; }
            set
            {
                if (!_Model.LastKana.Equals(value))
                {
                    _Model.LastKana = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 郵便番号
        /// </summary>
        public string[] PostalCode
        {
            get { return _Model.PostalCode; }
            set
            {
                if (!_Model.PostalCode.Equals(value))
                {
                    _Model.PostalCode = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 住所
        /// </summary>
        public string Address
        {
            get { return _Model.Address; }
            set
            {
                if (!_Model.Address.Equals(value))
                {
                    _Model.Address = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string[] PhoneNo
        {
            get { return _Model.PhoneNo; }
            set
            {
                if (!_Model.PhoneNo.Equals(value))
                {
                    _Model.PhoneNo = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// FAX番号
        /// </summary>
        public string[] FaxNo
        {
            get { return _Model.FaxNo; }
            set
            {
                if (!_Model.FaxNo.Equals(value))
                {
                    _Model.FaxNo = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string EMailAddress
        {
            get { return _Model.EMailAddress; }
            set
            {
                if (!_Model.EMailAddress.Equals(value))
                {
                    _Model.EMailAddress = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 銀行口座
        /// </summary>
        public string BankAccount
        {
            get { return _Model.BankAccount; }
            set
            {
                if (!_Model.BankAccount.Equals(value))
                {
                    _Model.BankAccount = value;
                    CallPropertyChanged();
                }
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
                    ()=> 
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
        public string ClosingTitle { get { return Properties.Title.MasterInfo; } }

        /// <summary>
        /// 閉じる確認メッセージ.文章
        /// </summary>
        public string ClosingMessage { get { return Properties.MasterInfo.MessageClose; } }

        /// <summary>
        /// マスタ管理.ViewModel
        /// </summary>
        public MasterInfo()
        {

            _Model = new Model.Master.MasterInfo();

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

            if (_Model != null)
            {
                _Model.Dispose();
                _Model = null;
            }

        }

        /// <summary>
        /// データ保存
        /// </summary>
        public void Save()
        {
            _Model.Save();
            ResetEditFlg();
        }

    }

}
