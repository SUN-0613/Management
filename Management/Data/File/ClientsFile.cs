using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Management.Data.File
{

    /// <summary>
    /// 取引先一覧情報ファイル
    /// </summary>
    public class ClientsFile : Base.DataFile
    {

        /// <summary>
        /// 取引先一覧
        /// </summary>
        public ObservableCollection<Client> Clients { get; set; }

        /// <summary>
        /// 取引先一覧情報ファイル
        /// </summary>
        public ClientsFile() : base(new PathInfo().Files.Clients)
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

            var names = GetValues(nameof(Client.Name));
            var wildNames = GetValues(nameof(Client.FileWildName));

            for (int iLoop = 0; iLoop < names.Count; iLoop++)
            {
                Clients.Add(new Client(names[iLoop], wildNames[iLoop]));
            }

        }

        /// <summary>
        /// ファイル保存
        /// </summary>
        public override void Save()
        {

            var names = new List<string>();
            var wildNames = new List<string>();

            for (int iLoop = 0; iLoop < Clients.Count; iLoop++)
            {
                names.Add(Clients[iLoop].Name);
                wildNames.Add(Clients[iLoop].FileWildName);
            }

            Update(nameof(Client.Name), names);
            Update(nameof(Client.FileWildName), wildNames);

            WriteFile();

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
        public string Name;

        /// <summary>
        /// ファイル名の"*"部分の名称
        /// </summary>
        public string FileWildName;

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
