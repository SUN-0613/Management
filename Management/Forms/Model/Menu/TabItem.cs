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
        public ObservableCollection<TabItemData> TabItems { get; set; }

        /// <summary>
        /// 選択タブIndex
        /// </summary>
        public int SelectedTabIndex = -1;

        #endregion

        /// <summary>
        /// メニュー.タブ.Model
        /// </summary>
        public TabItem()
        {

            TabItems = new ObservableCollection<TabItemData>();

            //#if DEBUG
            //            AddTabItem("テスト表示", "TEST");
            //#endif

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (TabItems != null)
            {
                
                foreach (var tabItem in TabItems)
                {
                    tabItem.PropertyChanged -= OnPropertyChanged;
                    tabItem.Dispose();
                }

                TabItems.Clear();
                TabItems = null;

            }

        }

        /// <summary>
        /// TabItemDataイベント通知
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallCloseTabItem":    // タブを閉じる

                    foreach (var tabItem in TabItems)
                    {

                        if (tabItem.IsCloseTab)
                        {

                            TabItems.Remove(tabItem);
                            break;

                        }

                    }

                    break;

                case "CallAddTabItem":      // タブを追加

                    // 対象クラス内で表示データを作成済なので、NewTabがnullでないものを探して追加
                    foreach (var tabItem in TabItems)
                    {

                        if (tabItem.IsMakeNewTab)
                        {

                            AddTabItem(tabItem.NewTab);
                            tabItem.IsMakeNewTab = false;

                            break;

                        }

                    }

                    break;

                default:
                    break;

            }

        }

        /// <summary>
        /// タブ追加
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="content">表示内容</param>
        public void AddTabItem(string title, object content)
        {
            AddTabItem(new TabItemData(title, content));
        }

        /// <summary>
        /// タブ追加
        /// </summary>
        /// <param name="tabItem">タブ表示データ</param>
        private void AddTabItem(TabItemData tabItem)
        {

            tabItem.PropertyChanged += OnPropertyChanged;
            TabItems.Add(tabItem);

            SelectedTabIndex = TabItems.Count - 1;
            CallPropertyChanged("CallSelectedTabItem");

        }

    }

}
