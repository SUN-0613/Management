using AYam.Common.MVVM;
using System;
using System.ComponentModel;

namespace Management.Forms.ViewModel
{

    /// <summary>
    /// メニュー.ViewModel
    /// </summary>
    public class MainMenu : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// カレンダー.Model
        /// </summary>
        private Model.Calendar _Calendar;

        /// <summary>
        /// 現在日時.Model
        /// </summary>
        private Model.Timer _Timer;

        #endregion

        #region Property

        /// <summary>
        /// カレンダー
        /// </summary>
        public Pages.View.Calendar Calender { get { return _Calendar.Page; } }

        /// <summary>
        /// 現在日時
        /// </summary>
        public DateTime Now { get { return _Timer.Now; } }

        #endregion

        /// <summary>
        /// メニュー.ViewModel
        /// </summary>
        public MainMenu()
        {

            _Calendar = new Model.Calendar();

            _Timer = new Model.Timer();
            _Timer.PropertyChanged += OnTimerPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Calendar != null)
            {
                _Calendar.Dispose();
                _Calendar = null;
            }

            if (_Timer != null)
            {
                _Timer.PropertyChanged -= OnTimerPropertyChanged;
                _Timer.Dispose();
                _Timer = null;
            }

        }

        /// <summary>
        /// 現在日時.Modelプロパティ変更イベント
        /// </summary>
        private void OnTimerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallDateTime":
                    CallPropertyChanged(nameof(Now));
                    break;

                default:
                    break;

            }

        }

    }

}
