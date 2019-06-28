using Management.Forms.Model.Menu.Class;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Management.Forms.Model.Menu
{

    /// <summary>
    /// メニュー.タブ.Model
    /// </summary>
    public class TabItem : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// タブ一覧
        /// </summary>
        public ObservableCollection<TabItemData> TabItems { get; set; }

        #endregion

        /// <summary>
        /// メニュー.タブ.Model
        /// </summary>
        public TabItem()
        {

            TabItems = new ObservableCollection<TabItemData>();

#if DEBUG

            var tab = new TabItemData("テスト表示", "TEST");
            tab.PropertyChanged += OnPropertyChanged;
            TabItems.Add(tab);

#endif

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (TabItems != null)
            {
                
                for (int iLoop = 0; iLoop < TabItems.Count; iLoop++)
                {

                    TabItems[iLoop].PropertyChanged -= OnPropertyChanged;
                    TabItems[iLoop].Dispose();
                    TabItems[iLoop] = null;

                }

                TabItems.Clear();
                TabItems = null;

            }

        }

        /// <summary>
        /// 閉じるコマンド実行イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallTabItemClose":

                    // 対象はDispose()実行済みなので、Contentがnullのものを探して削除
                    for (int iLoop = TabItems.Count - 1; iLoop >= 0; iLoop--)
                    {

                        if (TabItems[iLoop].Content == null)
                        {
                            TabItems.RemoveAt(iLoop);
                            break;
                        }

                    }

                    break;

                default:
                    break;

            }

        }

    }

}
