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

            var client = new Client("https://vtc.suprnova.cc");
            var response = client.Public();
            
            getdata.Content = response.Workers;
        }

        private void getstats(object sender, RoutedEventArgs e)
        {

        }
    }
}
