using System;
using System.Collections.Generic;
using System.Linq;
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

        private string MainFilter(string text)
        {
            var normalized = Formatter.RemoveLineBreakToEndOfLine(text);
            var normalizedLines = normalized.Split('\n').ToList();
            return Formatter.AddQuoteRange(normalized, new Range(1, normalizedLines.Count - 1));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textLines = textBoxSource.Text.Split('\n').ToList();
            if (textLines.Count <= 0)
            {
                return;
            }

            textBoxDestination.Text = Formatter.FilterForwardMessage(textBoxSource.Text);
            //textBoxDestination.Text = MainFilter(textBoxSource.Text);
        }

        private void ButtonDestination_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBoxDestination.Text);
        }
    }
}
