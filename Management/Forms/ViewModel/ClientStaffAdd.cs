using AYam.Common.MVVM;
using Management.Data.File;
using System;

namespace Management.Forms.ViewModel
{

    /// <summary>
    /// 担当者登録.ViewModel
    /// </summary>
    public class ClientStaffAdd : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// Model
        /// </summary>
        private Model.Clients.ClientStaffAdd _Model;

        #endregion

        #region Property

        /// <summary>
        /// 氏名
        /// </summary>
        public string Name
        {
            get { return _Model.Staff.Name; }
            set
            {
                if (!_Model.Staff.Name.Equals(value))
                {
                    _Model.Staff.Name = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 振り仮名
        /// </summary>
        public string Phonetic
        {
            get { return _Model.Staff.Phonetic; }
            set
            {
                if (!_Model.Staff.Phonetic.Equals(value))
                {
                    _Model.Staff.Phonetic = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string EMailAddress
        {
            get { return _Model.Staff.EMailAddress; }
            set
            {
                if (!_Model.Staff.EMailAddress.Equals(value))
                {
                    _Model.Staff.EMailAddress = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 携帯電話番号
        /// </summary>
        public string MobilePhone
        {
            get { return _Model.Staff.MobilePhone; }
            set
            {
                if (!_Model.Staff.MobilePhone.Equals(value))
                {
                    _Model.Staff.MobilePhone = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime CreateDate { get { return _Model.Staff.CreateDate; } }

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
        /// 担当者登録.ViewModel
        /// </summary>
        public ClientStaffAdd()
        {
            _Model = new Model.Clients.ClientStaffAdd("");
        }

        /// <summary>
        /// 担当者登録.ViewModel
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        public ClientStaffAdd(string wildName)
        {
            _Model = new Model.Clients.ClientStaffAdd(wildName);
        }

        /// <summary>
        /// 担当者登録.ViewModel
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        /// <param name="staff">編集対象の担当者情報</param>
        public ClientStaffAdd(string wildName, Staff staff)
        {
            _Model = new Model.Clients.ClientStaffAdd(wildName, staff.Name, staff.Phonetic, staff.EMailAddress, staff.MobilePhone, staff.CreateDate);
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
        /// 入力した会社名が取引先一覧に登録済みかチェック
        /// </summary>
        /// <returns>
        /// True:登録済み
        /// False:未登録
        /// </returns>
        public bool Contains()
        {
            return _Model.Contains();
        }

    }
}
