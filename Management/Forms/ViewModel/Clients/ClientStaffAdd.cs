using AYam.Common.MVVM;
using Management.Data.Info;
using Models = Management.Forms.Model.Clients;
using System;

namespace Management.Forms.ViewModel.Clients
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
        private Models.ClientStaffAdd _Model;

        #endregion

        #region Property

        /// <summary>
        /// 名前
        /// </summary>
        public string FirstName
        {
            get { return _Model.Staff.FirstName; }
            set
            {
                if (!_Model.Staff.FirstName.Equals(value))
                {
                    _Model.Staff.FirstName = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 名前：よみがな
        /// </summary>
        public string FirstKana
        {
            get { return _Model.Staff.FirstKana; }
            set
            {
                if (!_Model.Staff.FirstKana.Equals(value))
                {
                    _Model.Staff.FirstKana = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string LastName
        {
            get { return _Model.Staff.LastName; }
            set
            {
                if (!_Model.Staff.LastName.Equals(value))
                {
                    _Model.Staff.LastName = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 名字：よみがな
        /// </summary>
        public string LastKana
        {
            get { return _Model.Staff.LastKana; }
            set
            {
                if (!_Model.Staff.LastKana.Equals(value))
                {
                    _Model.Staff.LastKana = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 部署
        /// </summary>
        public string Department
        {
            get { return _Model.Staff.Department; }
            set
            {
                if (!_Model.Staff.Department.Equals(value))
                {
                    _Model.Staff.Department = value;
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
        /// メモ
        /// </summary>
        public string Remarks
        {
            get { return _Model.Staff.Remarks; }
            set
            {
                if (!_Model.Staff.Remarks.Equals(value))
                {
                    _Model.Staff.Remarks = value;
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
            _Model = new Models.ClientStaffAdd("");
        }

        /// <summary>
        /// 担当者登録.ViewModel
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        /// <param name="staff">編集対象の担当者情報</param>
        public ClientStaffAdd(string wildName, Staff staff = null)
        {
            _Model = new Models.ClientStaffAdd(wildName, staff);
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

        /// <summary>
        /// 担当者情報のクローンを取得
        /// </summary>
        /// <returns>担当者情報</returns>
        public Staff GetStaffClone()
        {
            return (Staff)_Model.Staff.Clone();
        }

    }
}
