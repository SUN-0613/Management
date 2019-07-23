using AYam.Common.MVVM;
using ViewModel = Management.Pages.ViewModel.JobList;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Management.Pages.View.JobList
{
    /// <summary>
    /// JobList.xaml の相互作用ロジック
    /// </summary>
    public partial class JobList : Page, IDisposable
    {

        /// <summary>
        /// ジョブ一覧.View
        /// </summary>
        public JobList()
        {

            InitializeComponent();

            if (DataContext is ViewModelBase viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

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

            if (DataContext is ViewModel::JobList viewModel)
            {

                switch (e.PropertyName)
                {

                    case "CallListAdd":

                        if (MessageBox.Show(Properties.JobList.MessageJobAdd, Properties.Title.JobList, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {
                            viewModel.AddNewJob();
                            viewModel.SelectedDetail = viewModel.Details[viewModel.Details.Count - 1];
                            viewModel.AddPageAction(Properties.Title.JobDetail, viewModel.SelectedDetail);
                        }

                        break;

                    case "CallListRemove":

                        if (viewModel.SelectedDetail != null
                            && MessageBox.Show(Properties.JobList.MessageJobRemove.Replace("*", viewModel.SelectedDetail.Name), Properties.Title.JobList, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {
                            viewModel.RemoveSelectedJob();
                        }
                        break;

                    case "CallDetail":
                        viewModel.AddPageAction(Properties.Title.JobDetail, new JobDetail(viewModel.SelectedDetail));
                        break;

                    default:
                        break;
                }

            }

        }

    }
}
