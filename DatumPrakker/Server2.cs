//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using DP;


//namespace DP
//{
//    class Server2
//    {
//        private static IPAddress localhost;

//        static void Main(string[] args)
//        {

//            bool ipIsOk = IPAddress.TryParse("127.0.0.1", out localhost);

//            if (!ipIsOk)
//            {
//                Console.WriteLine("ip adres kan niet geparsed worden.");
//                Environment.Exit(1);
//            }
//            else
//            {
//                IPAddress localhost = IPAddress.Parse("127.0.0.1");
//                TcpListener listener = new System.Net.Sockets.TcpListener(localhost, 1330);

//                // Starts listening for incoming connection requests in the listener-thread:
//                listener.Start();

//                while (true)
//                {
//                    Console.WriteLine("Waiting for connection");

//                    //AcceptTcpClient waits for a connection from the client:
//                    TcpClient client = listener.AcceptTcpClient();

//                    // handle this client in a new worker-thread 
//                    // and continue accepting new clients:
//                    Thread thread = new Thread(HandleClientThread);
//                    thread.Start(client);
//                }
//            }
//        }

       
//       static void HandleClientThread(object obj)
//       {
//                TcpClient client = obj as TcpClient;

//                bool done = false;
//                while (!done)
//                {
//                    string received = ClientServerUtil.ReadTextMessage(client);
//                    Console.WriteLine("Received: {0}", received);

//                    done = received.Equals("bye");
//                    if (done) ClientServerUtil.WriteTextMessage(client, "BYE");
//                    else ClientServerUtil.WriteTextMessage(client, "OK");
//                }
//                client.Close();
//                Console.WriteLine("Connection closed");
//       }

       



//    }


//}
