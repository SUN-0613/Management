using Management.Data.File;
using Management.Data.Info;
using System;

namespace Management.Forms.Model.Clients
{

    /// <summary>
    /// 担当者登録.Model
    /// </summary>
    public class ClientStaffAdd : IDisposable
    {

        /// <summary>
        /// 取引先詳細情報ファイル
        /// </summary>
        private ClientDetailFile _File;

        #region ViewModel.Property

        /// <summary>
        /// 担当者情報
        /// </summary>
        public Staff Staff;

        #endregion

        /// <summary>
        /// 担当者登録.Model
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        /// <param name="staff">担当者情報</param>
        public ClientStaffAdd(string wildName, Staff staff = null)
        {
            _File = new ClientDetailFile(wildName);

            if (staff == null)
            {
                Staff = new Staff("", "", "", "", "", "", "", "", "", DateTime.Now);
            }
            else
            {
                Staff = (Staff)staff.Clone();
            }
            
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
        /// 入力した担当者が担当一覧に登録済みかチェック
        /// </summary>
        /// <returns>
        /// True:登録済
        /// False:未登録
        /// </returns>
        public bool Contains()
        {
            return _File.Staffs.Contains(Staff);
        }

    }

}
