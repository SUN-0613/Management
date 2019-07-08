using AYam.Common.MVVM;
using System;
using System.Collections.ObjectModel;

namespace Management.Pages.ViewModel.JobList
{

    /// <summary>
    /// ジョブ一覧.ViewModel
    /// </summary>
    public class JobList : ViewModelBase, IDisposable
    {

        #region Property

        /// <summary>
        /// ジョブ詳細.ViewModel一覧
        /// </summary>
        public ObservableCollection<JobDetail> Details;

        #endregion

        /// <summary>
        /// ジョブ一覧.ViewModel
        /// </summary>
        public JobList()
        {
            Details = new ObservableCollection<JobDetail>();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Details != null)
            {

                foreach (var detail in Details)
                {
                    detail.Dispose();
                }

                Details.Clear();
                Details = null;
                
            }

        }

    }

}
