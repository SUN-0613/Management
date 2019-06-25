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
        /// <param name="e">引数</param>
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

                // データ保存パス用変数
                bool isSelectPath = false;
                int pathLength = Management.Properties.Settings.Default.RootPath.Length;

                // 引数取得
                foreach (var cmd in e.Args)
                {

                    switch (cmd)
                    {

                        case "-d":  // データ保存先の変更
                            isSelectPath = true;
                            break;

                        default:
                            break;
                    }

                }

                // フォルダ選択FLG更新
                // 引数指定 or パス未指定 or パスが存在しない
                isSelectPath = isSelectPath 
                                || pathLength.Equals(0)
                                || !System.IO.Directory.Exists(Management.Properties.Settings.Default.RootPath);

                // 初期設定
                if (isSelectPath)
                {

                    string defaultPath = pathLength.Equals(0) 
                                            || !System.IO.Directory.Exists(Management.Properties.Settings.Default.RootPath)
                                                ? System.Environment.CurrentDirectory 
                                                : Management.Properties.Settings.Default.RootPath;

                    // フォルダ選択画面表示


                    

                }

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
