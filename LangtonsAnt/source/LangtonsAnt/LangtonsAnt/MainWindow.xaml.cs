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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LangtonsAnt
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        #region
        private const int WINDOW_BORDER_SIZE = 14;
        #endregion

        #region プライベート変数
        private AntSimulate _antSimulate = null;
        private int _antMoveCount = 0;
        private Random _random = new Random();
        private AntWorldWindow _antWorldWindow = null;
        #endregion

        #region プロパティ
        /// <summary>
        /// アリの行動回数
        /// </summary>
        public int AntMoveCount
        {
            set { this._antMoveCount = value; }
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this._antSimulate = new AntSimulate();
            this._antWorldWindow = new AntWorldWindow();

            speedComboBox.SelectedIndex = 2;
            scaleComboBox.SelectedIndex = 1;
        }

        /// <summary>
        /// シミュレーション開始ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this._antSimulate.StartAntWalk())
            {
                MessageBox.Show("アリが一匹もいません", "アリが居ない世界", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// 世界を作るボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void makeWorldButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (int.Parse(this.antWorldHeightText.Text) < 100)
                {
                    MessageBox.Show("アリの世界の縦サイズは100pixcel以上に設定してください。", "(´・ω・`)", MessageBoxButton.OK);
                    this.antWorldHeightText.Text = "100";
                    return;
                }
                if (int.Parse(this.antWorldWidthText.Text) < 320)
                {
                    MessageBox.Show("アリの世界の横サイズは160pixcel以上に設定してください。", "(´・ω・`)", MessageBoxButton.OK);
                    this.antWorldWidthText.Text = "160";
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("縦幅、横幅の入力値が不正です。", "(´・ω・`)", MessageBoxButton.OK);
                return;
            }

            this._antWorldWindow.antWorldImage.Width = int.Parse(antWorldWidthText.Text) * (scaleComboBox.SelectedIndex + 1);
            this._antWorldWindow.antWorldImage.Height = int.Parse(antWorldHeightText.Text) * (scaleComboBox.SelectedIndex + 1);
            this._antWorldWindow.Width = this._antWorldWindow.antWorldImage.Width + WINDOW_BORDER_SIZE;
            this._antWorldWindow.Height = this._antWorldWindow.antWorldImage.Height + WINDOW_BORDER_SIZE;

            this._antSimulate.MakeAntWorld(int.Parse(antWorldWidthText.Text), int.Parse(antWorldHeightText.Text));
            this._antWorldWindow.antWorldImage.Source = null;
            this._antWorldWindow.antWorldImage.Source = this._antSimulate.AntWorldWb;

            antCountValueLabel.Content = "0";

            this._antWorldWindow.Top = toolBoxWindow.Top;
            this._antWorldWindow.Left = toolBoxWindow.Left + toolBoxWindow.Width;
            this._antWorldWindow.Show();
        }

        /// <summary>
        /// アリを追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void makeAntButton_Click(object sender, RoutedEventArgs e)
        {
            int antCount = this._antSimulate.AntCount;
            uint antColor = ConstValue.WHITE;

            int x = this._random.Next(int.Parse(antWorldWidthText.Text));
            int y = this._random.Next(int.Parse(antWorldHeightText.Text));
            switch (antCount)
            {
                case 0:
                    antColor = ConstValue.WHITE;
                    break;
                case 1:
                    antColor = ConstValue.RED;
                    break;
                case 2:
                    antColor = ConstValue.GREEN;
                    break;
                case 3:
                    antColor = ConstValue.BLUE;
                    break;
                case 4:
                    antColor = ConstValue.MAGENTA;
                    break;
                case 5:
                    antColor = ConstValue.YELLOW;
                    break;
                case 6:
                    antColor = ConstValue.CIAN;
                    break;
                default:
                    antColor = PaintTool.MakeColor((uint)(this._random.Next(256)), (uint)(this._random.Next(256)), (uint)(this._random.Next(256)), 0);
                    break;
            }

            if (!this._antSimulate.AddAnt(x, y, this._random.Next(4), antColor))
            {
                MessageBox.Show("アリの世界がありません", "(´・ω・`)", MessageBoxButton.OK);
                return;
            }

            antCount++;
            antCountValueLabel.Content = antCount.ToString();
        }

        /// <summary>
        /// アリを削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteAntButton_Click(object sender, RoutedEventArgs e)
        {
            int antCount = this._antSimulate.AntCount;

            if (!this._antSimulate.DeleteAnt(this._antSimulate.AntCount - 1))
            {
                MessageBox.Show("アリが一匹もいません", "アリが居ない世界", MessageBoxButton.OK);
                return;
            }

            antCount--;
            antCountValueLabel.Content = antCount.ToString();
        }

        /// <summary>
        /// 停止ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            this._antSimulate.StopAntWalk();
        }

        /// <summary>
        /// 速度変更のコンボボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._antSimulate.SetAntWalkInterval(speedComboBox.SelectedIndex);
        }

        /// <summary>
        /// プログラム終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBoxWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _antWorldWindow.Close();
            Application.Current.Shutdown(0);
        }

        /// <summary>
        /// ツールボックスの移動にアリの世界を追従させる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBoxWindow_LocationChanged(object sender, EventArgs e)
        {
            if (this._antWorldWindow != null)
            {
                this._antWorldWindow.Top = toolBoxWindow.Top;
                this._antWorldWindow.Left = toolBoxWindow.Left + toolBoxWindow.Width;
            }
        }

        /// <summary>
        /// 画面サイズの拡大率のコンボボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scaleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._antWorldWindow.antWorldImage.Width = int.Parse(antWorldWidthText.Text) * (scaleComboBox.SelectedIndex + 1);
            this._antWorldWindow.antWorldImage.Height = int.Parse(antWorldHeightText.Text) * (scaleComboBox.SelectedIndex + 1);
            this._antWorldWindow.Width = this._antWorldWindow.antWorldImage.Width + WINDOW_BORDER_SIZE;
            this._antWorldWindow.Height = this._antWorldWindow.antWorldImage.Height + WINDOW_BORDER_SIZE;
        }

        /// <summary>
        /// テキストボックスの入力を数字のみにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputTonumeric(object sender, KeyEventArgs e)
        {
            // 数値以外、または数値の入力に関係しないキーが押された場合、イベントを処理済みに。
            if (!((Key.D0 <= e.Key && e.Key <= Key.D9) ||
                  (Key.NumPad0 <= e.Key && e.Key <= Key.NumPad9) ||
                  Key.Back == e.Key ||
                  Key.Delete == e.Key ||
                  Key.Tab == e.Key) ||
                (Keyboard.Modifiers & ModifierKeys.Shift) > 0)
            {
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }
    }
}
