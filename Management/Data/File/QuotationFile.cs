using AYam.Common.Data.File;
using AYam.Common.Data.List;
using Management.Data.Info;
using Management.Data.Path;
using Property = Management.Properties;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;

namespace Management.Data.File
{

    /// <summary>
    /// 見積書情報ファイル
    /// </summary>
    public class QuotationFile : XmlDataFile
    {

        /// <summary>
        /// 取引先社名
        /// </summary>
        public string ClientName;

        /// <summary>
        /// 取引先担当者名
        /// </summary>
        public ObservableCollection<Staff> ClientStaffs;

        /// <summary>
        /// 見積No
        /// </summary>
        public string QuoteNo;

        /// <summary>
        /// 見積日
        /// </summary>
        public DateTime QuoteDate;

        /// <summary>
        /// 件名
        /// </summary>
        public string JobName;

        /// <summary>
        /// マスタ情報
        /// </summary>
        public MasterFile Master;

        /// <summary>
        /// 納期
        /// </summary>
        public DeliveryDate DeliveryDate;

        /// <summary>
        /// 見積内容一覧
        /// </summary>
        public ObservableCollection<QuoteSummary> Summaries;

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks;

        /// <summary>
        /// 見積書情報ファイル
        /// </summary>
        /// <param name="dataFile">作成書類情報</param>
        public QuotationFile(DataFileInfo dataFile) : base(new PathInfo().Files.Quotation.Replace(PathInfo.Wild, dataFile.GetFileNameParts()), "Quote")
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Dispose()
        {

            Master.Dispose();
            Master = null;

            ClientStaffs.Clear();
            ClientStaffs = null;

            foreach (var summary in Summaries)
            {
                summary.InitializeTotalPrice();
            }

            Summaries.Clear();
            Summaries = null;

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override void Read()
        {

            Master = new MasterFile();
            ClientStaffs = new ObservableCollection<Staff>();
            Summaries = new ObservableCollection<QuoteSummary>();

            ClientName = GetValue(nameof(ClientName), "");
            QuoteNo = GetValue(nameof(QuoteNo), "");
            QuoteDate = GetValue(nameof(QuoteDate), DateTime.Today);
            JobName = GetValue(nameof(JobName), "");
            Remarks = GetValue(nameof(Remarks), "");

            if (Element != null)
            {

                var delivery = Element.Element(nameof(DeliveryDate));
                DeliveryDate = new DeliveryDate()
                {
                    No = GetValue(delivery.Element(nameof(DeliveryDate.No)), 0d),
                    Unit = GetValue(delivery.Element(nameof(DeliveryDate.Unit)), DeliveryUnit.Months)
                };

                foreach (var element in Element.Elements(nameof(ClientStaffs)))
                {
                    ClientStaffs.Add(new Staff(GetValue(element.Element(nameof(Staff.FirstName)), "")
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
                                        , GetAttribute(element, nameof(Staff.IsSelected), false))
                    {
                        IsSelected = GetAttribute(element, nameof(Staff.IsSelected), false)
                    });

                }

                foreach (var element in Element.Elements(nameof(QuoteSummary)))
                {

                    Summaries.Add(new QuoteSummary()
                    {
                        No = GetValue(element.Element(nameof(QuoteSummary.No)), 1),
                        Summary = GetValue(element.Element(nameof(QuoteSummary.Summary)), ""),
                        Volume = GetValue(element.Element(nameof(QuoteSummary.Volume)), 0),
                        Unit = GetValue(element.Element(nameof(QuoteSummary.Unit)), VolumeUnit.Pieces),
                        UnitPrice = GetValue(element.Element(nameof(QuoteSummary.UnitPrice)), 0),
                    });

                }

            }
            else
            {

                DeliveryDate = new DeliveryDate()
                {
                    No = 0d,
                    Unit = DeliveryUnit.Months
                };

            }

        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        public override void Save()
        {

            using (var elements = new List<XElement>
            {
                new XElement(nameof(ClientName), ClientName),
                new XElement(nameof(QuoteNo), QuoteNo),
                new XElement(nameof(QuoteDate), QuoteDate),
                new XElement(nameof(JobName), JobName),
                new XElement(nameof(Remarks), Remarks)
            })
            {

                var delivery = new XElement(nameof(DeliveryDate));

                AddElement(ref delivery, new XElement(nameof(DeliveryDate.No), DeliveryDate.No));
                AddElement(ref delivery, new XElement(nameof(DeliveryDate.Unit), DeliveryDate.Unit));

                elements.Add(delivery);

                foreach (var staff in ClientStaffs)
                {

                    var element = new XElement(nameof(ClientStaffs));

                    AddElement(ref element, new XElement(nameof(Staff.FirstName), staff.FirstName));
                    AddElement(ref element, new XElement(nameof(Staff.FirstKana), staff.FirstKana));
                    AddElement(ref element, new XElement(nameof(Staff.LastName), staff.LastName));
                    AddElement(ref element, new XElement(nameof(Staff.LastKana), staff.LastKana));
                    AddElement(ref element, new XElement(nameof(Staff.Department), staff.Department));
                    AddElement(ref element, new XElement(nameof(Staff.Position), staff.Position));
                    AddElement(ref element, new XElement(nameof(Staff.EMailAddress), staff.EMailAddress));
                    AddElement(ref element, new XElement(nameof(Staff.MobilePhone), staff.MobilePhone));
                    AddElement(ref element, new XElement(nameof(Staff.Remarks), staff.Remarks));
                    AddAttribute(ref element, new XAttribute(nameof(Staff.CreateDate), staff.CreateDate));
                    AddAttribute(ref element, new XAttribute(nameof(Staff.IsNotationFullName), staff.IsNotationFullName));
                    AddAttribute(ref element, new XAttribute(nameof(Staff.IsSelected), staff.IsSelected));

                    elements.Add(element);

                }

                foreach (var summary in Summaries)
                {

                    var element = new XElement(nameof(QuoteSummary));

                    AddElement(ref element, new XElement(nameof(QuoteSummary.No), summary.No));
                    AddElement(ref element, new XElement(nameof(QuoteSummary.Summary), summary.Summary));
                    AddElement(ref element, new XElement(nameof(QuoteSummary.Volume), summary.Volume));
                    AddElement(ref element, new XElement(nameof(QuoteSummary.Unit), summary.Unit));
                    AddElement(ref element, new XElement(nameof(QuoteSummary.UnitPrice), summary.UnitPrice));

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

        /// <summary>
        /// 客先担当者一覧を区切文字と敬称を付与して取得
        /// </summary>
        public string GetStaffNames()
        {

            var text = new StringBuilder(64);
            string returnValue = "";

            try
            {

                foreach (var staff in ClientStaffs)
                {

                    if (staff.IsSelected)
                    {

                        if (!text.Length.Equals(0))
                        {
                            text.Append(Property::ClientInfo.Comma);
                        }

                        text.Append(staff.IsNotationFullName ? staff.FullName : staff.LastName).Append(Property::ClientInfo.HonorificTitle);

                    }

                }

                returnValue = text.ToString();

            }
            finally
            {
                text.Clear();
                text = null;
            }

            return returnValue;

        }

    }
}
