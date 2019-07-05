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
        /// 名前
        /// </summary>
        public string FirstName;

        /// <summary>
        /// 名前：よみがな
        /// </summary>
        public string FirstKana;

        /// <summary>
        /// 名字
        /// </summary>
        public string LastName;

        /// <summary>
        /// 名字：よみがな
        /// </summary>
        public string LastKana;

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
        /// FAX番号
        /// </summary>
        public string FaxNo;

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
        public MasterFile() : base(new Path.PathInfo().Files.Master, "Master")
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Dispose()
        { }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override void Read()
        {

            FirstName = GetValue(nameof(FirstName), "");
            FirstKana = GetValue(nameof(FirstKana), "");
            LastName = GetValue(nameof(LastName), "");
            LastKana = GetValue(nameof(LastKana), "");
            PostalCode = GetValue(nameof(PostalCode), "-");
            Address = GetValue(nameof(Address), "");
            PhoneNo = GetValue(nameof(PhoneNo), "--");
            FaxNo = GetValue(nameof(FaxNo), "--");
            EMailAddress = GetValue(nameof(EMailAddress), "");
            BankAccount = GetValue(nameof(BankAccount), "");

        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        public override void Save()
        {

            using (var elements = new List<XElement>
            {
                new XElement(nameof(FirstName), FirstName)
                , new XElement(nameof(FirstKana), FirstKana)
                , new XElement(nameof(LastName), LastName)
                , new XElement(nameof(LastKana), LastKana)
                , new XElement(nameof(PostalCode), PostalCode)
                , new XElement(nameof(Address), Address)
                , new XElement(nameof(PhoneNo), PhoneNo)
                , new XElement(nameof(FaxNo), FaxNo)
                , new XElement(nameof(EMailAddress), EMailAddress)
                , new XElement(nameof(BankAccount), BankAccount)
            })
            {

                WriteFile(elements);

            }

        }

        /// <summary>
        /// フルネーム取得
        /// </summary>
        /// <param name="isJapaneseStyle">姓名表記か</param>
        /// <returns>フルネーム</returns>
        public string GetFullName(bool isJapaneseStyle = true)
        {

            if (isJapaneseStyle)
            {
                return LastName + " " + FirstName;
            }
            else
            {
                return FirstName + " " + LastName;
            }

        }

    }

}
