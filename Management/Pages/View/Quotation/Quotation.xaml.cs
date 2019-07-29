using AYam.Common.MVVM;
using Management.Data.Info;
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

            switch (e.PropertyName)
            {

                default:
                    break;

            }

        }

    }
}
