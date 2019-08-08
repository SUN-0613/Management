using AYam.Common.MVVM;
using Management.Data.Schedule;
using Model = Management.Forms.Model.Menu;
using Class = Management.Forms.Model.Menu.Class;
using Calendar = Management.Pages.View.Calendar;
using Job = Management.Pages.View.JobList;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Management.Forms.ViewModel.Menu
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
        private Model::Calendar _Calendar;

        /// <summary>
        /// 現在日時.Model
        /// </summary>
        private Model::Timer _Timer;

        /// <summary>
        /// スケジュール.Model
        /// </summary>
        private Model::Schedule _Schedule;

        /// <summary>
        /// タブ.Model
        /// </summary>
        private Model::TabItem _TabItem;

        #endregion

        #region Property

        /// <summary>
        /// カレンダー
        /// </summary>
        public object Calender { get { return _Calendar.Page; } }

        /// <summary>
        /// 現在日時
        /// </summary>
        public DateTime Now { get { return _Timer.Now; } }

        /// <summary>
        /// 予定一覧
        /// </summary>
        public ObservableCollection<ScheduleInfo> Schedules { get { return _Schedule.Schedules; } }

        /// <summary>
        /// タブ一覧
        /// </summary>
        public ObservableCollection<Class::TabItemData> TabItems
        {
            get { return _TabItem.TabItems; }
            set { _TabItem.TabItems = value; }
        }

        /// <summary>
        /// 選択しているタブ
        /// </summary>
        public int SelectedTabIndex
        {
            get { return _TabItem.SelectedTabIndex; }
            set
            {
                _TabItem.SelectedTabIndex = value;
                CallPropertyChanged(nameof(SelectedTabIndex));
            }
        }

        /// <summary>
        /// 表示するスケジュール一覧の切り替えコマンド
        /// </summary>
        public DelegateCommand<string> ChangeListCommand { get { return _Schedule.ChangeListCommand; } }

        /// <summary>
        /// ファイルメニューコマンド
        /// </summary>
        public DelegateCommand<string> FileCommand
        {
            get
            {

                return new DelegateCommand<string>(
                    (parameter) =>
                    {
                        switch (parameter)
                        {

                            case "path":
                                CallPropertyChanged("CallPath");
                                break;

                            case "quit":
                                CallPropertyChanged("CallQuit");
                                break;

                            default:
                                break;

                        }

                    }
                    , () => true);

            }
        }

        /// <summary>
        /// 常用処理コマンド
        /// </summary>
        public DelegateCommand<string> ProcessCommand
        {
            get
            {

                int index = -1;

                return new DelegateCommand<string>(
                    (parameter) =>
                    {
                        switch (parameter)
                        {

                            case "calendar":    // カレンダー

                                index = _TabItem.IsOpenTabItemSameTitle(Properties.Title.Calendar);

                                if (index.Equals(-1))
                                {
                                    _TabItem.AddTabItem(Properties.Title.Calendar, new Calendar::Calendar());
                                }
                                else
                                {
                                    SelectedTabIndex = index;
                                }

                                break;

                            case "job":         // ジョブ一覧

                                index = _TabItem.IsOpenTabItemSameTitle(Properties.Title.JobList);

                                if (index.Equals(-1))
                                {
                                    _TabItem.AddTabItem(Properties.Title.JobList, new Job::JobList());
                                }
                                else
                                {
                                    SelectedTabIndex = index;
                                }
                                
                                break;

                            default:
                                break;
                        }

                    }
                    , () => true);

            }
        }

        /// <summary>
        /// マスタ情報メニューコマンド
        /// </summary>
        public DelegateCommand<string> MasterCommand
        {
            get
            {

                return new DelegateCommand<string>(
                    (parameter) => 
                    {
                        switch (parameter)
                        {

                            case "master":  // マスタ情報
                                CallPropertyChanged("CallMaster");
                                break;

                            case "client":  // 取引先情報
                                CallPropertyChanged("CallClient");
                                break;

                            default:
                                break;
                        }
                    }
                    ,() => true);

            }
        }

        #endregion

        /// <summary>
        /// メニュー.ViewModel
        /// </summary>
        public MainMenu()
        {

            _Calendar = new Model::Calendar();

            _Timer = new Model::Timer();
            _Timer.PropertyChanged += OnTimerPropertyChanged;

            _Schedule = new Model::Schedule();

            _TabItem = new Model::TabItem();
            _TabItem.PropertyChanged += OnTabItemPropertyChanged;

            // 初期値表示
            _TabItem.AddTabItem(Properties.Title.Calendar, new Calendar::Calendar());
            _TabItem.AddTabItem(Properties.Title.JobList, new Job::JobList());
            SelectedTabIndex = 0;

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

            if (_Schedule != null)
            {
                _Schedule.Dispose();
                _Schedule = null;
            }

            if (_TabItem != null)
            {
                _TabItem.PropertyChanged -= OnTabItemPropertyChanged;
                _TabItem.Dispose();
                _TabItem = null;
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

        /// <summary>
        /// タブ.Modelプロパティ変更イベント
        /// </summary>
        private void OnTabItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallSelectedTabItem":
                    CallPropertyChanged(nameof(SelectedTabIndex));
                    break;

                default:
                    break;

            }

        }

    }

}
