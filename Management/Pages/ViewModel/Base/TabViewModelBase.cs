using AYam.Common.MVVM;
using System;

namespace Management.Pages.ViewModel.Base
{

    /// <summary>
    /// タブ制御用ViewModel基幹
    /// </summary>
    public class TabViewModelBase : ViewModelBase
    {

        /// <summary>
        /// ページを閉じるアクション
        /// </summary>
        public Action ClosePageAction;

        /// <summary>
        /// ページを追加するアクション
        /// 第1引数：タブに表記する名称
        /// 第2引数：タブ内容(Page等)
        /// </summary>
        public Action<string, object> AddPageAction;

    }

}
