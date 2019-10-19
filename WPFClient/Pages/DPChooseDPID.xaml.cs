using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using DP;

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for DPChooseDPID.xaml
    /// </summary>
    public partial class DPChooseDPID : Page
    {
        private DPOptionsPage dpOptionPage;
        private Page pageToGoTo;
        public bool received { get; set; }
        public Object receivedObject { get; set; }

        public DPChooseDPID(DPOptionsPage dpOptionPage_, Page pageToGoTo_)
        {
            InitializeComponent();
            dpOptionPage = dpOptionPage_;
            pageToGoTo = pageToGoTo_;
            received = false;
            //Console.Out.WriteLine($"GOTO pagetype is {pageToGoTo_.GetType()}");
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("[HomePage] Button Next Clicked");

            if (dpIDTXTBox.Text.Equals(null) || dpIDTXTBox.Text.Equals("") || dpIDTXTBox.Text.Contains(" "))
            {
                System.Windows.MessageBox.Show("Please fill in an ID without spaces.");
            }
            else
            {
                string DPID = dpIDTXTBox.Text;

                MainWindow.getDP(DPID);

                new Thread(() =>
                {
                    while (receivedObject == null)
                    {
                        Thread.Sleep(10);
                    }

                    if (receivedObject != null)
                    {
                        Console.Out.WriteLine($"receivedObject.GetType() = {receivedObject.GetType()}");
                        if (receivedObject.GetType().ToString().Equals("DP.DatumPrakker"))
                        {
                            DatumPrakker datumPrakker = (DatumPrakker)receivedObject;
                            Console.Out.WriteLine($"DP received: {datumPrakker}");

                            Console.Out.WriteLine($"got page type = {pageToGoTo.GetType().ToString()}");

                            if (pageToGoTo.GetType().ToString().Equals("WPFClient.Pages.DPViewPage"))
                            {
                                Console.Out.WriteLine("goto view");
                                DPViewPage dpViewPage = (DPViewPage) pageToGoTo;
                                Dispatcher.Invoke(() => { dpViewPage.setDatumPrakker(datumPrakker); });
                                Dispatcher.Invoke(() => { NavigationService.Navigate(dpViewPage); });


                            } else if (pageToGoTo.GetType().ToString().Equals("WPFClient.Pages.DPFillinPage"))
                            {
                                Console.Out.WriteLine("goto fillin");
                                DPFillinPage dpFillinPage = (DPFillinPage)pageToGoTo;
                                Dispatcher.Invoke(() => { dpFillinPage.setDatumPrakker(datumPrakker); });
                                Dispatcher.Invoke(() => { NavigationService.Navigate(dpFillinPage); });
                                
                            }
                        }
                        else if (receivedObject.GetType().ToString().Equals("System.String"))
                        {
                            String s = (String) receivedObject;
                            if (s.Contains("NOTFOUND"))
                            {
                                Console.Out.WriteLine("DP NOTFOUND");
                                System.Windows.MessageBox.Show($"Datumprakker '{DPID}' not found");
                                Dispatcher.Invoke(() => { dpIDTXTBox.Text = ""; });

                            }
                        }

                    }
                    else
                    {
                        Console.Out.WriteLine("receivedObject is null");
                    }

                    //receivedObject = null;
                }).Start();
                //if ()
                //{

                //}
                //NavigationService.Navigate(pageToGoTo);
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Back");
            NavigationService.Navigate(dpOptionPage);
        }

    }
}
