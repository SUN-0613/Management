using AYam.Common.MVVM;
using Management.Pages.Model.Class;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Management.Pages.ViewModel
{

    /// <summary>
    /// Calendar.ViewModel
    /// </summary>
    public class Calendar : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// Calendar.Model
        /// </summary>
        private Model.Calendar _Model;

        /// <summary>
        /// 切替用タイマ.Model
        /// </summary>
        private Model.Timer _TimerModel;

        #endregion

        #region Property

        /// <summary>
        /// 月変更コマンド
        /// </summary>
        public DelegateCommand<string> ChangeMonthCommand
        {
            get
            {

                if (_Model.ChangeMonthCommand == null)
                {

                    _Model.ChangeMonthCommand = new DelegateCommand<string>
                        (
                            (parameter) =>
                            {

                                switch (parameter)
                                {

                                    case "previous":
                                        SelectMonth = SelectMonth.AddMonths(-1);
                                        break;

                                    case "next":
                                        SelectMonth = SelectMonth.AddMonths(1);
                                        break;

                                    default:
                                        break;

                                }

                            }
                            , () => true
                        );

                }

                return _Model.ChangeMonthCommand;

            }
        }

        /// <summary>
        /// 日付一覧
        /// </summary>
        public ObservableCollection<DayInfo> Days
        {
            get { return _Model.Days; }
            set
            {
                _Model.Days = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 選択年月
        /// </summary>
        public DateTime SelectMonth
        {
            get { return _Model.SelectMonth; }
            set
            {

                if (!_Model.SelectMonth.Equals(value))
                {

                    _Model.SelectMonth = value;
                    CallPropertyChanged();

                    _Model.SetDaysPosition();

                }

            }
        }

        #endregion

        /// <summary>
        /// Calendar.ViewModel
        /// </summary>
        public Calendar()
        {

            _Model = new Model.Calendar();
            _TimerModel = new Model.Timer();
            _TimerModel.PropertyChanged += OnTimerPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Model != null)
            {

                _Model.Dispose();
                _Model = null;

            }

            if (_TimerModel != null)
            {
                _TimerModel.PropertyChanged -= OnTimerPropertyChanged;
                _TimerModel.Dispose();
                _TimerModel = null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void OnTimerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallDateTime":
                    _Model.SetDaysPosition();
                    break;

                default:
                    break;

            }

        }

    }

}
