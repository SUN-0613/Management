using Management.Data.File;
using System;
using System.ComponentModel;
using System.Windows;

namespace Management.Forms.View
{
    /// <summary>
    /// ClientStaffAdd.xaml の相互作用ロジック
    /// </summary>
    public partial class ClientStaffAdd : Window, IDisposable
    {

        /// <summary>
        /// 担当者登録.ViewModel
        /// </summary>
        /// <param name="wildName">ファイル名のワイルド部分</param>
        /// <param name="staff">編集対象の担当者情報</param>
        public ClientStaffAdd(string wildName, Staff staff = null)
        {

            InitializeComponent();

            if (DataContext != null && DataContext is IDisposable dataContext)
            {
                dataContext.Dispose();
                dataContext = null;
            }

            if (staff == null)
            {
                DataContext = new ViewModel.ClientStaffAdd(wildName);
            }
            else
            {
                DataContext = new ViewModel.ClientStaffAdd(wildName, staff);
            }

            if (DataContext is ViewModel.ClientStaffAdd viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel.ClientStaffAdd viewModel)
            {
                viewModel.PropertyChanged -= OnPropertyChanged;
            }

        }

        /// <summary>
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (DataContext is ViewModel.ClientStaffAdd viewModel)
            {

                switch (e.PropertyName)
                {

                    case "CallSave":

                        if (!viewModel.Contains()
                            && MessageBox.Show(Properties.ClientInfo.MessageSave, Properties.Title.ClientInfo, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {
                            DialogResult = true;
                        }

                        break;

                    case "CallClose":
                        DialogResult = false;
                        break;

                    default:
                        break;

                }

            }

        }

    }
}
