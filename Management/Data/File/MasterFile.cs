using AYam.Common.Data.File;
using AYam.Common.Data.List;
using System.Xml.Linq;

namespace Management.Data.File
{

    /// <summary>
    /// マスタ情報ファイル
    /// </summary>
    public class MasterFile : XmlDataFile
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
        public MasterFile() : base(new PathInfo().Files.Master, "Master")
        { }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override void Read()
        {

            Name = GetValue<string>(nameof(Name));
            PostalCode = GetValue<string>(nameof(PostalCode), "-");
            Address = GetValue<string>(nameof(Address));
            PhoneNo = GetValue<string>(nameof(PhoneNo), "--");
            EMailAddress = GetValue<string>(nameof(EMailAddress));
            BankAccount = GetValue<string>(nameof(BankAccount));

        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        public override void Save()
        {

            using (var elements = new List<XElement>
            {
                new XElement(nameof(Name), Name)
                , new XElement(nameof(PostalCode), PostalCode)
                , new XElement(nameof(Address), Address)
                , new XElement(nameof(PhoneNo), PhoneNo)
                , new XElement(nameof(EMailAddress), EMailAddress)
                , new XElement(nameof(BankAccount), BankAccount)
            })
            {

                WriteFile(elements);

            }

        }

    }

}
