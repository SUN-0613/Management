using System;

namespace Management.Forms.Model
{

    /// <summary>
    /// メニュー.Model
    /// </summary>
    public class MainMenu : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// カレンダー
        /// </summary>
        public Pages.View.Calendar Calendar;

        #endregion

        /// <summary>
        /// メニュー.Model
        /// </summary>
        public MainMenu()
        {

            Calendar = new Pages.View.Calendar();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Calendar != null)
            {
                Calendar.Dispose();
                Calendar = null;
            }

        }

    }
}
