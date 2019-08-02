using AYam.Common.MVVM;
using Management.Data.Info;
using FormsView = Management.Forms.View;
using FormsViewModel = Management.Forms.ViewModel;
using ViewModel = Management.Pages.ViewModel.Quotation;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace Management.Pages.View.Quotation
{
    /// <summary>
    /// Quotation.xaml の相互作用ロジック
    /// </summary>
    public partial class Quotation : Page, IDisposable
    {

        /// <summary>
        /// 見積書.View
        /// </summary>
        /// <param name="dataFile">見積情報</param>
        public Quotation(DataFileInfo dataFile)
        {

            InitializeComponent();

            if (DataContext is IDisposable dispose)
            {
                dispose.Dispose();
            }

            var viewModel = new ViewModel::Quotation(dataFile);
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

            DataContext = null;

        }

        /// <summary>
        /// ViewModel変更通知イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (DataContext is ViewModel::Quotation viewModel)
            {

                switch (e.PropertyName)
                {

                    case "CallSelectedStaff":       // 客先担当者を選択

                        var selectStaffs = new FormsView::Clients.SelectedStaff(viewModel.GetDataFileInfo());
                        var resultValue = selectStaffs.ShowDialog();

                        if (resultValue.HasValue && resultValue.Value
                            && selectStaffs.DataContext is FormsViewModel::Clients.SelectedStaff staffViewModel)
                        {

                            viewModel.SetClientStaffs(staffViewModel.Staffs);

                        }

                        selectStaffs.Dispose();
                        selectStaffs = null;

                        break;

                    case "CallInputDeliveryDate":   // 納期入力

                        var deliveryDate = new FormsView::Quotation.InputDeliveryDate(viewModel.GetDeliveryDate());
                        resultValue = deliveryDate.ShowDialog();

                        if (resultValue.HasValue && resultValue.Value
                            && deliveryDate.DataContext is FormsViewModel::Quotation.InputDeliveryDate deliveryViewModel)
                        {

                            viewModel.SetDeliveryDate(deliveryViewModel.No, deliveryViewModel.SelectedUnit);

                        }

                        deliveryDate.Dispose();
                        deliveryDate = null;

                        break;

                    default:
                        break;

                }

            }

        }

    }
}
