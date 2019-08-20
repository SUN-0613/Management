using AYam.Common.Data.File;
using AYam.Common.Data.List;
using Management.Data.Info;
using Management.Data.Path;
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
        /// 会社名：よみがな
        /// </summary>
        public string CompanyKana;

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
        /// 銀行口座
        /// </summary>
        public string BankAccount;

        /// <summary>
        /// 担当差一覧
        /// </summary>
        public ObservableCollection<Staff> Staffs { get; set; }

        /// <summary>
        /// メモ
        /// </summary>
        public string Remarks;

        /// <summary>
        /// 取引先情報ファイル
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        public ClientDetailFile(string wildName) : base(new PathInfo().Files.ClientDetail.Replace(PathInfo.Wild, wildName), "Client")
        { }

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

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override void Read()
        {

            CompanyName = GetValue(nameof(CompanyName), "");
            CompanyKana = GetValue(nameof(CompanyKana), "");
            PostalCode = GetValue(nameof(PostalCode), "-");
            Address = GetValue(nameof(Address), "");
            PhoneNo = GetValue(nameof(PhoneNo), "--");
            FaxNo = GetValue(nameof(FaxNo), "--");
            BankAccount = GetValue(nameof(BankAccount), "");
            Remarks = GetValue(nameof(Remarks), "");

            Staffs = new ObservableCollection<Staff>();

            if (Element != null)
            {

                foreach (var element in Element.Elements(nameof(Staff)))
                {

                    Staffs.Add(new Staff(GetValue(element.Element(nameof(Staff.FirstName)), "")
                                        , GetValue(element.Element(nameof(Staff.FirstKana)), "")
                                        , GetValue(element.Element(nameof(Staff.LastName)), "")
                                        , GetValue(element.Element(nameof(Staff.LastKana)), "")
                                        , GetValue(element.Element(nameof(Staff.Department)), "")
                                        , GetValue(element.Element(nameof(Staff.Position)), "")
                                        , GetValue(element.Element(nameof(Staff.EMailAddress)), "")
                                        , GetValue(element.Element(nameof(Staff.MobilePhone)), "")
                                        , GetValue(element.Element(nameof(Staff.Remarks)), "")
                                        , GetAttribute(element, nameof(Staff.CreateDate), DateTime.Now)
                                        , GetAttribute(element, nameof(Staff.IsNotationFullName), false)
                                        , GetAttribute(element, nameof(Staff.IsFullNameJapaneseStyle), true)
                                        , GetAttribute(element, nameof(Staff.IsSelected), false)
                                        ));

                }

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
                , new XElement(nameof(CompanyKana), CompanyKana)
                , new XElement(nameof(PostalCode), PostalCode)
                , new XElement(nameof(Address), Address)
                , new XElement(nameof(PhoneNo), PhoneNo)
                , new XElement(nameof(FaxNo), FaxNo)
                , new XElement(nameof(BankAccount), BankAccount)
                , new XElement(nameof(Remarks), Remarks)
            })
            {

                for (int iLoop = 0; iLoop < Staffs.Count; iLoop++)
                {

                    var element = new XElement(nameof(Staff));

                    AddElement(ref element, new XElement(nameof(Staff.FirstName), Staffs[iLoop].FirstName));
                    AddElement(ref element, new XElement(nameof(Staff.FirstKana), Staffs[iLoop].FirstKana));
                    AddElement(ref element, new XElement(nameof(Staff.LastName), Staffs[iLoop].LastName));
                    AddElement(ref element, new XElement(nameof(Staff.LastKana), Staffs[iLoop].LastKana));
                    AddElement(ref element, new XElement(nameof(Staff.Department), Staffs[iLoop].Department));
                    AddElement(ref element, new XElement(nameof(Staff.Position), Staffs[iLoop].Position));
                    AddElement(ref element, new XElement(nameof(Staff.EMailAddress), Staffs[iLoop].EMailAddress));
                    AddElement(ref element, new XElement(nameof(Staff.MobilePhone), Staffs[iLoop].MobilePhone));
                    AddElement(ref element, new XElement(nameof(Staff.Remarks), Staffs[iLoop].Remarks));
                    AddAttribute(ref element, new XAttribute(nameof(Staff.CreateDate), Staffs[iLoop].CreateDate));
                    AddAttribute(ref element, new XAttribute(nameof(Staff.IsNotationFullName), Staffs[iLoop].IsNotationFullName));

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

}
