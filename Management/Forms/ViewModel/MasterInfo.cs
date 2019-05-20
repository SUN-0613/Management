using AYam.Common.Controls.Interface;
using AYam.Common.MVVM;
using AYam.Common.MVVM.Custom;
using System;

namespace Management.Forms.ViewModel
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
        /// 氏名
        /// </summary>
        public string Name
        {
            get { return _Model.Name; }
            set
            {
                if (!_Model.Name.Equals(value))
                {
                    _Model.Name = value;
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
        /// 閉じる処理OK
        /// </summary>
        public bool IsClose { private get; set; } = false;

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
        /// True:閉じる処理中止
        /// False:閉じる処理続行
        /// </returns>
        bool IClosing.OnClosing()
        {

            if (IsClose || !IsEdited)
            {
                return false;
            }
            else
            {
                CallPropertyChanged("CallClose");
                return true;
            }

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
