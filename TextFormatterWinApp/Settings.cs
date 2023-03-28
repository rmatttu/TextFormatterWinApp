using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace TextFormatterWinApp
{
    [Serializable()]
    internal class Settings
    {
        public int MainFormX { get; }
        public int MainFormY { get; }
        public int MainFormWidth { get; }
        public int MainFormHeight { get; }
        public bool AutoFormat { get; }
        public bool Normalizer { get; }
        public bool RemoveLineBreakToEndOfLine { get; }
        public bool QuoteAfterLine2 { get; }

        public Settings(
            int mainFormX = 10,
            int mainFormY = 25,
            int mainFormWidth = 520,
            int mainFormHeight = 200,
            bool autoFormat = false,
            bool normalizer = false,
            bool removeLineBreakToEndOfLine = false,
            bool quoteAfterLine2 = false
        )
        {
            MainFormX = mainFormX;
            MainFormY = mainFormY;
            MainFormWidth = mainFormWidth;
            MainFormHeight = mainFormHeight;
            AutoFormat = autoFormat;
            Normalizer = normalizer;
            RemoveLineBreakToEndOfLine = removeLineBreakToEndOfLine;
            QuoteAfterLine2 = quoteAfterLine2;
        }

        public static Settings GenerateOrLoad()
        {
            string path = GetSavePath();
            if (File.Exists(path))
            {
                return LoadFromXmlFile();
            }
            else
            {
                return new Settings();
            }
        }

        /// <summary>
        /// 設定をXMLファイルから読み込み復元します
        /// </summary>
        public static Settings LoadFromXmlFile()
        {
            using (FileStream fs = new FileStream(GetSavePath(), FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                object obj = xs.Deserialize(fs);
                fs.Close();
                return (Settings)obj;
            }
        }

        /// <summary>
        /// 設定をXMLファイルに保存します
        /// </summary>
        public static void SaveToXmlFile(Settings settings)
        {
            using (FileStream fs = new FileStream(GetSavePath(), FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                xs.Serialize(fs, settings);
                fs.Close();
            }
        }

        public static string GetSavePath()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            var parent = Directory.GetParent(Assembly.GetExecutingAssembly().Location);
            return Path.Join(parent.FullName, info.ProductName + ".config");
        }
    }
}
