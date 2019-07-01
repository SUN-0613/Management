using AYam.Common.MVVM;
using Model = Management.Pages.Model.JobList;
using System;

namespace Management.Pages.ViewModel.JobList
{

    /// <summary>
    /// ジョブ一覧.ViewModel
    /// </summary>
    public class JobList : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// ジョブ一覧.Model
        /// </summary>
        private Model::JobList _Model;

        #endregion

        #region Property

        #endregion

        /// <summary>
        /// ジョブ一覧.ViewModel
        /// </summary>
        public JobList()
        {

            _Model = new Model::JobList();

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

        }

    }

}
