using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LangtonsAnt
{
    /// <summary>
    /// AntWorldWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class AntWorldWindow : Window
    {
        private AntSimulate _antSimulate;
        private Point? _lastMousePoint;

        private int _antLifeSpan = 80000;
        private int _antAddSpan = 3500;
        private int _antmaxCount = 10;
        private bool _isPreview = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="isShowScreen"></param>
        public AntWorldWindow(bool isPreview = false)
        {
            InitializeComponent();

            RenderOptions.SetBitmapScalingMode(this.antWorldImage, BitmapScalingMode.NearestNeighbor);

            //プレビューモード
            this._isPreview = isPreview;
        }

        /// <summary>
        /// スクリーンセーバモードに設定する
        /// </summary>
        public void SetScreenSaverMode()
        {
            _antSimulate = new AntSimulate(this._antLifeSpan, this._antAddSpan, this._antmaxCount);

            int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            int scale = 2;

            this.antWorldWindow.antWorldImage.Width = screenWidth;
            this.antWorldWindow.antWorldImage.Height = screenHeight;
            this.antWorldWindow.WindowState = WindowState.Maximized;
            this.antWorldWindow.Topmost = true;
            this.antWorldWindow.Cursor = Cursors.None;

            this._antSimulate.MakeAntWorld(screenWidth / scale, screenHeight / scale);
            this.antWorldWindow.antWorldImage.Source = this._antSimulate.AntWorldWb;
            this.antWorldWindow.ResizeMode = ResizeMode.NoResize;
            this.antWorldWindow.MouseMove += new MouseEventHandler(AntWorldWindow_MouseMove);
            this.antWorldWindow.KeyDown += AntWorldWindow_KeyDown;

            this._antSimulate.SetAntWalkInterval((int)ConstValue.AntSpeed.Normal);
        }

        /// <summary>
        /// xml設定ファイルから設定を読み込む
        /// </summary>
        public void SetConfigFromXml()
        {
            Config config = new Config();
            ControlXml.ReadConfigXml(ref config);
            this._antSimulate.SetConfigAntWorld(config.Speed, config.MaxAntNum, config.AntAddSpan, config.AntLife);
        }

        /// <summary>
        /// 引数から設定を読み込む
        /// </summary>
        /// <param name="speed">シミュレーションの速度</param>
        /// <param name="antMaxCount">アリの最大数</param>
        /// <param name="antAddSpan">アリを自動追加する間隔</param>
        /// <param name="antLife">アリの寿命</param>
        public void SetConfigFromArg(int speed, int antMaxCount, int antAddSpan, int antLife)
        {
            this._antSimulate.SetConfigAntWorld(speed, antMaxCount, antAddSpan, antLife);
        }

        /// <summary>
        /// キーを押されたらスクリーンセーバを終了する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AntWorldWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //プレビューモードの場合は画面を閉じるだけ
            if (this._isPreview)
            {
                this.antWorldWindow.Close();
                return;
            }

            //スクリーンセーバモードの場合はプログラム自体を終了する
            Application.Current.Shutdown(0);
        }

        /// <summary>
        /// マウスを動かしたらスクリーンセーバを終了する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AntWorldWindow_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentMousePosition = e.MouseDevice.GetPosition(null);
            Point firstPoint = new Point(0, 0);

            if (_lastMousePoint.HasValue)
            {
                if (Math.Abs(_lastMousePoint.Value.X - currentMousePosition.X) > 0 ||
                    Math.Abs(_lastMousePoint.Value.Y - currentMousePosition.Y) > 0)
                {
                    //プレビューモードの場合は画面を閉じるだけ
                    if (this._isPreview)
                    {
                        this.antWorldWindow.Close();
                        return;
                    }

                    //スクリーンセーバモードの場合はプログラム自体を終了する
                    Application.Current.Shutdown(0);
                    return;
                }
            }
            else
            {
                _lastMousePoint = currentMousePosition;
            }
        }

        /// <summary>
        /// スクリーンセーバモードの場合、アリの行動を開始する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void antWorldWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.antWorldWindow.WindowState == WindowState.Maximized)
            {
                this._antSimulate.StartAntWalk();
            }
        }
    }
}
