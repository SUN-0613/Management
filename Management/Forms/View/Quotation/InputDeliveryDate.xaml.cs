using AYam.Common.MVVM;
using Management.Data.Info;
using ViewModel = Management.Forms.ViewModel.Quotation;
using System;
using System.ComponentModel;
using System.Windows;

namespace Management.Forms.View.Quotation
{
    /// <summary>
    /// InputDeliveryDate.xaml の相互作用ロジック
    /// </summary>
    public partial class InputDeliveryDate : Window, IDisposable
    {

        /// <summary>
        /// 納期入力.View
        /// </summary>
        /// <param name="deliveryDate">見積納期</param>
        public InputDeliveryDate(DeliveryDate deliveryDate)
        {

            InitializeComponent();

            if (DataContext is IDisposable dispose)
            {
                dispose.Dispose();
            }

            var viewModel = new ViewModel::InputDeliveryDate(deliveryDate);
            viewModel.PropertyChanged += OnPropertyChanged;

            DataContext = viewModel;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModelBase viewModel)
            {
                viewModel.PropertyChanged -= OnPropertyChanged;
            }

            if (DataContext is IDisposable dispose)
            {
                dispose.Dispose();
            }

        }

        /// <summary>
        /// ViewModelプロパティ変更イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (DataContext is ViewModel::InputDeliveryDate viewModel)
            {

                switch (e.PropertyName)
                {

                    case "CallOk":
                        DialogResult = true;
                        break;

                    default:
                        break;

                }

            }

        }

    }
}
