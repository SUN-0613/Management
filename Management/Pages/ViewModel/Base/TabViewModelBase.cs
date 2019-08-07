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
        /// 第3引数：タブを閉じる際にDispose処理を行うか
        /// </summary>
        public Action<string, object, bool> AddPageAction;

    }

}
