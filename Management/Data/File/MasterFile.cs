using System;

namespace Management.Data.File
{

    /// <summary>
    /// マスタ情報ファイル
    /// </summary>
    public class MasterFile : Base.DataFile, IDisposable
    {

        /// <summary>
        /// 氏名
        /// </summary>
        public string Name;

        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostalCode;

        /// <summary>
        /// 住所
        /// </summary>
        public string Address;

        /// <summary>
        /// 電話番号
        /// </summary>
        public string PhoneNo;

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string EMailAddress;

        /// <summary>
        /// 銀行口座
        /// </summary>
        public string BankAccount;

        /// <summary>
        /// マスタ情報ファイル
        /// </summary>
        public MasterFile() : base(new PathInfo().Files.Master)
        {

            Name = GetValue(nameof(Name));
            PostalCode = GetValue(nameof(PostalCode), "-");
            Address = GetValue(nameof(Address));
            PhoneNo = GetValue(nameof(PhoneNo), "--");
            EMailAddress = GetValue(nameof(EMailAddress));
            BankAccount = GetValue(nameof(BankAccount));

        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        public override void Save()
        {

            Update(nameof(Name), Name);
            Update(nameof(PostalCode), PostalCode);
            Update(nameof(Address), Address);
            Update(nameof(PhoneNo), PhoneNo);
            Update(nameof(EMailAddress), EMailAddress);
            Update(nameof(BankAccount), BankAccount);

            WriteFile();

        }

    }

}
