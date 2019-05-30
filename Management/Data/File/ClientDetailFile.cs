using AYam.Common.Data.File;
using AYam.Common.Data.List;
using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Management.Data.File
{

    /// <summary>
    /// 取引先情報ファイル
    /// </summary>
    public class ClientDetailFile : XmlDataFile
    {

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName;

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
        /// 銀行口座
        /// </summary>
        public string BankAccount;

        /// <summary>
        /// 担当差一覧
        /// </summary>
        public ObservableCollection<Staff> Staffs { get; set; }

        /// <summary>
        /// 取引先情報ファイル
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        public ClientDetailFile(string wildName) : base(new PathInfo().Files.ClientDetail.Replace(PathInfo.Wild, wildName), "Client")
        {
            Staffs = new ObservableCollection<Staff>();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Dispose()
        {

            if (Staffs != null)
            {
                Staffs.Clear();
                Staffs = null;
            }

            base.Dispose();

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override void Read()
        {

            CompanyName = GetValue(nameof(CompanyName), "");
            PostalCode = GetValue(nameof(PostalCode), "-");
            Address = GetValue(nameof(Address), "");
            PhoneNo = GetValue(nameof(PhoneNo), "--");
            BankAccount = GetValue(nameof(BankAccount), "");

            foreach (var element in Element.Elements(nameof(Staff)))
            {

                Staffs.Add(new Staff(GetValue(element.Element(nameof(Staff.Name)), "")
                                    , GetValue(element.Element(nameof(Staff.Phonetic)), "")
                                    , GetValue(element.Element(nameof(Staff.EMailAddress)), "")
                                    , GetValue(element.Element(nameof(Staff.MobilePhone)), "")
                                    , GetAttribute(nameof(Staff.CreateDate), DateTime.Now)
                                    ));

            }


        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        public override void Save()
        {

            using (var elements = new List<XElement>()
            {
                new XElement(nameof(CompanyName), CompanyName)
                , new XElement(nameof(PostalCode), PostalCode)
                , new XElement(nameof(Address), Address)
                , new XElement(nameof(PhoneNo), PhoneNo)
                , new XElement(nameof(BankAccount), BankAccount)

            })
            {

                for (int iLoop = 0; iLoop < Staffs.Count; iLoop++)
                {

                    var element = new XElement(nameof(Staff));

                    AddElement(ref element, new XElement(nameof(Staff.Name), Staffs[iLoop].Name));
                    AddElement(ref element, new XElement(nameof(Staff.Phonetic), Staffs[iLoop].Phonetic));
                    AddElement(ref element, new XElement(nameof(Staff.EMailAddress), Staffs[iLoop].EMailAddress));
                    AddElement(ref element, new XElement(nameof(Staff.MobilePhone), Staffs[iLoop].MobilePhone));
                    AddAttribute(ref element, new XAttribute(nameof(Staff.CreateDate), Staffs[iLoop].CreateDate));

                    elements.Add(element);

                }

                WriteFile(elements);

            }

        }

        /// <summary>
        /// XMLファイルの削除
        /// </summary>
        public void Delete()
        {
            DeleteXmlFile();
        }

    }

    /// <summary>
    /// 担当者情報
    /// </summary>
    public class Staff
    {

        /// <summary>
        /// 氏名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 振り仮名
        /// </summary>
        public string Phonetic { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string EMailAddress { get; set; }

        /// <summary>
        /// 携帯電話番号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 担当者情報
        /// </summary>
        /// <param name="name">氏名</param>
        /// <param name="phonetic">振り仮名</param>
        /// <param name="eMailAddress">メールアドレス</param>
        /// <param name="mobilePhone">携帯電話番号</param>
        /// <param name="createDate">登録日</param>
        public Staff(string name, string phonetic, string eMailAddress, string mobilePhone, DateTime createDate)
        {

            Name = name;
            Phonetic = phonetic;
            EMailAddress = eMailAddress;
            MobilePhone = mobilePhone;
            CreateDate = createDate;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {

            if (obj is Staff staff)
            {

                return staff.Name.Equals(Name)
                        && staff.Phonetic.Equals(Phonetic)
                        && staff.EMailAddress.Equals(EMailAddress)
                        && staff.MobilePhone.Equals(MobilePhone)
                        && staff.CreateDate.Equals(CreateDate);
                
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// ハッシュコードの取得
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

}
