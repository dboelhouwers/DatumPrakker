using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using DP;


namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static String username { get; set; }
        private static NetworkStream networkStream;
        public MainWindow()
        {
            InitializeComponent();
            InitWindow();


            //this.MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            new Thread(() =>
            {
                InitTcp();
            }).Start();
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
            networkStream = client.GetStream();

            bool done = false;


            //while (username == null)
            //{
            //    Thread.Sleep(50);
            //}

            //CSUtil.SendMessage(networkStream, ("N^^M" + username));

            //string nameOk = CSUtil.ReadMessage(networkStream);
            //Object s = CSUtil.ReadObject(networkStream);

            //Console.WriteLine("Name status: " + nameOk);
            //Console.WriteLine("Type 'bye' to end connection");

            while (!done)
            {

                if (networkStream.DataAvailable)
                {
                    Console.Out.WriteLine("data available");
                    Byte[] byteA = CSUtil.ReadObject(networkStream);
                    Console.Out.WriteLine($"Object type = '{byteA.GetType()}'");

                    if (byteA.GetType().ToString().Equals("System.Byte[]"))
                    {

                        Object objUnknown = CSUtil.ByteArrayToObject(byteA);
                        Console.Out.WriteLine($"Object3 type = '{objUnknown.GetType()}'");

                        if (objUnknown.GetType().ToString().Equals("DP.Test"))
                        {
                            Test test = (Test)CSUtil.ByteArrayToObject(byteA);
                            Console.Out.WriteLine($"test3: {test.i}");
                        }
                        else if (objUnknown.GetType().ToString().Equals("DP.DatumPrakker"))
                        {
                            DatumPrakker datumPrakker = (DatumPrakker)CSUtil.ByteArrayToObject(byteA);
                            Console.Out.WriteLine($"DatumPrakker: {datumPrakker}");
                        }
                        else
                        {
                            Console.Out.WriteLine($"UnknownObject is Unknown: '{objUnknown.GetType()}'");
                        }

                    }

                    //Console.Out.WriteLine($"Test i: {test.i}");
                    ////CSUtil.Message obj = CSUtil.ReadObject(networkStream);
                    //DatumPrakker dp = CSUtil.readDP(client);
                    //Console.Out.WriteLine($"Name: {dp.name}");
                    ////Console.Out.WriteLine(received.GetType().ToString());
                    ////Console.WriteLine($"Received: {received} from {cTcpClient.userName}");
                }

                //if (networkStream.DataAvailable)
                //{


                //    //string messageR = CSUtil.ReadMessage(networkStream);
                //    //Console.WriteLine("message: " + messageR);
                //    //done = messageR.Equals("BYE");
                //}
                //else
                //{
                //    string messageAnswer = Console.ReadLine();
                //    CSUtil.SendMessage(networkStream, (messageAnswer));
                //}
            }
        }

        public static void addDP(string id, string name, List<DateTime> chooseableDates)
        {
            DatumPrakker dp = new DatumPrakker(id, name, username, chooseableDates);
            //Test test = new Test(45);
            CSUtil.SendObject(networkStream, dp);
            //DatumPrakker.DPAnswer dpAnswer = new DatumPrakker.DPAnswer(dp.id, username, new List<DateTime>()
            //{
            //    new DateTime(2020, 10, 26)
            //});
            //CSUtil.SendObject(networkStream, dpAnswer);
        }

        public static void getDP(String id)
        {
            CSUtil.SendObject(networkStream, $"GETDP{id}");
        }

        public static void addDPAnswer(String dp_id, string username, List<DateTime> answers)
        {
            DatumPrakker.DPAnswer dpAnswer = new DatumPrakker.DPAnswer(dp_id, username, answers);
            CSUtil.SendObject(networkStream, dpAnswer);
        }

        public static void setUsername(string username_)
        {
            username = username_;
            CSUtil.SendObject(networkStream, "N^^MLorenzo");
        }



    }
}
