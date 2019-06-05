using AYam.Common.MVVM;
using Management.Pages.Model.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Threading.Tasks;

namespace Management.Pages.Model
{

    /// <summary>
    /// Calendar.Model
    /// </summary>
    public class Calendar : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// 月変更コマンド
        /// </summary>
        public DelegateCommand<string> ChangeMonthCommand;

        /// <summary>
        /// 選択年月
        /// </summary>
        public DateTime SelectMonth = DateTime.Now;

        /// <summary>
        /// 日付一覧
        /// </summary>
        public ObservableCollection<DayInfo> Days;

        #endregion

        /// <summary>
        /// 祝日コレクション
        /// </summary>
        private SortedDictionary<DateTime, Holiday> _Holidays;

        /// <summary>
        /// 祝日を取得している年
        /// </summary>
        private int _YearGetHoliday;

        /// <summary>
        /// Calendar.Model
        /// </summary>
        public Calendar()
        {

            // 日付情報作成
            Days = new ObservableCollection<DayInfo>();

            for (int iLoop = 0; iLoop < 6; iLoop++)
            {

                for (int jLoop = 0; jLoop < 7; jLoop++)
                {
                    Days.Add(new DayInfo());
                }

            }

            // 選択年の祝日取得
            _YearGetHoliday = SelectMonth.Year;
            Class.HolidayMethod.SetHolidays(_YearGetHoliday, ref _Holidays);

            // 選択年月の日付情報セット
            SetDaysPosition();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Days != null)
            {
                Days.Clear();
                Days = null;
            }

        }

        /// <summary>
        /// 日付の配置決め
        /// </summary>
        public async void SetDaysPosition()
        {

            await Task.Run(() =>
            {

                // 初期化：全てを空白にする
                Parallel.For(0, Days.Count, iLoop => 
                {
                    Days[iLoop].DayNumber = "";
                    Days[iLoop].Schedule = null;
                });

                // 月末日の取得
                int daysInMonth = DateTime.ParseExact(SelectMonth.AddMonths(1).ToString("yyyyMM") + "01", "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo).AddDays(-1).Day;
                int startDayOfWeek = (int)DateTime.ParseExact(SelectMonth.ToString("yyyyMM") + "01", "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo).DayOfWeek;

                // 選択年の祝日取得
                if (!_YearGetHoliday.Equals(SelectMonth.Year))
                {
                    _YearGetHoliday = SelectMonth.Year;
                    Class.HolidayMethod.SetHolidays(_YearGetHoliday, ref _Holidays);
                }

                // 1日から月末日までセット
                Parallel.For(0, daysInMonth, iLoop => 
                {

                    int index = startDayOfWeek + iLoop;
                    int day = iLoop + 1;

                    Days[index].DayNumber = day.ToString();

                    DateTime date = new DateTime(SelectMonth.Year, SelectMonth.Month, day);

                    switch (date.DayOfWeek)
                    {

                        case DayOfWeek.Sunday:
                            Days[index].Foreground = Brushes.Red;
                            break;

                        case DayOfWeek.Saturday:
                            Days[index].Foreground = Brushes.Blue;
                            break;

                        default:    // 平日
                            Days[index].Foreground = Brushes.Black;
                            break;

                    }

                    // 祝日チェック
                    if (_Holidays.ContainsKey(date))
                    {
                        Days[index].Foreground = Brushes.Red;
                        Days[index].Schedule = _Holidays[date].Name;
                    }

                    /* 後ほど実装

                    Days[index].Schedule = "";  

                    */

                });

            });

        }

    }

}
