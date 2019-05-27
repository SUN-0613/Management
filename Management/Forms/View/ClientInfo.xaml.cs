using Management.Data.File;
using System;
using System.ComponentModel;
using System.Windows;

namespace Management.Forms.View
{
    /// <summary>
    /// ClientInfo.xaml の相互作用ロジック
    /// </summary>
    public partial class ClientInfo : Window, IDisposable
    {

        /// <summary>
        /// 取引先情報.View
        /// </summary>
        public ClientInfo()
        {

            InitializeComponent();

            if (DataContext is ViewModel.ClientInfo viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel.ClientInfo viewModel)
            {
                viewModel.PropertyChanged -= OnPropertyChanged;
            }

        }

        /// <summary>
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (DataContext is ViewModel.ClientInfo viewModel)
            {

                switch (e.PropertyName)
                {

                    case "CallListAdd":

                        var clientAdd = new ClientAdd() { Owner = this };
                        var resultValue = clientAdd.ShowDialog();

                        if (resultValue.HasValue && resultValue.Value
                            && clientAdd.DataContext is ViewModel.ClientAdd addViewModel)
                        {

                            viewModel.AddClient(addViewModel.CompanyName);

                        }

                        clientAdd.Dispose();
                        clientAdd = null;

                        break;

                    case "CallListRemove":

                        if (MessageBox.Show(Properties.ClientInfo.MessageListRemove.Replace("*", viewModel.CompanyName), Properties.Title.ClientInfo, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {
                            viewModel.RemoveSelectedClient();
                            Close();
                        }

                        break;

                    case "CallStaffAdd":

                        var staffAdd = new ClientStaffAdd(viewModel.SelectedClient.FileWildName) { Owner = this };
                        resultValue = staffAdd.ShowDialog();

                        if (resultValue.HasValue && resultValue.Value
                            && staffAdd.DataContext is ViewModel.ClientStaffAdd addVM)
                        {

                            viewModel.AddStaff(new Staff(addVM.Name, addVM.Phonetic, addVM.EMailAddress, addVM.MobilePhone, addVM.CreateDate));

                        }

                        staffAdd.Dispose();
                        staffAdd = null;

                        break;

                    case "CallStaffEdit":

                        staffAdd = new ClientStaffAdd(viewModel.SelectedClient.FileWildName, viewModel.SelectedStaff) { Owner = this };
                        resultValue = staffAdd.ShowDialog();

                        if (resultValue.HasValue && resultValue.Value
                            && staffAdd.DataContext is ViewModel.ClientStaffAdd editVM)
                        {
                            viewModel.EditStaff(new Staff(editVM.Name, editVM.Phonetic, editVM.EMailAddress, editVM.MobilePhone, editVM.CreateDate));
                        }

                        staffAdd.Dispose();
                        staffAdd = null;

                        break;

                    case "CallStaffRemove":

                        if (MessageBox.Show(Properties.ClientInfo.MessageStaffRemove.Replace("*", viewModel.SelectedStaff.Name), Properties.Title.ClientInfo, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
                        {
                            viewModel.RemoveSelectedStaff();
                            Close();
                        }

                        break;

                    case "CallSave":

                        if (MessageBox.Show(Properties.ClientInfo.MessageSave, Properties.Title.ClientInfo, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.Yes))
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
