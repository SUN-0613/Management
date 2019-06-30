using AYam.Common.MVVM;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Management.Pages.Model.Calendar
{

    /// <summary>
    /// カレンダー切替用タイマー.Model
    /// </summary>
    public class Timer : ViewModelBase, IDisposable
    {

        /// <summary>
        /// タイマ処理
        /// </summary>
        private DispatcherTimer _Timer;

        /// <summary>
        /// 起動日付の保持
        /// </summary>
        private DateTime _SaveToday = DateTime.Today;

        /// <summary>
        /// カレンダー切替用タイマー.Model
        /// </summary>
        public Timer()
        {

            _Timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            _Timer.Tick += new EventHandler(Timer_Tick);

            Task.Run(() => { _Timer.Start(); });

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Timer != null)
            {

                if (_Timer.IsEnabled)
                {
                    _Timer.Stop();
                }

                _Timer = null;

            }

        }

        /// <summary>
        /// タイマイベント
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {

            // 日付が切り替わったら通知
            if (!_SaveToday.Equals(DateTime.Today))
            {

                _SaveToday = DateTime.Today;
                CallPropertyChanged("CallDateTime");

            }

        }

    }

}
