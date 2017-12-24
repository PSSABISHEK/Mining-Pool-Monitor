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
using MetaphoricalSheep.Mpos.Api;
using MetaphoricalSheep.Mpos.Api.Responses;

namespace MiningPoolMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ContentSizeHandler();

            if (Properties.Settings.Default.Pool == null)
            {
                ConfigureMonitor();
            }
            else
            {
                //Do Nothing!!!
            }
            
            GetStats();
         


        }

        private void GetStats()
        {
            try
            {
                var pool = Properties.Settings.Default.Pool;
                var client = new Client(pool);

                PopulatePublicTab(client);
                PopulateDashboardTab(client);
                PopulateWorkerTab(client);


            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            

        }

        private void PopulateWorkerTab(Client client)
        {
            client.ApiKey = Properties.Settings.Default.API_Key;
            var WorkerResponse = client.GetUserWorkers();
            WorkersList.ItemsSource = WorkerResponse.Response.Data;
        }

        private void PopulateDashboardTab(Client client)
        {
            client.ApiKey = Properties.Settings.Default.API_Key;
            var DashboardResponse = client.GetDashboardData();

           
            TotalHashrate.Content = DashboardResponse.Response.Data.Personal.Hashrate;
            ShareRate.Content = DashboardResponse.Response.Data.Personal.ShareRate;

            var UserBalanceResponse = client.GetUserBalance();

            Unconfirmed.Content = UserBalanceResponse.Response.Data.Unconfirmed;
            Confirmed.Content = UserBalanceResponse.Response.Data.Confirmed;
            Orphaned.Content = UserBalanceResponse.Response.Data.Orphaned;            
        }

        private void PopulatePublicTab(Client client)
        {
            var response = client.Public();

            PoolName.Content = response.PoolName;
            HashRate.Content = response.Hashrate;
            Workers.Content = response.Workers;
            LastBlock.Content = response.LastBlock;
            NetworkHashrate.Content = response.NetworkHashrate;
        }

        private void ConfigureButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigureMonitor();
        }

        private void ConfigureMonitor()
        {
            var s = new SettingsDialog();
            bool? res = s.ShowDialog();

            if (res == true)
            {
                Properties.Settings.Default.Pool=s.Userpool.Text.ToString();
                Properties.Settings.Default.API_Key = s.UserAPIKey.Text.ToString();
                Properties.Settings.Default.Save();
            }

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            GetStats();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ContentSizeHandler();
        }

        private void ContentSizeHandler()
        {
            MainTab.Width = this.Width;
            MainTab.Height = this.Height - 70;
        }
    }
}
