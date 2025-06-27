using System.Diagnostics;
using System.Windows;

namespace Portfolio.WPF
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

		private void OpenExplorer_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("explorer.exe", "E:");
        }

		private void OpenEnvironmentVariables_Click(object sender, RoutedEventArgs e)
		{
			ProcessStartInfo processStartInfo = new("SystemPropertiesAdvanced")
			{
				UseShellExecute = true
			};
			Process.Start(processStartInfo);
		}

        private void RestartApplication_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Environment.ProcessPath!);
            Application.Current.Shutdown();
        }
    }
}