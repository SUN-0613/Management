using AYam.Common.Data.File;
using AYam.Common.Data.List;
using Management.Data.Info;
using Management.Data.Path;
using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Management.Data.File
{
    public class QuotationFile : XmlDataFile
    {

        /// <summary>
        /// 取引先社名
        /// </summary>
        public string ClientName;

        /// <summary>
        /// 取引先担当者名
        /// </summary>
        public string ClientStaff;

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
        public string DeliveryDate;

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
            Summaries = new ObservableCollection<QuoteSummary>();

            ClientName = GetValue(nameof(ClientName), "");
            ClientStaff = GetValue(nameof(ClientStaff), "");
            QuoteNo = GetValue(nameof(QuoteNo), "");
            QuoteDate = GetValue(nameof(QuoteDate), DateTime.Now);
            JobName = GetValue(nameof(JobName), "");
            DeliveryDate = GetValue(nameof(DeliveryDate), "");
            Remarks = GetValue(nameof(Remarks), "");

            if (Element != null)
            {

                foreach (var element in Element.Elements(nameof(QuoteSummary)))
                {

                    Summaries.Add(new QuoteSummary()
                    {
                        No = GetValue(element.Element(nameof(QuoteSummary.No)), 1),
                        Summary = GetValue(element.Element(nameof(QuoteSummary.Summary)), ""),
                        Volume = GetValue(element.Element(nameof(QuoteSummary.Volume)), 0),
                        UnitPrice = GetValue(element.Element(nameof(QuoteSummary.UnitPrice)), 0),
                    });

                }

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
                new XElement(nameof(ClientStaff), ClientStaff),
                new XElement(nameof(QuoteNo), QuoteNo),
                new XElement(nameof(QuoteDate), QuoteDate),
                new XElement(nameof(JobName), JobName),
                new XElement(nameof(DeliveryDate), DeliveryDate),
                new XElement(nameof(Remarks), Remarks)
            })
            {

                foreach (var summary in Summaries)
                {

                    var element = new XElement(nameof(QuoteSummary));

                    AddElement(ref element, new XElement(nameof(QuoteSummary.No), summary.No));
                    AddElement(ref element, new XElement(nameof(QuoteSummary.Summary), summary.Summary));
                    AddElement(ref element, new XElement(nameof(QuoteSummary.Volume), summary.Volume));
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

    }
}
