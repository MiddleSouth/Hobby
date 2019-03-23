using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace LangtonsAnt
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private AntWorldWindow _antWorldWindow;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 0)
            {
                this._mainWindow = new MainWindow();
                this._mainWindow.Show();
                return;
            }

            switch (e.Args[0].Substring(0, 2).ToUpper())
            {
                // スクリーンセーバ実行
                case "-S":
                case "/S":
                    this._antWorldWindow = new AntWorldWindow();
                    this._antWorldWindow.SetScreenSaverMode();
                    this._antWorldWindow.SetConfigFromXml();
                    this._antWorldWindow.Show();
                    break;
                //プレビューモード
                case "-P":
                case "/P":
                    Application.Current.Shutdown();
                    break;
                case "-C":
                case "/C":
                    //設定モード
                    ConfigWindow configWindow = new ConfigWindow();
                    configWindow.Show();
                    break;
                default:
                    Application.Current.Shutdown();
                    break;
            }

            return;
        }
    }
}
