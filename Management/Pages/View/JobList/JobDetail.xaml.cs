using AYam.Common.MVVM;
using Management.Data.Info;
using ViewModel = Management.Pages.ViewModel.JobList;
using System;
using System.ComponentModel;
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
        public JobDetail(Job job)
        {

            InitializeComponent();

            if (DataContext is IDisposable dispose)
            {
                dispose.Dispose();
                dispose = null;
            }

            var viewModel = new ViewModel::JobDetail(job);
            viewModel.PropertyChanged += OnPropertyChagned;

            DataContext = viewModel;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModelBase viewModel)
            {
                viewModel.PropertyChanged -= OnPropertyChagned;
            }

            if (DataContext is IDisposable dispose)
            {
                dispose.Dispose();
                dispose = null;
            }

        }

        /// <summary>
        /// ViewModelプロパティ変更イベント
        /// </summary>
        private void OnPropertyChagned(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallOpenQuotation":       // 見積書を開く
                    break;

                case "CallRevisionQuotation":   // 見積書の改訂
                    break;

                case "CallOpenDelivery":        // 納品書を開く
                    break;

                case "CallRevisionDelivery":    // 納品書の改訂
                    break;

                case "CallOpenInvoice":         // 請求書を開く
                    break;

                case "CallRevisionInvoice":     // 請求書の改訂
                    break;

                case "CallOpenCoverLetter":     // 封筒・送付状を開く
                    break;

                case "CallSave":                // データ保存
                    break;

                default:
                    break;

            }

        }

    }
}
