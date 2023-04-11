using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextFormatterWinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            var settings = Settings.GenerateOrLoad();
            Left = settings.MainFormX;
            Top = settings.MainFormY;
            Width = settings.MainFormWidth;
            Height = settings.MainFormHeight;
            checkBoxAutoFormat.IsChecked = settings.AutoFormat;
            checkBoxQuoteAfterLine2.IsChecked = settings.QuoteAfterLine2;
            checkBoxRemoveLineBreakToEndOfLine.IsChecked = settings.RemoveLineBreakToEndOfLine;

            AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();
            mainWindow.Title = assembly.Name + " " + assembly.Version;
        }

        private Settings GetSettings()
        {
            return new Settings()
            {
                MainFormX = Left,
                MainFormY = Top,
                MainFormWidth = Width,
                MainFormHeight = Height,
                AutoFormat = checkBoxAutoFormat.IsChecked.Value,
                QuoteAfterLine2 = checkBoxQuoteAfterLine2.IsChecked.Value,
                RemoveLineBreakToEndOfLine = checkBoxRemoveLineBreakToEndOfLine.IsChecked.Value,
            };
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var currentSettings = GetSettings();
            Settings.SaveToXmlFile(currentSettings);
        }

        private string MainFilter(string text)
        {
            var normalized = Formatter.RemoveLineBreakToEndOfLine(text);
            var normalizedLines = normalized.Split('\n').ToList();
            return Formatter.AddQuoteRange(normalized, new Range(1, normalizedLines.Count - 1));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = textBoxSource.Text.Replace("\r", "");
            var textLines = text.Split('\n').ToList();
            if (textLines.Count <= 0)
            {
                return;
            }

            var method = Detector.Detect(text);
            switch (method)
            {
                case FormatMethod.Email:
                    textBoxDestination.Text = Formatter.FilterForwardMessage(text);
                    break;
                case FormatMethod.Normal:
                    textBoxDestination.Text = MainFilter(textBoxSource.Text);
                    break;
                default:
                    throw new Exception("No such FormatMethod");
            }
        }

        private void ButtonDestination_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBoxDestination.Text);
        }

    }
}
