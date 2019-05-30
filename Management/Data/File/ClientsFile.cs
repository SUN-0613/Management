using AYam.Common.Data.File;
using AYam.Common.Data.List;
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
        {
            Clients = new ObservableCollection<Client>();
        }

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

            base.Dispose();

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public override void Read()
        {

            foreach(var element in Element.Elements(_ElementName))
            {
                Clients.Add(new Client(GetValue(nameof(Client.Name), ""), GetAttribute(nameof(Client.FileWildName), "")));
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

                    var element = new XElement(_ElementName, Clients[iLoop].Name);
                    AddAttribute(ref element, new XAttribute(nameof(Client.FileWildName), Clients[iLoop].FileWildName));

                    elements.Add(element);

                }

                WriteFile(elements);

            }

        }

    }

    /// <summary>
    /// 取引先情報
    /// </summary>
    public class Client
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ファイル名の"*"部分の名称
        /// </summary>
        public string FileWildName { get; set; }

        /// <summary>
        /// 取引先情報
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="wild">ファイル名の"*"部分の名称</param>
        public Client(string name, string wild)
        {
            Name = name;
            FileWildName = wild;
        }

    }

}
