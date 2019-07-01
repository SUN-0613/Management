using AYam.Common.MVVM;
using Management.Data.Info;
using Model = Management.Pages.Model.JobList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Pages.ViewModel.JobList
{

    /// <summary>
    /// ジョブ.ViewModel
    /// </summary>
    public class JobDetail : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// ジョブ.Model
        /// </summary>
        private Model::JobDetail _Model;

        #endregion

        #region Property

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                if (_Model != null)
                {
                    return _Model.CreateDate;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            set
            {
                if (_Model != null)
                {
                    _Model.CreateDate = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        /// <summary>
        /// ジョブ.ViewModel
        /// </summary>
        public JobDetail()
        { }

        /// <summary>
        /// ジョブ.ViewModel
        /// </summary>
        /// <param name="job">ジョブファイル</param>
        public JobDetail(Job job)
        {
            _Model = new Model::JobDetail(job);
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
