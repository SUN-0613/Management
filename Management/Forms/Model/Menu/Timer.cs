using AYam.Common.MVVM;
using System;
using System.Windows.Threading;

namespace Management.Forms.Model.Menu
{

    /// <summary>
    /// メニュー.タイマー.Model
    /// </summary>
    public class Timer : ViewModelBase, IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// 現在日時
        /// </summary>
        public DateTime Now { get; set; } = DateTime.Now;

        #endregion

        /// <summary>
        /// タイマ処理
        /// </summary>
        private DispatcherTimer _Timer;

        /// <summary>
        /// メニュー.タイマー.Model
        /// </summary>
        public Timer()
        {

            _Timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = new TimeSpan(0, 0, 0, 0, 100)
            };
            _Timer.Tick += new EventHandler(Timer_Tick);
            _Timer.Start();

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
            Now = DateTime.Now;
            CallPropertyChanged("CallDateTime");
        }

    }

}
