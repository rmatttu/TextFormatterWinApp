using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TextFormatterWinApp
{
    [Serializable()]
    public record Settings
    {
        public double MainFormX { get; init; }
        public double MainFormY { get; init; }
        public double MainFormWidth { get; init; }
        public double MainFormHeight { get; init; }
        public bool AutoFormat { get; init; }
        public bool Normalizer { get; init; }
        public bool RemoveLineBreakToEndOfLine { get; init; }
        public bool QuoteAfterLine2 { get; init; }

        public Settings()
        {
            MainFormX = 10;
            MainFormY = 25;
            MainFormWidth = 520;
            MainFormHeight = 200;
            AutoFormat = false;
            Normalizer = false;
            RemoveLineBreakToEndOfLine = false;
            QuoteAfterLine2 = false;
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
