using Management.Data.File;
using System;

namespace Management.Forms.Model.Master
{

    /// <summary>
    /// マスタ管理.Model
    /// </summary>
    public class MasterInfo : IDisposable
    {

        /// <summary>
        /// マスタ情報ファイル
        /// </summary>
        private MasterFile _File;

        #region ViewModel.Property

        /// <summary>
        /// 名前
        /// </summary>
        public string FirstName
        {
            get { return _File.FirstName; }
            set
            {
                if (!_File.FirstName.Equals(value))
                {
                    _File.FirstName = value;
                }
            }
        }

        /// <summary>
        /// 名前：よみがな
        /// </summary>
        public string FirstKana
        {
            get { return _File.FirstKana; }
            set
            {
                if (!_File.FirstKana.Equals(value))
                {
                    _File.FirstKana = value;
                }
            }
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string LastName
        {
            get { return _File.LastName; }
            set
            {
                if (!_File.LastName.Equals(value))
                {
                    _File.LastName = value;
                }
            }
        }

        /// <summary>
        /// 名字：よみがな
        /// </summary>
        public string LastKana
        {
            get { return _File.LastKana; }
            set
            {
                if (!_File.LastKana.Equals(value))
                {
                    _File.LastKana = value;
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
        /// メールアドレス
        /// </summary>
        public string EMailAddress
        {
            get { return _File.EMailAddress; }
            set
            {
                if (!_File.EMailAddress.Equals(value))
                {
                    _File.EMailAddress = value;
                }
            }
        }

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

        #endregion

        /// <summary>
        /// マスタ管理.Model
        /// </summary>
        public MasterInfo()
        {

            _File = new MasterFile();

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

    }

}
