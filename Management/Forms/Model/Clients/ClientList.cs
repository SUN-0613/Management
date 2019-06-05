using Management.Data.File;
using Management.Data.Info;
using System;
using System.Collections.ObjectModel;

namespace Management.Forms.Model.Clients
{

    /// <summary>
    /// 取引先管理.取引先一覧Model
    /// </summary>
    public class ClientList : IDisposable
    {

        /// <summary>
        /// 取引先一覧情報ファイル
        /// </summary>
        private ClientsFile _File;

        #region ViewModel.Property

        /// <summary>
        /// 取引先一覧
        /// </summary>
        public ObservableCollection<Client> Clients
        {
            get { return _File.Clients; }
            set { _File.Clients = value; }
        }

        #endregion

        /// <summary>
        /// 取引先管理.取引先一覧Model
        /// </summary>
        public ClientList()
        {

            _File = new ClientsFile();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_File != null)
            {
                _File.Dispose();
                _File = null;
            }

        }

        /// <summary>
        /// 入力値保存
        /// </summary>
        public void Save()
        {
            _File.Save();
        }

    }

}
