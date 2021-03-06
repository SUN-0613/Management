﻿using AYam.Common.Data.File;
using AYam.Common.Data.List;
using Management.Data.Info;
using Management.Data.Path;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace Management.Data.File
{

    /// <summary>
    /// 取引先一覧情報ファイル
    /// </summary>
    public class ClientsFile : XmlDataFile
    {

        /// <summary>
        /// XML要素名
        /// </summary>
        private const string _ElementName = "Client";

        /// <summary>
        /// 取引先一覧
        /// </summary>
        public ObservableCollection<Client> Clients { get; set; }

        /// <summary>
        /// 取引先一覧情報ファイル
        /// </summary>
        public ClientsFile() : base(new PathInfo().Files.Clients, "Clients")
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Dispose()
        {

            if (Clients != null)
            {
                Clients.Clear();
                Clients = null;
            }

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override void Read()
        {

            Clients = new ObservableCollection<Client>();

            if (Element != null)
            {

                foreach (var element in Element.Elements(_ElementName))
                {
                    Clients.Add(
                        new Client(
                            GetValue(element, nameof(Client.Name), "")
                            , GetAttribute(element, nameof(Client.FileWildName), "")
                            )
                        );
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

                for (int iLoop = 0; iLoop < Clients.Count; iLoop++)
                {

                    var nameElement = new XElement(nameof(Client.Name), Clients[iLoop].Name);
                    var element = new XElement(_ElementName, nameElement);
                    AddAttribute(ref element, new XAttribute(nameof(Client.FileWildName), Clients[iLoop].FileWildName));

                    elements.Add(element);

                }

                WriteFile(elements);

            }

        }

    }

}
