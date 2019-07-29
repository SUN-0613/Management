using AYam.Common.MVVM;
using Model = Management.Pages.Model.JobList;
using Management.Pages.ViewModel.Base;
using System;
using System.Collections.ObjectModel;

namespace Management.Pages.ViewModel.JobList
{

    /// <summary>
    /// ジョブ一覧.ViewModel
    /// </summary>
    public class JobList : TabViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// ジョブ一覧.Model
        /// </summary>
        private Model::JobList _Model = new Model::JobList();

        #endregion

        #region Property

        /// <summary>
        /// ジョブ詳細.ViewModel一覧
        /// </summary>
        public ObservableCollection<JobDetail> Details
        {
            get { return _Model?.Details; }
            set
            {
                if (_Model != null)
                {
                    _Model.Details = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 選択しているジョブ詳細
        /// </summary>
        public JobDetail SelectedDetail { get; set; } = null;

        /// <summary>
        /// 取引先一覧の操作コマンド
        /// </summary>
        public DelegateCommand<string> ListCommand
        {
            get
            {

                return new DelegateCommand<string>
                    (
                        (parameter) =>
                        {

                            switch (parameter)
                            {

                                case "add":
                                    CallPropertyChanged("CallListAdd");
                                    break;

                                case "remove":

                                    if (SelectedDetail != null)
                                    {
                                        CallPropertyChanged("CallListRemove");
                                    }

                                    break;

                                default:
                                    break;

                            }

                            _Model.Save();

                        }
                        , () => true
                    );

            }
        }

        #endregion

        /// <summary>
        /// ジョブ一覧.ViewModel
        /// </summary>
        public JobList()
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            _Model.Dispose();
            _Model = null;
                
        }

        /// <summary>
        /// 新規ジョブの作成
        /// </summary>
        public void AddNewJob()
        {
            _Model.AddNewJob();
        }

        /// <summary>
        /// 選択したジョブを削除
        /// </summary>
        public void RemoveSelectedJob()
        {
            _Model.RemoveJob(SelectedDetail);
        }

    }

}
