using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using DP;


namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static String username { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            InitWindow();


            //this.MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            //InitTcp();
        }

        private void InitWindow()
        {
            //MainFrame.ShowsNavigationUI = false; // Used for NavigationWindow 
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            MainFrame.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseForward, OnBrowseForward));
            MainFrame.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseBack, OnBrowseBack));

            void OnBrowseForward(object sender, ExecutedRoutedEventArgs args)
            {
                Console.WriteLine($"Forward Mainframe");
                //Do Nothing 
            }
            void OnBrowseBack(object sender, ExecutedRoutedEventArgs args)
            {
                Console.WriteLine($"Back Mainframe");
                //Do Nothing 
            }
        }

        private void InitTcp()
        {
            TcpClient client = new TcpClient("127.0.0.1", 7575);
            NetworkStream networkStream = client.GetStream();

            bool done = false;

            //Console.WriteLine("Type your name: ");
            //string clientUserName = Console.ReadLine();

            //CSUtil.SendMessage(networkStream, ("N^^M" + clientUserName));
            string nameOk = CSUtil.ReadMessage(networkStream);

            Console.WriteLine("Name status: " + nameOk);
            //Console.WriteLine("Type 'bye' to end connection");

            while (!done)
            {

                //Console.Write("Enter a message to send to server: ");
                //string message = Console.ReadLine();

                if (networkStream.DataAvailable)
                {
                    string messageR = CSUtil.ReadMessage(networkStream);
                    Console.WriteLine("message: " + messageR);
                    done = messageR.Equals("BYE");
                }
                //else
                //{
                //    string messageAnswer = Console.ReadLine();
                //    CSUtil.SendMessage(networkStream, (messageAnswer));
                //}
            }
        }

        public static void addDP(string name, string createdByUsername, List<DateTime> chooseableDates)
        {
            DatumPrakker dp = new DatumPrakker(name, createdByUsername, chooseableDates);

        }

        public static void getDP(String id)
        {


        }

        public static void addDPAnswer(String dp_id, string username, List<DateTime> answers)
        {
            DatumPrakker.DPAnswer dpAnswer = new DatumPrakker.DPAnswer(dp_id, username, answers);

        }

    }
}
