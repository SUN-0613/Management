using AYam.Common.MVVM;
using Management.Class;
using System;
using System.Collections.ObjectModel;
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
        private Model.Menu.Calendar _Calendar;

        /// <summary>
        /// 現在日時.Model
        /// </summary>
        private Model.Menu.Timer _Timer;

        /// <summary>
        /// スケジュール.Model
        /// </summary>
        private Model.Menu.Schedule _Schedule;

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

        /// <summary>
        /// 予定一覧
        /// </summary>
        public ObservableCollection<ScheduleInfo> Schedules { get { return _Schedule.Schedules; } }

        /// <summary>
        /// 表示するスケジュール一覧の切り替えコマンド
        /// </summary>
        public DelegateCommand<string> ChangeListCommand { get { return _Schedule.ChangeListCommand; } }

        #endregion

        /// <summary>
        /// メニュー.ViewModel
        /// </summary>
        public MainMenu()
        {

            _Calendar = new Model.Menu.Calendar();

            _Timer = new Model.Menu.Timer();
            _Timer.PropertyChanged += OnTimerPropertyChanged;

            _Schedule = new Model.Menu.Schedule();

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
