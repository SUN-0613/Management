using Management.Method;
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

            if (DataContext is ViewModel::JobList viewModel)
            {

                viewModel.PropertyChanged += OnPropertyChanged;

                foreach (var detail in viewModel.Details)
                {
                    detail.PropertyChanged += OnPropertyChanged;
                }

            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel::JobList viewModel)
            {

                foreach (var detail in viewModel.Details)
                {
                    detail.PropertyChanged += OnPropertyChanged;
                }

                viewModel.PropertyChanged -= OnPropertyChanged;
                viewModel.Dispose();

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

                var detail = sender as ViewModel::JobDetail;

                switch (e.PropertyName)
                {

                    case "CallListAdd":

                        if (MessageBox.Show(Properties.JobList.MessageJobAdd, Properties.Title.JobList, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {
                            viewModel.AddNewJob();
                            viewModel.Details[viewModel.Details.Count - 1].PropertyChanged += OnPropertyChanged;
                            viewModel.SelectedDetail = viewModel.Details[viewModel.Details.Count - 1];
                            viewModel.AddPageAction(Properties.Title.JobDetail + ":" + Properties.Resources.NewData , new JobDetail(viewModel.SelectedDetail));
                        }

                        break;

                    case "CallListRemove":

                        if (viewModel.SelectedDetail != null
                            && MessageBox.Show(Properties.JobList.MessageJobRemove.Replace("*", viewModel.SelectedDetail.Name), Properties.Title.JobList, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {

                            var index = viewModel.Details.IndexOf(viewModel.SelectedDetail);
                            viewModel.Details[index].PropertyChanged -= OnPropertyChanged;

                            viewModel.RemoveSelectedJob();

                        }
                        break;

                    case "CallDetail":

                        if (detail != null)
                        {

                            viewModel.SelectedDetail = detail;
                            viewModel.AddPageAction(TabNameMethod.GetTabName(Properties.Title.JobDetail, viewModel.SelectedDetail.Name), new JobDetail(viewModel.SelectedDetail));

                        }
                        
                        break;

                    case "CallOpenQuotation":

                        if (detail != null)
                        {

                            viewModel.SelectedDetail = detail;
                            viewModel.AddPageAction(TabNameMethod.GetTabName(Properties.Title.Quotation, viewModel.SelectedDetail.Name), new Quotation.Quotation(detail.SelectedQuotation));

                        }
                        
                        break;

                    default:
                        break;
                }

            }

        }

    }
}
