using AYam.Common.MVVM;
using MVVM = Management.Pages.ViewModel.Base;
using System;
using System.Windows.Controls;

namespace Management.Forms.Model.Menu.Class
{

#pragma warning disable 0659

    /// <summary>
    /// TabControl内に表示するデータ
    /// </summary>
    public class TabItemData : ViewModelBase, IDisposable
    {

        #region Property

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDate = DateTime.Now;

        /// <summary>
        /// ヘッダ
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 表示データ
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// 閉じるコマンド
        /// </summary>
        public DelegateCommand CloseCommand
        {
            get
            {
                return new DelegateCommand(
                    ()=> 
                    {
                        CloseTab();
                    }
                    ,()=>true);
            }
        }

        /// <summary>
        /// 新規タブを呼び出す場合の表示データ
        /// </summary>
        public TabItemData NewTab = null;

        /// <summary>
        /// 新規タブ作成FLG
        /// </summary>
        public bool IsMakeNewTab = false;

        /// <summary>
        /// タブ終了FLG
        /// </summary>
        public bool IsCloseTab = false;

        /// <summary>
        /// 終了処理実行FLG
        /// </summary>
        private bool IsDisposable = true;

        #endregion

        /// <summary>
        /// TabControl内に表示するデータ
        /// </summary>
        /// <param name="header">ヘッダ</param>
        /// <param name="content">表示データ</param>
        public TabItemData(string header, object content)
        {

            Header = header;
            Content = content;

            if (content is Page page
                && page.DataContext is MVVM::TabViewModelBase viewModel
                && (viewModel.ClosePageAction == null
                    || viewModel.AddPageAction == null))
            { 

                    viewModel.ClosePageAction = new Action(() => CloseTab());
                    viewModel.AddPageAction = new Action<string, object, bool>(
                        (string newTabName, object newContent, bool isDisposable) => 
                        {

                            AddTab(newTabName, newContent, isDisposable);

                        });

            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (IsDisposable && Content is IDisposable content)
            {
                content.Dispose();
            }

            Content = null;

        }

        /// <summary>
        /// タブを閉じる
        /// </summary>
        private void CloseTab()
        {

            Dispose();
            IsCloseTab = true;
            CallPropertyChanged("CallCloseTabItem");

        }

        /// <summary>
        /// タブを追加する
        /// </summary>
        /// <param name="tabName">追加タブに表示するデータ名</param>
        /// <param name="content">追加タブに表示するデータ</param>
        /// <param name="isDisposable">終了処理を行うか</param>
        private void AddTab(string tabName, object content, bool isDisposable)
        {

            IsMakeNewTab = true;
            NewTab = new TabItemData(tabName, content) { IsDisposable = isDisposable };
            CallPropertyChanged("CallAddTabItem");

        }

        /// <summary>
        /// データが等しいかチェック
        /// </summary>
        public override bool Equals(object obj)
        {

            if (obj is TabItemData tabItem)
            {
                return CreateDate.Equals(tabItem.CreateDate);
            }
            else
            {
                return false;
            }

        }
    }

}
