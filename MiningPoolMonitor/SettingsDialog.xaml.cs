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
using System.Windows.Shapes;

namespace MiningPoolMonitor
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        public SettingsDialog()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Pool == null)
            {
                Userpool.Text = "ex: https://zec.suprnova.cc";
            }
            else
            {
                Userpool.Text = Properties.Settings.Default.Pool;
                UserAPIKey.Text = Properties.Settings.Default.API_Key;
            }
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            bool result = Uri.TryCreate(Userpool.Text, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps); //https://stackoverflow.com/questions/7578857/how-to-check-whether-a-string-is-a-valid-http-url

            if (result == true)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("The URL should be of the form http://<pooldomain> or https://<pooldomain>");
            }

            
        }
    }
}
