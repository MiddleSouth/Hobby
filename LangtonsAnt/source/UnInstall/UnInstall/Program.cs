using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UnInstall
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

                result = MessageBox.Show("ラングトンのアリ　スクリーンセーバーを削除します。\nよろしいですか？", "アンインストール", MessageBoxButtons.OKCancel);

                switch (result)
                {
                    case DialogResult.OK:
                        InstallScreenSaver();
                        break;

                    case DialogResult.Cancel:
                        MessageBox.Show("アンインストールを中止しました。", "(｀・ω・´)", MessageBoxButtons.OK);
                        break;

                    default:
                        MessageBox.Show("アンインストールを中止しました。", "(｀・ω・´)", MessageBoxButtons.OK);
                        break;
                }

                Environment.Exit(0);
                //Application.Run(new Form1());
            }

        private static void InstallScreenSaver()
        {
            string exeDir = Environment.GetFolderPath(Environment.SpecialFolder.System);
            string exeFileName = "LangtonsAnt.scr";
            string exeFilePath = Path.Combine(exeDir, exeFileName);

            string xmlDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string xmlFileName = "LangtonsAnt.xml";
            string xmlFilePath = Path.Combine(xmlDir, xmlFileName);

            try
            {
                if(File.Exists(exeFilePath))
                {
                    File.Delete(exeFilePath);
                }
                else
                {
                    MessageBox.Show("ファイルが見つかりません。\nアンインストールに失敗したか、ラングトンのアリがインストールされていません。", "(´・ω・`)", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }

                if (File.Exists(xmlFilePath))
                {
                    File.Delete(xmlFilePath);
                }
                MessageBox.Show("アンインストールが完了しました。", "(´・ω・`)", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show("アンインストールに失敗しました。\n" + e.Message, "(´・ω・`)", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
