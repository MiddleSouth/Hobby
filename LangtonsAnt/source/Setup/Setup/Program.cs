using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Setup
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DialogResult result;

            result = MessageBox.Show("ラングトンのアリ　スクリーンセーバーをインストールします。\nよろしいですか？","インストール",MessageBoxButtons.OKCancel);

            switch(result)
            {
                case DialogResult.OK:
                    InstallScreenSaver();
                    break;

                case DialogResult.Cancel:
                    MessageBox.Show("インストールを中止しました。", "(´・ω・`)", MessageBoxButtons.OK);
                    break;

                default:
                    MessageBox.Show("インストールを中止しました。", "(´・ω・`)", MessageBoxButtons.OK);
                    break;
            }

            Environment.Exit(0);
            //Application.Run(new Form1());
        }

        private static void InstallScreenSaver()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.System);
            string fileName = "LangtonsAnt";
            string fromFilePath = Path.Combine(@"./", fileName + ".exe");
            string toFilePath = Path.Combine(dir, fileName + ".scr");

            try
            {
                File.Copy(fromFilePath, toFilePath, true);
                MessageBox.Show("インストールが完了しました。\nデスクトップ->右クリック->個人設定->スクリーンセーバより\n設定を行ってください。","(｀・ω・´)",MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show("インストールに失敗しました。\n" + e.Message, "(´・ω・`)", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
