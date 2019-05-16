using System;

namespace Management.Data.Info
{

    /// <summary>
    /// 予定情報
    /// </summary>
    public class ScheduleInfo
    {

        /// <summary>
        /// 予定日
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 詳細
        /// </summary>
        public string Detail { get; private set; }

        /// <summary>
        /// 予定情報
        /// </summary>
        /// <param name="date">予定日</param>
        /// <param name="name">名称</param>
        /// <param name="detail">詳細</param>
        public ScheduleInfo(DateTime date, string name, string detail)
        {

            Date = date;
            Name = name;
            Detail = detail;

        }

    }

}
