using System;
using System.Collections.Generic;
using System.Linq;

namespace Management.Pages.Model.Class
{

    /// <summary>
    /// 休日の種類
    /// </summary>
    public enum HolidayKind
    {
        /// <summary>
        /// 平日
        /// </summary>
        Weekday = 0,

        /// <summary>
        /// 国民の祝日
        /// </summary>
        Public = 1,

        /// <summary>
        /// 振替休日
        /// </summary>
        Compensating = 2,

        /// <summary>
        /// 国民の休日
        /// </summary>
        National = 3

    }

    /// <summary>
    /// 祝日
    /// </summary>
    /// <remarks>
    /// 参考URL：https://www.atmarkit.co.jp/ait/articles/1507/22/news024.html
    /// </remarks>
    public class Holiday
    {

        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Date;

        /// <summary>
        /// 種類
        /// </summary>
        public HolidayKind Kind;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 祝日の定義
        /// </summary>
        public string Definition;

        /// <summary>
        /// 祝日
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="kind">種類</param>
        /// <param name="name">名称</param>
        /// <param name="definition">祝日の定義</param>
        public Holiday(DateTime date, HolidayKind kind, string name, string definition)
        {
            Date = date;
            Kind = kind;
            Name = name;
            Definition = definition;
        }

    }

    /// <summary>
    /// 祝日取得用メソッド
    /// </summary>
    public static class HolidayMethod
    {

        /// <summary>
        /// 祝日の設定
        /// </summary>
        /// <param name="year">対象年</param>
        /// <param name="holidays">祝日格納ディクショナリ</param>
        /// <remarks>
        /// 2008年以降より有効
        /// 参考URL：https://www.atmarkit.co.jp/ait/articles/1507/22/news024.html
        /// </remarks>
        public static void SetHolidays(int year, ref SortedDictionary<DateTime, Holiday> holidays)
        {

            DateTime date;
            List<Holiday> addList;

            if (holidays == null)
            {
                holidays = new SortedDictionary<DateTime, Holiday>();
            }
            else
            {
                holidays.Clear();
            }

            #region 固定日

            date = new DateTime(year, 1, 1);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "元日", date.ToString("M月d日")));

