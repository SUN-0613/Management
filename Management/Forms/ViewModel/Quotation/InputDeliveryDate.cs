using AYam.Common.MVVM;
using Management.Data.Info;
using Model = Management.Forms.Model.Quotation;
using System;

namespace Management.Forms.ViewModel.Quotation
{

    /// <summary>
    /// 納期入力.ViewModel
    /// </summary>
    public class InputDeliveryDate : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// 納期入力.Model
        /// </summary>
        private Model::InputDeliveryDate _Model;

        #endregion

        #region Property

        /// <summary>
        /// 日数、週数、月数
        /// </summary>
        public double No
        {
            get { return _Model?.DeliveryDate.No ?? 0d; }
            set
            {
                if (_Model != null)
                {
                    _Model.DeliveryDate.No = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 納期単位
        /// </summary>
        public DeliveryUnit SelectedUnit
        {
            get { return _Model?.DeliveryDate.Unit ?? DeliveryUnit.Months; }
            set
            {
                if (_Model != null)
                {
                    _Model.DeliveryDate.Unit = value;
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
        /// 納期入力.ViewModel
        /// </summary>
        public InputDeliveryDate()
        { }

        /// <summary>
        /// 納期入力.ViewModel
        /// </summary>
        /// <param name="deliveryDate">見積納期</param>
        public InputDeliveryDate(DeliveryDate deliveryDate)
        {

            _Model = new Model::InputDeliveryDate(deliveryDate);

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
