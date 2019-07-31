using AYam.Common.MVVM;
using Management.Data.Info;
using Model = Management.Forms.Model.Clients;
using System;
using System.Collections.ObjectModel;

namespace Management.Forms.ViewModel.Clients
{

    /// <summary>
    /// 客先担当選択.ViewModel
    /// </summary>
    public class SelectedStaff : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// 客先担当選択.Model
        /// </summary>
        private Model::SelectedStaff _Model;

        #endregion

        #region Property

        /// <summary>
        /// 取引先名称
        /// </summary>
        public string ClientName
        {
            get { return _Model?.ClientName ?? ""; }
        }

        /// <summary>
        /// 客先担当一覧
        /// </summary>
        public ObservableCollection<Staff> Staffs
        {
            get { return _Model?.Staffs; }
            set
            {
                if (_Model != null)
                {
                    Staffs = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// OKコマンド
        /// </summary>
        public DelegateCommand OkCommand
        {
            get
            {
                return new DelegateCommand(() => 
                {
                    CallPropertyChanged("CallOk");
                });
            }
        }

        #endregion

        /// <summary>
        /// 客先担当選択.ViewModel
        /// </summary>
        public SelectedStaff()
        { }

        /// <summary>
        /// 客先担当選択.ViewModel
        /// </summary>
        /// <param name="dataFile">ファイル情報</param>
        public SelectedStaff(DataFileInfo dataFile)
        {

            _Model = new Model::SelectedStaff(dataFile);

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

        /// <summary>
        /// データ保存
        /// </summary>
        public void Save()
        {
            _Model?.Save();
        }

    }

}
