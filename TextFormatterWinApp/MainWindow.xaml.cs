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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textLines = textBoxSource.Text.Split('\n');
            if (textLines.Length <= 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(textLines[0]);

            for (int i = 1; i < textLines.Length; i++)
            {
                sb.Append("> ");
                sb.Append(textLines[i]);
                sb.Append("\n");
            }

            textBoxDestination.Text = sb.ToString();
        }

        private void ButtonDestination_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBoxDestination.Text);
        }
    }
}
