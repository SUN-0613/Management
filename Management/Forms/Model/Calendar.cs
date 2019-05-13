using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Forms.Model
{

    /// <summary>
    /// メニュー.カレンダー.Model
    /// </summary>
    public class Calendar : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// カレンダー
        /// </summary>
        public Pages.View.Calendar Page;

        #endregion

        /// <summary>
        /// メニュー.カレンダー.Model
        /// </summary>
        public Calendar()
        {

            Page = new Pages.View.Calendar();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Page != null)
            {
                Page.Dispose();
                Page = null;
            }

        }

    }

}
