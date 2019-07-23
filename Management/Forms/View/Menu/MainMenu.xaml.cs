using ViewModel = Management.Forms.ViewModel.Menu;
using System;
using System.ComponentModel;
using System.Windows;

namespace Management.Forms.View.Menu
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

            if (DataContext is ViewModel::MainMenu viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel::MainMenu viewModel)
            {

                viewModel.PropertyChanged -= OnPropertyChanged;

                viewModel.Dispose();
                viewModel = null;

            }

        }

        /// <summary>
        /// ViewModelプロパティ変更イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                #region ファイル

                case "CallPath":    // データの保存場所変更
                    MessageBox.Show("考え中");
                    break;

                case "CallQuit":    // アプリ終了
                    Close();
                    break;

                #endregion

                #region マスタ管理

                case "CallMaster":  // マスタ情報画面の呼び出し

                    var master = new Master.MasterInfo()
                    {
                        Owner = this
                    };

                    master.ShowDialog();

                    break;

                case "CallClient":  // 取引先情報画面の呼び出し

                    var client = new Clients.ClientInfo()
                    {
                        Owner = this
                    };

                    client.ShowDialog();

                    break;

                #endregion

                default:
                    break;
            }

        }

    }

}
