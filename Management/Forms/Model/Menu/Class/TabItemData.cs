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
    public class TabItemData : IDisposable
    {

        #region Property

        public DateTime CreateDateTime = DateTime.Now;

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

        #endregion

        /// <summary>
        /// タブを閉じるアクション
        /// </summary>
        private Action<TabItemData> _CloseTabItemAction;

        /// <summary>
        /// タブを追加するアクション
        /// </summary>
        private Action<TabItemData> _AddTabItemAction;

        /// <summary>
        /// TabControl内に表示するデータ
        /// </summary>
        /// <param name="header">ヘッダ</param>
        /// <param name="content">表示データ</param>
        /// <param name="closeTabItemAction">タブを閉じるアクション</param>
        /// <param name="addTabItemAction">タブを追加するアクション</param>
        public TabItemData(string header, object content, Action<TabItemData> closeTabItemAction, Action<TabItemData> addTabItemAction)
        {

            Header = header;
            Content = content;
            _CloseTabItemAction = closeTabItemAction;
            _AddTabItemAction = addTabItemAction;

            if (content is Page page
                && page.DataContext is MVVM::TabViewModelBase viewModel)
            { 

                    viewModel.ClosePageAction = new Action(() => CloseTab());
                    viewModel.AddPageAction = new Action<string, object>(
                        (string newTabName, object newContent) =>  { AddTab(newTabName, newContent); });

            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Content is IDisposable content)
            {
                content.Dispose();
            }

            Content = null;

            _CloseTabItemAction = null;
            _AddTabItemAction = null;

        }

        /// <summary>
        /// タブを閉じる
        /// </summary>
        private void CloseTab()
        {

            _CloseTabItemAction(this);
            Dispose();

        }

        /// <summary>
        /// タブを追加する
        /// </summary>
        /// <param name="tabName">追加タブに表示するデータ名</param>
        /// <param name="content">追加タブに表示するデータ</param>
        private void AddTab(string tabName, object content)
        {
            _AddTabItemAction(new TabItemData(tabName, content, _CloseTabItemAction, _AddTabItemAction));
        }

        /// <summary>
        /// データが等しいかチェック
        /// </summary>
        public override bool Equals(object obj)
        {

            if (obj is TabItemData tabItem)
            {
                return CreateDateTime.Equals(tabItem.CreateDateTime);
            }
            else
            {
                return false;
            }

        }
    }

}
