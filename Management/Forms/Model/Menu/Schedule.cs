using AYam.Common.MVVM;
using Management.Data.Schedule;
using System;
using System.Collections.ObjectModel;

namespace Management.Forms.Model.Menu
{

    /// <summary>
    /// メニュー.スケジュール.Model
    /// </summary>
    public class Schedule : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// 予定一覧
        /// </summary>
        public ObservableCollection<ScheduleInfo> Schedules { get; private set; }

        /// <summary>
        /// 表示するスケジュール一覧の切り替えコマンド
        /// </summary>
        public DelegateCommand<string> ChangeListCommand;

        #endregion

        /// <summary>
        /// 基準日
        /// </summary>
        private DateTime BaseDate = DateTime.Now;

        /// <summary>
        /// メニュー.スケジュール.Model
        /// </summary>
        public Schedule()
        {

            MakeSchedules(0);
            
            ChangeListCommand = new DelegateCommand<string>(
                (parameter) =>
                {

                    switch (parameter)
                    {
                        case "next":
                            MakeSchedules(10);
                            break;

                        case "previous":
                            MakeSchedules(-10);
                            break;

                        default:
                            break;
                    }

                }
                , () => true);

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Schedules != null)
            {
                Schedules.Clear();
                Schedules = null;
            }

        }

        /// <summary>
        /// スケジュール一覧の作成
        /// </summary>
        /// <param name="addDays">基準日への加算値</param>
        private void MakeSchedules(int addDays)
        {

            BaseDate = BaseDate.AddDays(addDays);

            if (Schedules == null)
            {
                Schedules = new ObservableCollection<ScheduleInfo>();
            }
            else
            {
                Schedules.Clear();
            }

            /*
             * 後ほど追加 
             */

        }

    }

}
