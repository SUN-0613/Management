using AYam.Common.MVVM;
using System;

namespace Management.Forms.ViewModel
{

    /// <summary>
    /// メニュー.ViewModel
    /// </summary>
    public class MainMenu : ViewModelBase, IDisposable
    {

        /// <summary>
        /// Model
        /// </summary>
        private Model.MainMenu _Model;

        #region Property

        /// <summary>
        /// カレンダー
        /// </summary>
        public Pages.View.Calendar Calender { get { return _Model.Calendar; } }

        #endregion

        /// <summary>
        /// メニュー.ViewModel
        /// </summary>
        public MainMenu()
        {

            _Model = new Model.MainMenu();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Model != null)
            {
                _Model.Dispose();
                _Model = null;
            }

        }

    }

}
