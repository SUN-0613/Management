using AYam.Common.MVVM;
using System;

namespace Management.Forms.ViewModel
{

    /// <summary>
    /// 取引先追加.ViewModel
    /// </summary>
    public class ClientAdd : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// Model
        /// </summary>
        private Model.Clients.ClientAdd _Model;

        #endregion

        #region Property

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName
        {
            get { return _Model.CompanyName; }
            set
            {
                if (!_Model.CompanyName.Equals(value))
                {
                    _Model.CompanyName = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 保存コマンド
        /// </summary>
        public DelegateCommand SaveCommand
        {
            get
            {

                return new DelegateCommand(
                    () => { CallPropertyChanged("CallSave"); },
                    () => true);

            }
        }

        /// <summary>
        /// 取消コマンド
        /// </summary>
        public DelegateCommand CancelCommand
        {
            get
            {

                return new DelegateCommand(
                    () =>
                    {
                        CallPropertyChanged("CallClose");
                    },
                    () => true);

            }
        }

        #endregion

        /// <summary>
        /// 取引先追加.ViewModel
        /// </summary>
        public ClientAdd()
        {

            _Model = new Model.Clients.ClientAdd();

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
        /// 入力した会社名が取引先一覧に登録済みかチェック
        /// </summary>
        /// <returns>
        /// True:登録済み
        /// False:未登録
        /// </returns>
        public bool Contains()
        {
            return _Model.Contains();
        }

    }

}
