using AYam.Common.Controls.Interface;
using AYam.Common.MVVM;
using AYam.Common.MVVM.Custom;
using System;
using System.Collections.ObjectModel;

namespace Management.Forms.ViewModel
{

    /// <summary>
    /// 取引先管理.ViewModel
    /// </summary>
    public class ClientInfo : EditedViewModelBase, IDisposable, IClosing
    {

        #region Model

        #endregion

        #region Property

        #endregion

        /// <summary>
        /// 閉じる確認メッセージ.タイトル
        /// </summary>
        public string ClosingTitle { get { return Properties.Title.MasterInfo; } }

        /// <summary>
        /// 閉じる確認メッセージ.文章
        /// </summary>
        public string ClosingMessage { get { return Properties.MasterInfo.MessageClose; } }

        /// <summary>
        /// 取引先管理.ViewModel
        /// </summary>
        public ClientInfo()
        { }

        /// <summary>
        /// 閉じる処理
        /// </summary>
        /// <returns>
        /// True:閉じる処理続行
        /// False:閉じる処理中止
        /// </returns>
        bool IClosing.OnClosing()
        {

            return !IsEdited;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        { }

        /// <summary>
        /// データ保存
        /// </summary>
        public void Save()
        {
            //_Model.Save();
            ResetEditFlg();
        }

    }

}
