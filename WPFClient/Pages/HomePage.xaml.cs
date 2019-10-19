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

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public DPOptionsPage dpOptionsPage { get; set; }
        //private String username;
        public HomePage()
        {
            InitializeComponent();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    // View Expense Report
        //    TestPage2 testPage2 = new TestPage2();
        //    this.NavigationService.Navigate(testPage2);
        //    Console.WriteLine("Butonn");
        //}

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("[HomePage] Button Next Clicked");
            
            if (usernameTXTBox.Text.Equals(null) || usernameTXTBox.Text.Equals("") || usernameTXTBox.Text.Contains(" "))
            {
                System.Windows.MessageBox.Show("Please fill in a username without spaces.");
            }
            else
            {
                //username = usernameTXTBox.Text;
                String username = usernameTXTBox.Text;
                MainWindow.setUsername(username);
                Console.Out.WriteLine($"username: {username}");
                dpOptionsPage = new DPOptionsPage();
                this.NavigationService.Navigate(dpOptionsPage);
            }

        }
    }
}
//wpf applicatie, change properties, output type to console. 
//string bar = (a == null ? null : a.PropertyOfA);
//if (bar != foo)
//{
//    ...
//}

//if (a?.PropertyOfA) // if foo is not null .propertyofA or a method 
//{
//    //somecode
//}


//IDevice selectedDevice = ((string)x).Contains("Tacx") ? new Devices.StationaryBike((string)x) : null;

//Task.Run(async () =>
//{
//await SetupDevice(deviceName);
//}).Wait();

