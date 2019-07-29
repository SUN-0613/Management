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
        /// </summary>
        public Action<string, object> AddPageAction;

    }

}
