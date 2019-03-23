using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LangtonsAnt
{
    class Common
    {
    }

    static class ConstValue
    {
        #region 定数
        //色に関する定数
        public const uint BLACK = 0;
        //red + green + blue + alpha。 alpha値はいずれも0のため設定省略
        public const uint WHITE = (255 << 16) + (255 << 8) + 255;
        public const uint RED = (255 << 16) + (0 << 8) + 0;
        public const uint GREEN = (0 << 16) + (255 << 8) + 0;
        public const uint BLUE = (0 << 16) + (0 << 8) + 255;
        public const uint MAGENTA = (255 << 16) + (0 << 8) + 255;
        public const uint YELLOW = (255 << 16) + (255 << 8) + 0;
        public const uint CIAN = (0 << 16) + (255 << 8) + 255;

        //アリに関する定数
        public const int ANT_UP = 0;
        public const int ANT_RIGHT = 1;
        public const int ANT_DOWN = 2;
        public const int ANT_LEFT = 3;

        public enum AntSpeed { VerySlow, Slow, Normal, Fast, VeryFast, Lightning }

        public const string CONFIG_XML_NAME = "LangtonsAnt.xml";
        #endregion
    }

    static class PaintTool
    {
        /// <summary>
        /// RGBaの色を取得
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static uint MakeColor(uint red, uint green, uint blue, uint alpha)
        {
            uint color;

            color = (red << 16) + (green << 8) + blue + (alpha << 24);

            return color;
        }

        /// <summary>
        /// 点を描く
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public static void PaintDot(ref WriteableBitmap wb, int x, int y, uint color)
        {
            uint[] dot = new uint[1];
            int stride = (1 * wb.Format.BitsPerPixel + 7) / 8; //1行内のバイト数

            dot[0] = color;
            wb.WritePixels(new Int32Rect(x, y, 1, 1), dot, stride, 0);
        }

        /// <summary>
        /// 塗りつぶし
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        public static void PaintFull(ref WriteableBitmap wb, int width, int height, uint color)
        {
            int stride = (width * wb.Format.BitsPerPixel + 7) / 8; //1行内のバイト数
            uint[] fullColor = new uint[width * height];
            for (int i = 0; i < fullColor.Length; i++)
            {
                fullColor[i] = color;
            }

            wb.WritePixels(new Int32Rect(0, 0, width, height), fullColor, stride, 0);
        }

        /// <summary>
        /// スポイト機能
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static uint GetCurrentColor(WriteableBitmap wb, int x, int y)
        {
            uint[] pixels = new uint[1];
            int stride = (1 * wb.Format.BitsPerPixel + 7) / 8; //1行内のバイト数

            wb.CopyPixels(new Int32Rect(x, y, 1, 1), pixels, stride, 0);

            return pixels[0];
        }
    }

    static class ControlXml
    {
        /// <summary>
        /// xmlから設定を読み込む
        /// </summary>
        /// <param name="config"></param>
        public static void ReadConfigXml(ref Config config)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(dir, ConstValue.CONFIG_XML_NAME);

            try
            {
                if (File.Exists(filePath))
                {
                    //XmlSerializerオブジェクトを作成
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Config));

                    //読み込むファイルを開く
                    StreamReader sr = new System.IO.StreamReader(filePath, new System.Text.UTF8Encoding(false));

                    //XMLファイルから読み込み、逆シリアル化する
                    config = (Config)serializer.Deserialize(sr);

                    //ファイルを閉じる
                    sr.Close();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("設定の読み込みに失敗しました\n" + e.Message, "Error", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// 設定をxmlに書き込む
        /// </summary>
        /// <param name="config"></param>
        public static void WriteConfigXml(ref Config config)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(dir, ConstValue.CONFIG_XML_NAME);

            try {
                //XmlSerializerオブジェクトを作成
                //オブジェクトの型を指定する
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Config));

                //書き込むファイルを開く（UTF-8 BOM無し）
                StreamWriter sw = new System.IO.StreamWriter(filePath, false, new System.Text.UTF8Encoding(false));

                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(sw, config);

                //ファイルを閉じる
                sw.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("設定の書き込みに失敗しました\n" + e.Message, "Error", MessageBoxButton.OK);
            }
        }

    }
}
