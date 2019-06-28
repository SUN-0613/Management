using AYam.Common.MVVM;
using System;

namespace Management.Forms.Model.Menu.Class
{

    /// <summary>
    /// TabControl内に表示するデータ
    /// </summary>
    public class TabItemData : ViewModelBase, IDisposable
    {

        #region Property

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
                        Dispose();
                        CallPropertyChanged("CallTabItemClose");
                    }
                    ,()=>true);
            }
        }

        #endregion

        /// <summary>
        /// TabControl内に表示するデータ
        /// </summary>
        public TabItemData()
        { }

        /// <summary>
        /// TabControl内に表示するデータ
        /// </summary>
        /// <param name="header">ヘッダ</param>
        /// <param name="content">表示データ</param>
        public TabItemData(string header, string content)
        {

            Header = header;
            Content = content;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Content is IDisposable content)
            {
                content.Dispose();
                content = null;
            }

#if DEBUG

            if (Content as string != null)
            {
                Content = null;
            }

#endif

        }

    }

}
