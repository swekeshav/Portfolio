using System.Windows;
using System.Windows.Controls;

namespace Portfolio.WPF
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        public Calculator()
        {
            InitializeComponent();
        }

        void BtnNumber_Click(object sender, RoutedEventArgs args)
        {
            var newContent = (sender as Button)?.Content;
            var currentContent = lblResult.Content;

            lblResult.Content = currentContent.Equals("0")
                ? newContent
                : $"{currentContent}{newContent}";
        }

        void BtnPoint_Click(object sender, RoutedEventArgs args)
        {
            var currentContent = lblResult.Content.ToString()!;
            if (currentContent.Contains('.'))
                return;
            lblResult.Content = $"{currentContent}.";
        }
    }
}
