using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DP
{
    class TCPServer
    {
        //Init datumprikker from fileIO first and than add with an add method 
        //public static List<DatumPrakker> datumPrakkers; 
        static void Main(string[] args)
        {
            new TCPServer();
            //List<DateTime> dates = new List<DateTime>()
            //{
            //    new DateTime(2019, 10, 26),
            //    new DateTime(2019, 10, 27)
            //};


            //DatumPrakker d = new DatumPrakker("Lorenzo's Datumprikker", "Lorenzo", dates);
            //d.answers.Add(new DatumPrakker.DPAnswer("Marleen", new List<DateTime>()
            //{
            //    new DateTime(2019, 10, 26),
            //    new DateTime(2019, 10, 27)
            //}));
            //d.answers.Add(new DatumPrakker.DPAnswer("Daphne", new List<DateTime>()
            //{
            //    new DateTime(2019, 10, 26)
            //}));

            //Console.WriteLine(d.ToString());

        }


        public TCPServer()
        {
            #region Server Init 
            IPAddress localhost; //= IPAddress.Parse("127.0.0.1");

            bool ipIsOk = IPAddress.TryParse("127.0.0.1", out localhost);
            if (!ipIsOk)
            {
                Console.WriteLine("ip adres kan niet geparsed worden.");
                Environment.Exit(1);
            }
            else
            {
                Console.WriteLine($"IPAddress okay: {localhost}");
            }

            TcpListener listener = new TcpListener(localhost, 7575);
            listener.Start();
            Console.WriteLine($@"
                ========================================
                  Server started at {DateTime.Now}
                  Waiting for connection
                ========================================"
            );
            #endregion

            while (true)
            {
                //AcceptTcpClient waits for a connection from the client
                TcpClient player = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted player at {DateTime.Now}");


                Thread thread = new Thread(HandleClientThread);
                thread.Start(player);
            }
        }

        static void HandleClientThread(object obj)
        {
            TcpClient client = obj as TcpClient;
            NetworkStream networkStream = client.GetStream();
            CTcpClient cTcpClient = new CTcpClient(client);

            bool done = false;
            try
            {
                while (!done) //&& client.Connected
                {

                    string received = CSUtil.ReadMessage(networkStream);

                    Console.WriteLine($"Received: {received} from {cTcpClient.userName}");

                    done = received.Equals("bye");
                    if (done)
                    {
                        Console.WriteLine("done=1");
                        //Console.WriteLine($"TEST: {streamReader.ReadToEnd()}");
                        CSUtil.SendMessage(networkStream, "BYE");
                        Console.WriteLine("sended BYE");
                    }
                    else
                    {
                        //Name verification 
                        if (received.StartsWith("N^^M"))
                        {
                            cTcpClient.userName = received.Remove(0, 4);
                            Console.WriteLine($"Accepted {cTcpClient.userName}");
                            CSUtil.SendMessage(networkStream, "OK Name");
                            networkStream.Flush();

                            //networkStream.Close();
                            //networkStream = null;
                        }
                        else
                        {
                            CSUtil.SendMessage(networkStream, "OK");
                            Console.WriteLine("sended OK");
                        }
                    }
                }
            }
            catch (IOException e)
            {
                if (!client.Connected)
                {
                    Console.WriteLine($"client.Connected: {client.Connected}");
                    Console.WriteLine($"client '{cTcpClient.userName}' Disconnected");
                    client.Close();
                }
                else
                {
                    Console.WriteLine(e);
                }
                //throw;
            }
        }
    }

    public class CTcpClient
    {
        public string userName { get; set; }
        public TcpClient tcpClient { get; set; }

        public CTcpClient(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        public void Close()
        {
            tcpClient.Close();
        }
    }

    public static class Generate
    {
        public static String generateID()
        {
            //Guid for customization. Now it is ' Guid.NewGuid().ToString().ToUpper(); '. 
            //Foreach lambda is better, but first needs to be cast to an object, like Char. 
            Guid guid = Guid.NewGuid();
            String idLower = guid.ToString();
            String idUpper = "";
            idLower.ToUpper();
            foreach (var i in idLower)
            {
                if (Char.IsDigit(i))
                {
                    idUpper += i.ToString();
                }
                else
                {
                    idUpper += i.ToString().ToUpper();
                }
            }

            return idUpper;
        }
    }

}
