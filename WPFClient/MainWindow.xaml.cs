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
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //InitTcp();
        }

        private void InitTcp()
        {
            TcpClient client = new TcpClient("127.0.0.1", 7575);
            NetworkStream networkStream = client.GetStream();

            bool done = false;

            Console.WriteLine("Type your name: ");
            string clientUserName = Console.ReadLine();

            CSUtil.SendMessage(networkStream, ("N^^M" + clientUserName));
            string nameOk = CSUtil.ReadMessage(networkStream);

            Console.WriteLine("Name status: " + nameOk);
            //Console.WriteLine("Type 'bye' to end connection");

            while (!done)
            {

                Console.Write("Enter a message to send to server: ");
                string message = Console.ReadLine();

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
    }
}
