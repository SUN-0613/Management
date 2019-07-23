using AYam.Common.MVVM;
using System;

namespace Management.Pages.ViewModel.Base
{
    public class PageViewModelBase : ViewModelBase
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
