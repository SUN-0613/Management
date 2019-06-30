using AYam.Common.MVVM;
using System.Windows.Media;

namespace Management.Pages.Model.Calendar.Class
{

    /// <summary>
    /// 日付情報クラス
    /// </summary>
    public class DayInfo : ViewModelBase
    {

        #region Property

        /// <summary>
        /// 日付
        /// </summary>
        public string DayNumber
        {
            get { return _DayNumber; }
            set
            {
                if (_DayNumber == null || !_DayNumber.Equals(value))
                {
                    _DayNumber = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 当日予定
        /// </summary>
        public string Schedule
        {
            get { return _Schedule; }
            set
            {
                if (_Schedule == null || !_Schedule.Equals(value))
                {
                    _Schedule = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 文字色
        /// </summary>
        public Brush Foreground
        {
            get { return _Foreground; }
            set
            {
                if (!_Foreground.Equals(value))
                {
                    _Foreground = value;
                    CallPropertyChanged();
                }
            }
        }

        public Brush Background
        {
            get { return _Background; }
            set
            {
                if (!_Background.Equals(value))
                {
                    _Background = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        /// <summary>
        /// 日付
        /// </summary>
        private string _DayNumber = string.Empty;

        /// <summary>
        /// 当日予定
        /// </summary>
        private string _Schedule = string.Empty;

        /// <summary>
        /// 文字色
        /// </summary>
        private Brush _Foreground = Brushes.Black;

        /// <summary>
        /// 背景色
        /// </summary>
        private Brush _Background = Brushes.White;

        /// <summary>
        /// 日付情報クラス
        /// </summary>
        public DayInfo()
        { }

        /// <summary>
        /// 日付情報クラス
        /// </summary>
        /// <param name="dayNumber">日付</param>
        /// <param name="schedule">予定</param>
        public DayInfo(string dayNumber, string schedule)
        {

            DayNumber = dayNumber;
            Schedule = schedule;

        }

    }

}
