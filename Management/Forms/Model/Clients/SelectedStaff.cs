using Management.Data.File;
using Management.Data.Info;
using System;
using System.Collections.ObjectModel;

namespace Management.Forms.Model.Clients
{

    /// <summary>
    /// 客先担当選択.Model
    /// </summary>
    public class SelectedStaff : IDisposable
    {


        #region File

        /// <summary>
        /// ファイル
        /// </summary>
        private object _File;

        #endregion

        #region ViewModel.Property

        /// <summary>
        /// 取引先名
        /// </summary>
        public string ClientName;

        /// <summary>
        /// 客先担当一覧
        /// </summary>
        public ObservableCollection<Staff> Staffs { get; set; }

        #endregion

        /// <summary>
        /// 客先担当選択.Model
        /// </summary>
        /// <param name="dataFile">ファイル情報</param>
        public SelectedStaff(DataFileInfo dataFile)
        {

            switch (dataFile.Kind)
            {

                case DataKind.Quotation:

                    var file = new QuotationFile(dataFile);

                    MakeClientStaffs(file.ClientName, file.ClientStaffs);
                    _File = file;

                    break;
                    

            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            InitializeStaffs();

            if (_File is IDisposable dispose)
            {
                dispose.Dispose();
            }

            _File = null;

        }

        /// <summary>
        /// Staffsの初期化
        /// </summary>
        private void InitializeStaffs()
        {

            if (Staffs != null)
            {
                Staffs.Clear();
                Staffs = null;
            }

        }

        /// <summary>
        /// 指定取引先の担当一覧を作成
        /// </summary>
        /// <param name="clientName">指定取引先</param>
        /// <param name="selectedStaffs">選択済の担当一覧</param>
        private void MakeClientStaffs(string clientName, ObservableCollection<Staff> selectedStaffs)
        {

            string wildName = "";

            // 初期化
            InitializeStaffs();
            Staffs = new ObservableCollection<Staff>();

            ClientName = clientName;

            // 取引先一覧の取得
            var clientList = new ClientsFile();

            // 取引先一覧より指定取引先を検索
            foreach (var client in clientList.Clients)
            {

                if (client.Name.Equals(clientName))
                {
                    wildName = client.FileWildName;
                    break;
                }

            }

            // メモリ解放
            clientList.Dispose();
            clientList = null;

            if (!wildName.Equals(""))
            {

                // 取引先詳細の取得
                var client = new ClientDetailFile(wildName);

                // 担当一覧の取得
                foreach(var staff in client.Staffs)
                {

                    Staffs.Add((Staff)staff.Clone());

                    bool isSelected = false;
                    bool isNotationFullName = false;
                    var index = selectedStaffs.IndexOf(staff);

                    if (!index.Equals(-1))
                    {
                        isSelected = selectedStaffs[index].IsSelected;
                        isNotationFullName = selectedStaffs[index].IsNotationFullName;
                    }

                    Staffs[Staffs.Count - 1].IsSelected = isSelected;
                    Staffs[Staffs.Count - 1].IsNotationFullName = isNotationFullName;

                }

                // メモリ解放
                client.Dispose();
                client = null;

            }

        }

    }
}
