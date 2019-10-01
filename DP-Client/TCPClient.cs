using System;
using System.Net.Sockets;
using WPFClient;

namespace DP
{
    class TCPClient
    {
        static void Main(string[] args)
        {
            //new TCPClient();
            //new WPFClient
            //MainWindow main = new WpfApplication1.MainWindow();
            //main.ShowDialog();
        }

        public TCPClient()
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
