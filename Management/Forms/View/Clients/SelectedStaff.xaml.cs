using AYam.Common.MVVM;
using Management.Data.Info;
using ViewModel = Management.Forms.ViewModel.Clients;
using System;
using System.ComponentModel;
using System.Windows;

namespace Management.Forms.View.Clients
{
    /// <summary>
    /// SelectedStaff.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectedStaff : Window, IDisposable
    {

        /// <summary>
        /// 客先担当選択.View
        /// </summary>
        /// <param name="dataFile">ファイル情報</param>
        public SelectedStaff(DataFileInfo dataFile)
        {

            InitializeComponent();

            if (DataContext is IDisposable dispose)
            {
                dispose.Dispose();
            }

            var viewModel = new ViewModel::SelectedStaff(dataFile);
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
        /// ViewModelプロパティ変更イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (DataContext is ViewModel::SelectedStaff viewModel)
            {

                switch (e.PropertyName)
                {


                    case "CallOk":
                        viewModel.Save();
                        DialogResult = true;
                        break;

                    default:
                        break;

                }

            }

        }

    }
}
