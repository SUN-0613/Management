using Management.Data.File;
using System;
using System.Collections.ObjectModel;

namespace Management.Forms.Model.Clients
{

    /// <summary>
    /// 取引先管理.取引先詳細Model
    /// </summary>
    public class ClientDetail : IDisposable
    {

        /// <summary>
        /// 取引先情報ファイル
        /// </summary>
        private ClientDetailFile _File;

        #region ViewModel.Property

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName
        {
            get { return _File.CompanyName; }
            set
            {
                if (!_File.CompanyName.Equals(value))
                {
                    _File.CompanyName = value;
                }
            }
        }

        /// <summary>
        /// 郵便番号を"-"で分割
        /// </summary>
        public string[] PostalCode;

        /// <summary>
        /// 住所
        /// </summary>
        public string Address
        {
            get { return _File.Address; }
            set
            {
                if (!_File.Address.Equals(value))
                {
                    _File.Address = value;
                }
            }
        }

        /// <summary>
        /// 電話番号を"-"で分割
        /// 市外局番-市内局番-加入者番号
        /// </summary>
        public string[] PhoneNo;

        /// <summary>
        /// 銀行口座
        /// </summary>
        public string BankAccount
        {
            get { return _File.BankAccount; }
            set
            {
                if (!_File.BankAccount.Equals(value))
                {
                    _File.BankAccount = value;
                }
            }
        }

        /// <summary>
        /// 担当差一覧
        /// </summary>
        public ObservableCollection<Staff> Staffs { get; set; }

        #endregion

        /// <summary>
        /// 取引先管理.取引先詳細Model
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        public ClientDetail(string wildName)
        {

            _File = new ClientDetailFile(wildName);

            PostalCode = _File.PostalCode.Split('-');
            PhoneNo = _File.PhoneNo.Split('-');

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_File != null)
            {
                _File.Dispose();
                _File = null;
            }

        }

        /// <summary>
        /// 入力値保存
        /// </summary>
        public void Save()
        {

            // 分割値の結合
            _File.PostalCode = string.Join("-", PostalCode);
            _File.PhoneNo = string.Join("-", PhoneNo);

            // ファイル保存
            _File.Save();

        }

    }

}
