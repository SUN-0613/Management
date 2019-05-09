using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Management.UserControls
{

    /// <summary>
    /// カレンダ日付枠ユーザコントロール
    /// </summary>
    public partial class Day : UserControl
    {

        #region DependencyProperty

        /// <summary>
        /// 日付依存プロパティ
        /// </summary>
        public static readonly DependencyProperty DateProperty
            = DependencyProperty.Register(
                nameof(Date)
                , typeof(int)
                , typeof(Day)
                , new UIPropertyMetadata(0));

        /// <summary>
        /// 日付色依存プロパティ
        /// </summary>
        public static readonly DependencyProperty WeekColorProperty
            = DependencyProperty.Register(
                nameof(WeekColor)
                , typeof(Brush)
                , typeof(Day)
                , new UIPropertyMetadata(Brushes.Black));

        /// <summary>
        /// 予定依存プロパティ
        /// </summary>
        public static readonly DependencyProperty ScheduleProperty
            = DependencyProperty.Register(
                nameof(Schedule)
                , typeof(string)
                , typeof(Day)
                , new UIPropertyMetadata(string.Empty));

        #endregion

        #region Property

        /// <summary>
        /// 日付
        /// </summary>
        public int Date
        {
            get { return (int)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        /// <summary>
        /// 日付色
        /// 日曜：赤
        /// 土曜：青
        /// 他：黒
        /// </summary>
        public Brush WeekColor
        {
            get { return (Brush)GetValue(WeekColorProperty); }
            set { SetValue(WeekColorProperty, value); }
        }

        /// <summary>
        /// 予定
        /// </summary>
        public string Schedule
        {
            get { return (string)GetValue(ScheduleProperty); }
            set { SetValue(ScheduleProperty, value); }
        }

        #endregion

        /// <summary>
        /// カレンダ日付枠ユーザコントロール
        /// </summary>
        public Day()
        {

            InitializeComponent();

        }

    }

}
