using ViewModels = Management.Forms.ViewModel.Clients;
using System;
using System.ComponentModel;
using System.Windows;

namespace Management.Forms.View.Clients
{
    /// <summary>
    /// ClientAdd.xaml の相互作用ロジック
    /// </summary>
    public partial class ClientAdd : Window , IDisposable
    {

        /// <summary>
        /// 取引先追加.View
        /// </summary>
        public ClientAdd()
        {

            InitializeComponent();

            if (DataContext is ViewModels.ClientAdd viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModels.ClientAdd viewModel)
            {
                viewModel.PropertyChanged -= OnPropertyChanged;
            }

        }

        /// <summary>
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (DataContext is ViewModels.ClientAdd viewModel)
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
