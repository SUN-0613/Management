using ViewModel = Management.Forms.ViewModel.Master;
using System;
using System.ComponentModel;
using System.Windows;

namespace Management.Forms.View.Master
{
    /// <summary>
    /// MasterInfo.xaml の相互作用ロジック
    /// </summary>
    public partial class MasterInfo : Window, IDisposable
    {

        /// <summary>
        /// マスタ情報.View
        /// </summary>
        public MasterInfo()
        {

            InitializeComponent();

            if (DataContext is ViewModel::MasterInfo viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel::MasterInfo viewModel)
            {
                viewModel.PropertyChanged -= OnPropertyChanged;
            }

        }

        /// <summary>
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (DataContext is ViewModel::MasterInfo viewModel)
            {

                switch (e.PropertyName)
                {

                    case "CallSave":

                        if (MessageBox.Show(Properties.MasterInfo.MessageSave, Properties.Title.MasterInfo, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {
                            viewModel.Save();
                            Close();
                        }

                        break;

                    case "CallClose":
                        Close();
                        break;

                    default:
                        break;

                }

            }

        }

    }
}
