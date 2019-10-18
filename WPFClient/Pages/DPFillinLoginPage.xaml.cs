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
    /// Interaction logic for DPFillinLoginPage.xaml
    /// </summary>
    public partial class DPFillinLoginPage : Page
    {
        private DPOptionsPage dpOptionPage;
        public DPFillinLoginPage(DPOptionsPage dpOptionPage_)
        {
            InitializeComponent();
            dpOptionPage = dpOptionPage_;
        }


        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("[FillInLoginPage] Button Next Clicked");

            if (idTXTBox.Text.Equals(null) || idTXTBox.Text.Equals("") || idTXTBox.Text.Contains(" "))
            {
                MessageBox.Show("ID incorrect");
            }
            else
            {
                //username = usernameTXTBox.Text;
                String roomID = idTXTBox.Text;
                Console.Out.WriteLine($"roomID: {roomID}");
                DPFillinPage dpFillinPage = new DPFillinPage(dpOptionPage);
                this.NavigationService.Navigate(dpFillinPage);
            }

        }

    }
}
