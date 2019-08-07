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
    /// ジョブ一覧情報ファイル
    /// </summary>
    public class JobListFile : XmlDataFile
    {

        /// <summary>
        /// XML要素名
        /// </summary>
        private const string _ElementName = "Job";

        /// <summary>
        /// ジョブ一覧
        /// </summary>
        public ObservableCollection<Job> Jobs { get; set; }

        /// <summary>
        /// ジョブ一覧情報ファイル
        /// </summary>
        public JobListFile() : base(new PathInfo().Files.Jobs, "Jobs")
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Dispose()
        {

            if (Jobs != null)
            {
                
                for (int iLoop = 0; iLoop < Jobs.Count; iLoop++)
                {

                    if (Jobs[iLoop] != null)
                    {
                        Jobs[iLoop].Dispose();
                        Jobs[iLoop] = null;
                    }

                }

                Jobs.Clear();
                Jobs = null;

            }

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override void Read()
        {

            Jobs = new ObservableCollection<Job>();

            if (Element != null)
            {

                foreach (var element in Element.Elements(_ElementName))
                {

                    var job = new Job
                    {
                        CreateDate = GetAttribute(element, nameof(Job.CreateDate), DateTime.MinValue),
                        Name = GetValue(element, nameof(Job.Name), ""),
                        Client = GetValue(element, nameof(Job.Client), ""),
                        DeliveryDate = GetValue(element, nameof(Job.DeliveryDate), DateTime.MinValue),
                        Price = GetValue(element, nameof(Job.Price), decimal.MinusOne)
                    };

                    foreach (var quote in element.Elements(nameof(Job.Quotations)))
                    {

                        job.Quotations.Add(
                            new DataFileInfo(DataKind.Quotation
                                            , GetAttribute(quote, nameof(DataFileInfo.Revision), 0)
                                            , GetAttribute(quote, nameof(DataFileInfo.CreateDate), DateTime.MinValue)
                                            , GetAttribute(quote, nameof(DataFileInfo.Seaquence), 1)
                                            , GetValue(quote, false)
                                            ));
                    }

                    foreach (var delivery in element.Elements(nameof(Job.Deliveries)))
                    {

                        job.Deliveries.Add(
                            new DataFileInfo(DataKind.Delivery
                                            , GetAttribute(delivery, nameof(DataFileInfo.Revision), 0)
                                            , GetAttribute(delivery, nameof(DataFileInfo.CreateDate), DateTime.MinValue)
                                            , GetAttribute(delivery, nameof(DataFileInfo.Seaquence), 1)
                                            , GetValue(delivery, false)
                                            ));

                    }

                    foreach (var invoice in element.Elements(nameof(Job.Invoices)))
                    {

                        job.Invoices.Add(
                            new DataFileInfo(DataKind.Invoice
                                            , GetAttribute(invoice, nameof(DataFileInfo.Revision), 0)
                                            , GetAttribute(invoice, nameof(DataFileInfo.CreateDate), DateTime.MinValue)
                                            , GetAttribute(invoice, nameof(DataFileInfo.Seaquence), 1)
                                            , GetValue(invoice, false)
                                            ));

                    }

                    var cover = element.Element(nameof(Job.CoverLetter));
                    job.CoverLetter = new DataFileInfo(DataKind.CoverLetter
                                                        , GetAttribute(cover, nameof(DataFileInfo.Revision), 0)
                                                        , GetAttribute(cover,nameof(DataFileInfo.CreateDate), DateTime.MinValue)
                                                        , GetAttribute(cover, nameof(DataFileInfo.Seaquence), 1)
                                                        , GetValue(cover, false)
                                                        );

                    var status = element.Element(nameof(Job.Status));
                    job.Status = new JobStatus()
                    {
                        Status = GetValue(status, StatusEnum.None)
                        , Deadline = GetAttribute(status, nameof(JobStatus.Deadline), "")
                        , OrderedDate = GetAttribute(status, nameof(JobStatus.OrderedDate), DateTime.MinValue)
                        , DeliveryDate = GetAttribute(status, nameof(JobStatus.DeliveryDate), DateTime.MinValue)
                    };

                    Jobs.Add(job);

                }

            }

        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        public override void Save()
        {

            using (var elements = new List<XElement>())
            {

                for (int iLoop = 0; iLoop < Jobs.Count; iLoop++)
                {

                    var element = new XElement(_ElementName);

                    AddAttribute(ref element, new XAttribute(nameof(Job.CreateDate), Jobs[iLoop].CreateDate));
                    AddElement(ref element, new XElement(nameof(Job.Name), Jobs[iLoop].Name));
                    AddElement(ref element, new XElement(nameof(Job.Client), Jobs[iLoop].Client));
                    AddElement(ref element, new XElement(nameof(Job.DeliveryDate), Jobs[iLoop].DeliveryDate));
                    AddElement(ref element, new XElement(nameof(Job.Price), Jobs[iLoop].Price));

                    for (int jLoop = 0; jLoop < Jobs[iLoop].Quotations.Count; jLoop++)
                    {

                        var child = new XElement(nameof(Job.Quotations), Jobs[iLoop].Quotations[jLoop].CreatedFlg);

                        AddAttribute(ref child, new XAttribute(nameof(DataFileInfo.Revision), Jobs[iLoop].Quotations[jLoop].Revision));
                        AddAttribute(ref child, new XAttribute(nameof(DataFileInfo.CreateDate), Jobs[iLoop].Quotations[jLoop].CreateDate));
                        AddAttribute(ref child, new XAttribute(nameof(DataFileInfo.Seaquence), Jobs[iLoop].Quotations[jLoop].Seaquence));

                        AddElement(ref element, child);

                    }

                    for (int jLoop = 0; jLoop < Jobs[iLoop].Deliveries.Count; jLoop++)
                    {

                        var child = new XElement(nameof(Job.Deliveries), Jobs[iLoop].Deliveries[jLoop].CreatedFlg);

                        AddAttribute(ref child, new XAttribute(nameof(DataFileInfo.Revision), Jobs[iLoop].Deliveries[jLoop].Revision));
                        AddAttribute(ref child, new XAttribute(nameof(DataFileInfo.CreateDate), Jobs[iLoop].Deliveries[jLoop].CreateDate));
                        AddAttribute(ref child, new XAttribute(nameof(DataFileInfo.Seaquence), Jobs[iLoop].Deliveries[jLoop].Seaquence));

                        AddElement(ref element, child);

                    }

                    for (int jLoop = 0; jLoop < Jobs[iLoop].Invoices.Count; jLoop++)
                    {

                        var child = new XElement(nameof(Job.Invoices), Jobs[iLoop].Invoices[jLoop].CreatedFlg);

                        AddAttribute(ref child, new XAttribute(nameof(DataFileInfo.Revision), Jobs[iLoop].Invoices[jLoop].Revision));
                        AddAttribute(ref child, new XAttribute(nameof(DataFileInfo.CreateDate), Jobs[iLoop].Invoices[jLoop].CreateDate));
                        AddAttribute(ref child, new XAttribute(nameof(DataFileInfo.Seaquence), Jobs[iLoop].Invoices[jLoop].Seaquence));

                        AddElement(ref element, child);

                    }

                    if (Jobs[iLoop].CoverLetter != null)
                    {

                        var cover = new XElement(nameof(Job.CoverLetter), Jobs[iLoop].CoverLetter.CreatedFlg);

                        AddAttribute(ref cover, new XAttribute(nameof(DataFileInfo.Revision), Jobs[iLoop].CoverLetter.Revision));
                        AddAttribute(ref cover, new XAttribute(nameof(DataFileInfo.CreateDate), Jobs[iLoop].CoverLetter.CreateDate));
                        AddAttribute(ref cover, new XAttribute(nameof(DataFileInfo.Seaquence), Jobs[iLoop].CoverLetter.Seaquence));

                        AddElement(ref element, cover);

                    }

                    if (Jobs[iLoop].Status != null)
                    {

                        var status = new XElement(nameof(Job.Status), Jobs[iLoop].Status.Status);

                        AddAttribute(ref status, new XAttribute(nameof(JobStatus.Deadline), Jobs[iLoop].Status.Deadline));
                        AddAttribute(ref status, new XAttribute(nameof(JobStatus.OrderedDate), Jobs[iLoop].Status.OrderedDate));
                        AddAttribute(ref status, new XAttribute(nameof(JobStatus.DeliveryDate), Jobs[iLoop].Status.DeliveryDate));

                        AddElement(ref element, status);

                    }

                    elements.Add(element);

                }

                WriteFile(elements);

            }

        }

    }

}
