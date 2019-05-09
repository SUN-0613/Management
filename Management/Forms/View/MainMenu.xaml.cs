using System;
using System.ComponentModel;
using System.Windows;

namespace Management.Forms.View
{
    
    /// <summary>
    /// メニュー.View
    /// </summary>
    public partial class MainMenu : Window, IDisposable
    {

        /// <summary>
        /// メインメニュー
        /// </summary>
        public MainMenu()
        {

            InitializeComponent();

            if (DataContext is ViewModel.MainMenu viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel.MainMenu viewModel)
            {
                viewModel.PropertyChanged -= OnPropertyChanged;
            }

        }

        /// <summary>
        /// ViewModelプロパティ変更イベント
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
