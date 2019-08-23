using Management.Data.Info;

namespace Management.Pages.ViewModel.Base
{

    /// <summary>
    /// 印刷Page用
    /// </summary>
    public abstract class TabPrintViewModelBase : TabViewModelBase
    {

        /*
        /// <summary>
        /// Pageの長一辺
        /// </summary>
        public double LongSide { get { return 1123d; } }

        /// <summary>
        /// Pageの短一辺
        /// </summary>
        public double ShortSide { get { return 794d; } }
        */

        /// <summary>
        /// 書類情報をModelより取得
        /// </summary>
        /// <returns>書類情報</returns>
        public abstract DataFileInfo GetDataFileInfo();

        /// <summary>
        /// 印刷実行
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public abstract string ExecutePrint();

    }

}
