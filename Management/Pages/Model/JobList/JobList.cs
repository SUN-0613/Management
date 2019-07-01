using Management.Data.File;
using Management.Data.Info;
using System;
using System.Collections.ObjectModel;

namespace Management.Pages.Model.JobList
{

    /// <summary>
    /// ジョブ一覧.Model
    /// </summary>
    public class JobList : IDisposable
    {

        /// <summary>
        /// ジョブ一覧ファイル
        /// </summary>
        private JobListFile _File;

        #region ViewModel.Property

        /// <summary>
        /// ジョブ一覧
        /// </summary>
        public ObservableCollection<JobDetail> Jobs { get; set; }

        #endregion

        /// <summary>
        /// ジョブ一覧.Model
        /// </summary>
        public JobList()
        {

            _File = new JobListFile();

            Jobs = new ObservableCollection<JobDetail>();

            for (int iLoop = 0; iLoop < _File.Jobs.Count; iLoop++)
            {
                Jobs.Add(new JobDetail(_File.Jobs[iLoop]));
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_File != null)
            {
                _File.Dispose();
                _File = null;
            }

        }

    }

}
