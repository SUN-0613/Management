using AYam.Common.MVVM;
using ViewModel = Management.Pages.ViewModel.JobList;
using Property = Management.Properties;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Management.Pages.View.JobList
{

    /// <summary>
    /// JobDetail.xaml の相互作用ロジック
    /// </summary>
    public partial class JobDetail : Page, IDisposable
    {

        /// <summary>
        /// ジョブ詳細.View
        /// </summary>
        /// <param name="job">ジョブファイル</param>
        public JobDetail(ViewModel::JobDetail viewModel)
        {

            InitializeComponent();

            if (DataContext is IDisposable dispose)
            {
                dispose.Dispose();
                dispose = null;
            }

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

            // JobListで行うため、ここではViewModel.Dispose()は行わない

            DataContext = null;

        }

        /// <summary>
        /// ViewModelプロパティ変更イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (DataContext is ViewModel::JobDetail viewModel)
            {

                switch (e.PropertyName)
                {

                    case "CallOpenQuotation":       // 見積書を開く・改訂
                        viewModel.AddPageAction(Property::Title.Quotation + ":" + viewModel.Name, new Quotation.Quotation(viewModel.SelectedQuotation));
                        break;

                    case "CallOpenDelivery":        // 納品書を開く
                        MessageBox.Show("考え中");
                        break;

                    case "CallRevisionDelivery":    // 納品書の改訂
                        MessageBox.Show("考え中");
                        break;

                    case "CallOpenInvoice":         // 請求書を開く
                        MessageBox.Show("考え中");
                        break;

                    case "CallRevisionInvoice":     // 請求書の改訂
                        MessageBox.Show("考え中");
                        break;

                    case "CallOpenCoverLetter":     // 封筒・送付状を開く
                        MessageBox.Show("考え中");
                        break;

                    case "CallOk":                  // OKボタンクリック
                        viewModel.ClosePageAction();
                        break;

                    default:
                        break;

                }

            }

        }

    }
}
