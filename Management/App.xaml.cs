using System.Reflection;
using System.Threading;
using System.Windows;

namespace Management
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// 初期開始
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {

            // 実行パス取得
            string appName = Assembly.GetExecutingAssembly().GetName().Name;

            // 実行ファイル名取得
            appName = System.IO.Path.GetFileNameWithoutExtension(appName);

            // 多重起動情報取得
            Mutex mutex = new Mutex(false, appName);

            // 多重起動確認
            if (mutex.WaitOne(0, false))
            {

                // 基本処理
                base.OnStartup(e);

                // 画面表示
                var form = new Forms.View.Menu.MainMenu();
                form.ShowDialog();

            }
            else
            {

                // 多重起動時はメッセージ出力、終了
                MessageBox.Show(Management.Properties.Resources.App_MutexMessage, appName, MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();

            }

        }

    }
}
