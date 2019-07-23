using Management.Data.File;
using ViewModel = Management.Pages.ViewModel.JobList;
using System;
using System.Collections.ObjectModel;

namespace Management.Pages.Model.JobList
{

    /// <summary>
    /// ジョブ一覧.Model
    /// </summary>
    public class JobList : IDisposable
    {

        #region File

        /// <summary>
        /// ジョブ一覧ファイル
        /// </summary>
        private JobListFile _File = new JobListFile();

        #endregion

        #region ViewModel.Property

        /// <summary>
        /// ジョブ詳細.ViewModel一覧
        /// </summary>
        public ObservableCollection<ViewModel::JobDetail> Details { get; set; }

        #endregion

        /// <summary>
        /// ジョブ一覧.Model
        /// </summary>
        public JobList()
        {

            Details = new ObservableCollection<ViewModel::JobDetail>();

            foreach (var job in _File.Jobs)
            {
                Details.Add(new ViewModel::JobDetail(job, Save));
            }

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

            _File.Dispose();
            _File = null;

        }

        /// <summary>
        /// データ保存
        /// </summary>
        public void Save()
        {
            _File.Save();
        }

        /// <summary>
        /// 新規ジョブの作成
        /// </summary>
        public void AddNewJob()
        {

            var job = new Data.Info.Job() { CreateDate = DateTime.Now };

            _File.Jobs.Add(job);
            Details.Add(new ViewModel::JobDetail(job, Save));

        }

        /// <summary>
        /// 指定したジョブを削除
        /// </summary>
        /// <param name="detail">指定したジョブの詳細</param>
        public void RemoveJob(ViewModel::JobDetail detail)
        {

            var index = Details.IndexOf(detail);

            if (!index.Equals(-1))
            {

                _File.Jobs.RemoveAt(index);
                Details.RemoveAt(index);

                Save();

            }

        }

    }

}