            date = new DateTime(year, 2, 11);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "建国記念の日", date.ToString("M月d日")));

            if (year > 2019)
            {
                date = new DateTime(year, 2, 23);
                holidays.Add(date, new Holiday(date, HolidayKind.Public, "天皇誕生日", date.ToString("M月d日")));
            }

            date = new DateTime(year, 4, 29);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "昭和の日", date.ToString("M月d日")));

            if (year.Equals(2019))
            {
                date = new DateTime(year, 5, 1);
                holidays.Add(date, new Holiday(date, HolidayKind.Public, "天皇即位", date.ToString("M月d日")));
            }

            date = new DateTime(year, 5, 3);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "憲法記念日", date.ToString("M月d日")));

            if (year >= 2007)
            {
                date = new DateTime(year, 5, 4);
                holidays.Add(date, new Holiday(date, HolidayKind.Public, "みどりの日", date.ToString("M月d日")));
            }

            date = new DateTime(year, 5, 5);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "こどもの日", date.ToString("M月d日")));

            if (year >= 2016)
            {
                date = new DateTime(year, 8, 11);
                holidays.Add(date, new Holiday(date, HolidayKind.Public, "山の日", date.ToString("M月d日")));
            }

            if (year.Equals(2019))
            {
                date = new DateTime(year, 10, 22);
                holidays.Add(date, new Holiday(date, HolidayKind.Public, "即位礼正殿の儀", date.ToString("M月d日")));
            }

            date = new DateTime(year, 11, 3);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "文化の日", date.ToString("M月d日")));

            date = new DateTime(year, 11, 23);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "勤労感謝の日", date.ToString("M月d日")));

            if (year < 2019)
            {
                date = new DateTime(year, 12, 23);
                holidays.Add(date, new Holiday(date, HolidayKind.Public, "天皇誕生日", date.ToString("M月d日")));
            }

            #endregion

            #region 曜日指定

            date = GetNthDayOfWeek(year, 1, 2, DayOfWeek.Monday);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "成人の日", "1月の第2月曜日"));

            date = GetNthDayOfWeek(year, 7, 3, DayOfWeek.Monday);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "海の日", "7月の第3月曜日"));

            date = GetNthDayOfWeek(year, 9, 3, DayOfWeek.Monday);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "敬老の日", "9月の第3月曜日"));

            date = GetNthDayOfWeek(year, 10, 2, DayOfWeek.Monday);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "体育の日", "10月の第2月曜日"));

            #endregion

            #region 天文現象

            date = CalcVernalEquinoxDay(year);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "春分の日", "春分日"));

            date = CalcAutumnalEquinoxDay(year);
            holidays.Add(date, new Holiday(date, HolidayKind.Public, "秋分の日", "秋分日"));

            #endregion

            #region 振替休日

            addList = new List<Holiday>();

            foreach (var holiday in holidays.Values)
            {

                if (holiday.Date.DayOfWeek.Equals(DayOfWeek.Sunday))
                {

                    date = holiday.Date.AddDays(1);

                    while (holidays.ContainsKey(date))
                    {
                        date = date.AddDays(1);
                    }

                    addList.Add(new Holiday(date, HolidayKind.Compensating, "振替休日", string.Format("{0}の振替休日", holiday.Name)));

                }

            }

            for (int iLoop = 0; iLoop < addList.Count; iLoop++)
            {

                var holiday = addList[iLoop];

                if (!holidays.ContainsKey(holiday.Date))
                {
                    holidays.Add(holiday.Date, new Holiday(holiday.Date, holiday.Kind, holiday.Name, holiday.Definition));
                }

            }

            addList.Clear();
            addList = null;

            #endregion

            #region 国民の休日

            addList = new List<Holiday>();

            foreach (var holiday in holidays.Values)
            {

                // 国民の祝日か
                if (holiday.Kind.Equals(HolidayKind.Public))
                {

                    date = holiday.Date.AddDays(2);

                    // 2日後も国民の祝日か
                    if (holidays.ContainsKey(date)
                        && holidays[date].Kind.Equals(HolidayKind.Public))
                    {

                        date = holiday.Date.AddDays(1);

                        // 1日後が日曜でも祝日でもない、平日か
                        if (!date.DayOfWeek.Equals(DayOfWeek.Sunday)
                            && !holidays.ContainsKey(date))
                        {

                            addList.Add(new Holiday(date, HolidayKind.National, "国民の休日", string.Format("{0}と{1}の間の日", holiday.Name, holidays[date.AddDays(1)].Name)));

                        }

                    }

                }

            }

            for (int iLoop = 0; iLoop < addList.Count; iLoop++)
            {

                var holiday = addList[iLoop];

                if (!holidays.ContainsKey(holiday.Date))
                {
                    holidays.Add(holiday.Date, new Holiday(holiday.Date, holiday.Kind, holiday.Name, holiday.Definition));
                }

            }

            addList.Clear();
            addList = null;

            #endregion

        }

        /// <summary>
        /// 第N週指定曜日の日付取得
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="nth">第N週目</param>
        /// <param name="dayOfWeek">曜日</param>
        /// <returns>第N月曜の日付</returns>
        private static DateTime GetNthDayOfWeek(int year, int month, int nth, DayOfWeek dayOfWeek)
        {

            // 指定年月の日数取得
            int days = DateTime.DaysInMonth(year, month);

            // 指定年月の月曜日を取得
            var allMondays = Enumerable.Range(1, days)
                                .Select(d => new DateTime(year, month, d))
                                .Where(dt => dt.DayOfWeek.Equals(dayOfWeek));

            // 第N月曜日を戻す
            return allMondays.ElementAt(nth - 1);

        }

        /// <summary>
        /// 春分の日の計算
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>春分の日</returns>
        /// <remarks>2099年まで有効</remarks>
        private static DateTime CalcVernalEquinoxDay(int year)
        {

            double origin = 20.69115;                       // 2000年の太陽の春分点通過日
            double movement = (year - 2000) * 0.242194;     // 春分点通過日の移動量
            int leapValue = (int)((year - 2000) / 4.0);     // 閏年によるリセット量
            int day = (int)(origin + movement - leapValue); // 春分日

            return new DateTime(year, 3, day);

        }

        /// <summary>
        /// 秋分の日の計算
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>秋分の日</returns>
        /// <remarks>2099年まで有効</remarks>
        private static DateTime CalcAutumnalEquinoxDay(int year)
        {

            double origin = 23.09;                          // 2000年の太陽の秋分点通過日
            double movement = (year - 2000) * 0.242194;     // 秋分点通過日の移動量
            int leapValue = (int)((year - 2000) / 4.0);     // 閏年によるリセット量
            int day = (int)(origin + movement - leapValue); // 秋分日

            return new DateTime(year, 9, day);

        }

    }

}
