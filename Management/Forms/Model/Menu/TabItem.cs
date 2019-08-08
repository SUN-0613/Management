using AYam.Common.MVVM;
using Management.Forms.Model.Menu.Class;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Management.Forms.Model.Menu
{

    /// <summary>
    /// メニュー.タブ.Model
    /// </summary>
    public class TabItem : ViewModelBase, IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// タブ一覧
        /// </summary>
        public ObservableCollection<TabItemData> TabItems { get; set; } = new ObservableCollection<TabItemData>();

        /// <summary>
        /// 選択タブIndex
        /// </summary>
        public int SelectedTabIndex = -1;

        #endregion

        /// <summary>
        /// メニュー.タブ.Model
        /// </summary>
        public TabItem()
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (TabItems != null)
            {
                
                foreach (var tabItem in TabItems)
                {
                    tabItem.Dispose();
                }

                TabItems.Clear();
                TabItems = null;

            }

        }

        /// <summary>
        /// 同内容のタブを開いているか
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <returns>
        /// -1:該当タブ無し
        /// 他：該当タブIndex
        /// </returns>
        public int IsOpenTabItemSameTitle(string title)
        {

            int index = -1;

            for (int iLoop = 0; iLoop < TabItems.Count; iLoop++)
            {

                var tabItem = TabItems[iLoop];

                if (tabItem.Header.Equals(title))
                {
                    index = iLoop;
                    break;
                }

            }

            return index;

        }

        /// <summary>
        /// 同内容のタブを開いているか
        /// </summary>
        /// <param name="title">表示内容</param>
        /// <returns>
        /// True:タブ有
        /// False:タブ無
        /// </returns>
        public bool IsOpenTabItemSameContent(object content)
        {

            bool isOpen = false;

            foreach (var tabItem in TabItems)
            {
                if (tabItem.Content.Equals(content))
                {
                    isOpen = true;
                    break;
                }
            }

            return isOpen;

        }

        /// <summary>
        /// タブを閉じる
        /// </summary>
        /// <param name="tabItem">タブ表示データ</param>
        private void CloseTabItem(TabItemData tabItem)
        {
            TabItems.Remove(tabItem);
        }

        /// <summary>
        /// タブ追加
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="content">表示内容</param>
        public void AddTabItem(string title, object content)
        {
            AddTabItem(new TabItemData(title, content, CloseTabItem, AddTabItem));
        }

        /// <summary>
        /// タブ追加
        /// </summary>
        /// <param name="tabItem">タブ表示データ</param>
        private void AddTabItem(TabItemData tabItem)
        {

            TabItems.Add(tabItem);

            SelectedTabIndex = TabItems.Count - 1;
            CallPropertyChanged("CallSelectedTabItem");

        }

    }

}
