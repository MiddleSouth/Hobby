using System;
using System.Collections.Generic;
using System.IO;
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
    /// config.xaml の相互作用ロジック
    /// </summary>
    public partial class ConfigWindow : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigWindow()
        {
            InitializeComponent();

            Config config = new Config();

            ControlXml.ReadConfigXml(ref config);

            GetConfig(config);
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

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        /// <param name="config"></param>
        private void GetConfig(Config config)
        {
            try
            {
                this.setSpeedComboBox.SelectedIndex = config.Speed;
                this.AntMaxNumTextBox.Text = config.MaxAntNum.ToString();
                this.AntAddSpanTextBox.Text = config.AntAddSpan.ToString();
                this.AntLifeTextBox.Text = config.AntLife.ToString();
            }
            catch (Exception e)
            {
                this.setSpeedComboBox.SelectedIndex = 1;
                this.AntMaxNumTextBox.Text = "10";
                this.AntAddSpanTextBox.Text = "4000";
                this.AntLifeTextBox.Text = "50000";
            }
        }

        /// <summary>
        /// 設定を書き込む
        /// </summary>
        /// <param name="config"></param>
        private void SetConfig(ref Config config)
        {
            try
            {
                config.Speed = this.setSpeedComboBox.SelectedIndex;
                config.MaxAntNum = int.Parse(this.AntMaxNumTextBox.Text);
                config.AntAddSpan = int.Parse(this.AntAddSpanTextBox.Text);
                config.AntLife = int.Parse(this.AntLifeTextBox.Text);
            }
            catch
            {
                MessageBox.Show("", "", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// キャンセルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        /// <summary>
        /// プレビューボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            AntWorldWindow antWorldWindow = new AntWorldWindow(true);
            antWorldWindow.SetScreenSaverMode();
            antWorldWindow.SetConfigFromArg(this.setSpeedComboBox.SelectedIndex, int.Parse(this.AntMaxNumTextBox.Text),
                int.Parse(this.AntAddSpanTextBox.Text),int.Parse(this.AntLifeTextBox.Text));
            antWorldWindow.Show();
        }

        /// <summary>
        /// 保存ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Config config = new Config();
            SetConfig(ref config);
            ControlXml.WriteConfigXml(ref config);

            Application.Current.Shutdown(0);
        }
    }
}
