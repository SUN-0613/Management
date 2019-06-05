using Management.Data.File;
using System;

namespace Management.Forms.Model.Clients
{

    /// <summary>
    /// 取引先追加.Model
    /// </summary>
    public class ClientAdd : IDisposable
    {

        /// <summary>
        /// 取引先一覧情報ファイル
        /// </summary>
        private ClientsFile _File;

        #region ViewModel.Property

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName = "";

        #endregion

        /// <summary>
        /// 取引先追加.Model
        /// </summary>
        public ClientAdd()
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
        /// 入力した会社名が取引先一覧に登録済みかチェック
        /// </summary>
        /// <returns>
        /// True:登録済み
        /// False:未登録
        /// </returns>
        public bool Contains()
        {

            bool returnValue = false;
            
            for (int iLoop = 0; iLoop < _File.Clients.Count; iLoop++)
            {

                if (_File.Clients[iLoop].Name.Equals(CompanyName))
                {
                    returnValue = true;
                    break;
                }

            }

            return returnValue;

        }

    }

}
