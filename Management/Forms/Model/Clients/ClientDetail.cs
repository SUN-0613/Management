using Management.Data.File;
using Management.Data.Info;
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
        /// 会社名：よみがな
        /// </summary>
        public string CompanyKana
        {
            get { return _File.CompanyKana; }
            set
            {
                if (!_File.CompanyKana.Equals(value))
                {
                    _File.CompanyKana = value;
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
        /// FAX番号を"-"で分割
        /// 市外局番-市内局番-加入者番号
        /// </summary>
        public string[] FaxNo;

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
        public ObservableCollection<Staff> Staffs
        {
            get { return _File.Staffs; }
            set { _File.Staffs = value; }
        }

        /// <summary>
        /// メモ
        /// </summary>
        public string Remarks
        {
            get { return _File.Remarks; }
            set
            {
                if (!_File.Remarks.Equals(value))
                {
                    _File.Remarks = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// 取引先管理.取引先詳細Model
        /// </summary>
        /// <param name="wildName">取引先情報</param>
        public ClientDetail(Client client)
        {

            _File = new ClientDetailFile(client.FileWildName);

            PostalCode = _File.PostalCode.Split('-');
            PhoneNo = _File.PhoneNo.Split('-');
            FaxNo = _File.FaxNo.Split('-');

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
            _File.FaxNo = string.Join("-", FaxNo);

            // ファイル保存
            _File.Save();

        }

        /// <summary>
        /// 保存ファイルも含め削除
        /// </summary>
        public void Delete()
        {

            _File.Delete();
            Dispose();

        }

    }

}
