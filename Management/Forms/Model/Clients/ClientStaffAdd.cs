using Management.Data.File;
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
        public ClientStaffAdd(string wildName)
        {

            _File = new ClientDetailFile(wildName);
            Staff = new Staff("", "", "", "", DateTime.Now);

        }

        /// <summary>
        /// 担当者登録.Model
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        /// <param name="name">氏名</param>
        /// <param name="phonetic">振り仮名</param>
        /// <param name="eMailAddress">メールアドレス</param>
        /// <param name="mobilePhone">携帯電話番号</param>
        /// <param name="createDate">登録日</param>
        public ClientStaffAdd(string wildName, string name, string phonetic, string eMailAddress, string mobilePhone, DateTime createDate)
        {
            _File = new ClientDetailFile(wildName);
            Staff = new Staff(name, phonetic, eMailAddress, mobilePhone, createDate);
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
