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
    /// Interaction logic for DPOptionsPage.xaml
    /// </summary>
    public partial class DPOptionsPage : Page
    {
        public DPOptionsPage()
        {
            InitializeComponent();
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            //TestPage2 testPage2 = new TestPage2();
            //this.NavigationService.Navigate(testPage2);
            Console.WriteLine("[DPOptionsPage] Button Create Clicked");

            DPCreatePage dpCreatePage = new DPCreatePage(this);
            NavigationService.Navigate(dpCreatePage);

        }

        private void Button_Click_View(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("[DPOptionsPage] Button View Clicked");
            DPViewPage dpViewPage = new DPViewPage(this);
            NavigationService.Navigate(dpViewPage);
        }

        private void Button_Click_FillIn(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("[DPOptionsPage] Button Fill in Clicked");
            DPFillinLoginPage dpfillinLoginPage = new DPFillinLoginPage(this);
            NavigationService.Navigate(dpfillinLoginPage);
        }
    }
}
