using AYam.Common.MVVM;
using Management.Data.Info;
using FormsView = Management.Forms.View;
using FormsViewModel = Management.Forms.ViewModel;
using ViewModel = Management.Pages.ViewModel.Quotation;
using System;
using System.ComponentModel;
using System.Windows;
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
        /// 見積書.View
        /// </summary>
        /// <param name="dataFile">見積情報</param>
        /// <param name="nowPage">現在ページ数</param>
        /// <param name="maxPage">最大ページ数</param>
        public Quotation(DataFileInfo dataFile, int nowPage, int maxPage)
        {

            InitializeComponent();

            if (DataContext is IDisposable dispose)
            {
                dispose.Dispose();
            }

            var viewModel = new ViewModel::Quotation(dataFile, nowPage, maxPage);
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

                    case "CallExecutePrint":        // 印刷実行

                        if (MessageBox.Show(Properties.Resources.MessagePrint, Properties.Title.Quotation, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {

                            var errMessage = viewModel.ExecutePrint();

                            if (!string.IsNullOrEmpty(errMessage) || errMessage.Length.Equals(0))
                            {
                                MessageBox.Show(Properties.Resources.MessageFinishPrint, Properties.Title.Quotation, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                            }
                            else
                            {
                                MessageBox.Show(errMessage, Properties.Title.Quotation, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                            }

                        }

                        break;

                    case "CallSave":                // 保存

                        if (MessageBox.Show(Properties.Resources.MessageSave, Properties.Title.Quotation, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {
                            viewModel.Save();
                        }

                        break;

                    default:
                        break;

                }

            }

        }

    }
}
