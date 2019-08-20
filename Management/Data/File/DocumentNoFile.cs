using AYam.Common.Data.File;
using AYam.Common.Data.List;
using Management.Data.Info;
using Management.Data.Path;
using System;
using System.Xml.Linq;

namespace Management.Data.File
{

    /// <summary>
    /// 各書類の最終No保持ファイル
    /// </summary>
    public class DocumentNoFile : XmlDataFile
    {

        /// <summary>
        /// 見積No
        /// </summary>
        public DocumentNo Quotation = new DocumentNo(DateTime.Today, 0);

        /// <summary>
        /// 納品No
        /// </summary>
        public DocumentNo Delivery = new DocumentNo(DateTime.Today, 0);

        /// <summary>
        /// 請求No
        /// </summary>
        public DocumentNo Invoice = new DocumentNo(DateTime.Today, 0);

        /// <summary>
        /// XMLタグ名：作成日
        /// </summary>
        private readonly string _CreateDate = "CreateDate";

        /// <summary>
        /// 各書類の最終No保持ファイル
        /// </summary>
        public DocumentNoFile() : base(new PathInfo().Files.DocumentNo, "Document")
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
            
            if (Element != null)
            {

                var quote = Element.Element(nameof(Quotation));
                Quotation = new DocumentNo(GetAttribute(quote, _CreateDate, DateTime.Today), GetValue(quote, 0));

                var delivery = Element.Element(nameof(Delivery));
                Delivery = new DocumentNo(GetAttribute(delivery, _CreateDate, DateTime.Today), GetValue(delivery, 0));

                var invoice = Element.Element(nameof(Invoice));
                Invoice = new DocumentNo(GetAttribute(invoice, _CreateDate, DateTime.Today), GetValue(invoice, 0));

            }

        }

        /// <summary>
        /// ファイル書込
        /// </summary>
        public override void Save()
        {

            using (var elements = new List<XElement>())
            {

                var quote = new XElement(nameof(Quotation), Quotation.No);
                AddAttribute(ref quote, new XAttribute(_CreateDate, Quotation.CreateDate));
                elements.Add(quote);

                var delivery = new XElement(nameof(Delivery), Delivery.No);
                AddAttribute(ref delivery, new XAttribute(_CreateDate, Delivery.CreateDate));
                elements.Add(delivery);

                var invoice = new XElement(nameof(Invoice), Invoice.No);
                AddAttribute(ref invoice, new XAttribute(_CreateDate, Invoice.CreateDate));
                elements.Add(invoice);

                WriteFile(elements);

            }

        }

        /// <summary>
        /// 書類No更新
        /// </summary>
        /// <param name="kind">書類の種類</param>
        /// <returns>更新後の書類No</returns>
        public string UpdateDocumentNo(DataKind kind)
        {

            switch (kind)
            {

                case DataKind.Quotation:
                    return UpdateDocumentNo(Quotation);

                case DataKind.Delivery:
                    return UpdateDocumentNo(Delivery);

                case DataKind.Invoice:
                    return UpdateDocumentNo(Invoice);

                default:
                    throw new Exception("該当する書類に書類Noはありません");

            }

        }

        /// <summary>
        /// 書類Noの更新
        /// </summary>
        /// <param name="document">対象書類</param>
        /// <returns>更新後の書類No</returns>
        private string UpdateDocumentNo(DocumentNo document)
        {

            document.UpdateDocumentNo();
            Save();

            return document.GetDocumentNo();

        }

    }

}
