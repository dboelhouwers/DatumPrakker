using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using DP_Server;

namespace DP
{
    class TCPServer
    {
        public static List<DatumPrakker> datumPrakkers { get; set; }
        static void Main(string[] args)
        {
            datumPrakkers = FileIO.ReadDPs();
            //Console.Out.WriteLine(datumPrakkers[1]);

            new TCPServer().TCPServerInit();

        }


        public async void TCPServerInit()
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
                TcpClient user = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted user at {DateTime.Now}");
                //Task<TcpClient> user = listener.AcceptTcpClientAsync();
                //TcpClient tcpClient = await user;

                //Thread thread = new Thread(HandleClientThread);
                //thread.Start(user);
                new Thread(async () => await HandleClientThread(user)).Start();
                
            }
        }

        static async Task HandleClientThread(object obj)
        {
            TcpClient client = obj as TcpClient;
            NetworkStream networkStream = client.GetStream();
            CTcpClient cTcpClient = new CTcpClient(client);

            bool done = false;
            try
            {
                while (!done) //&& client.Connected
                {

                    //Console.Out.WriteLine("while");
                    if (networkStream.DataAvailable)
                    {
                        Console.Out.WriteLine("data available");
                        Byte[] byteA = CSUtil.ReadObject(networkStream);
                        Console.Out.WriteLine($"Object type = '{byteA.GetType()}'");

                        if (byteA.GetType().ToString().Equals("System.Byte[]"))
                        {

                            Object objUnknown = CSUtil.ByteArrayToObject(byteA);
                            Console.Out.WriteLine($"Object3 type = '{objUnknown.GetType()}'");

                            //Checks which object it is 
                            if (objUnknown.GetType().ToString().Equals("DP.DatumPrakker"))
                            {
                                DatumPrakker datumPrakker = (DatumPrakker)CSUtil.ByteArrayToObject(byteA);

                                datumPrakkers.Add(datumPrakker);
                                FileIO.WriteDPs();
                                Console.Out.WriteLine($"DatumPrakker: {datumPrakker}");

                            }
                            else if (objUnknown.GetType().ToString().Equals("DP.DatumPrakker+DPAnswer"))
                            {
                                DatumPrakker.DPAnswer dpAnswer = (DatumPrakker.DPAnswer)CSUtil.ByteArrayToObject(byteA);

                                foreach (DatumPrakker p in datumPrakkers)
                                {
                                    if (p.id.Equals(dpAnswer.dpID))
                                    {
                                        p.answers.Add(dpAnswer);
                                        FileIO.WriteDPs();
                                    }
                                }

                                Console.Out.WriteLine($"DPAnswer: {dpAnswer}");

                            }
                            else if (objUnknown.GetType().ToString().Equals("System.String"))
                            {
                                String s = (String)CSUtil.ByteArrayToObject(byteA);
                                Console.Out.WriteLine($"String: {s}");

                                if (s.StartsWith("bye"))
                                {
                                    networkStream = null;
                                    client.Close();
                                    Console.WriteLine($"client.Connected: {client.Connected}");
                                    Console.WriteLine($"client '{cTcpClient.userName}' Disconnected");
                                    done = true;
                                    break;
                                }
                                else if (s.StartsWith("N^^M"))
                                {
                                    cTcpClient.userName = s.Remove(0, 4);
                                    Console.WriteLine($"Accepted {cTcpClient.userName}");
                                    //CSUtil.SendMessage(networkStream, "OK Name");
                                    networkStream.Flush();

                                }
                                else if (s.StartsWith("GETDP"))
                                {
                                    String dpID = s.Remove(0, 5);
                                    bool found = false;

                                    Console.Out.WriteLine($"getDP {dpID}");

                                    foreach (DatumPrakker dp in datumPrakkers)
                                    {
                                        if (dp.id.Equals(dpID))
                                        {
                                            found = true;
                                            CSUtil.SendObject(networkStream, dp);
                                            Console.Out.WriteLine($"DP '{dpID}' sended to {cTcpClient.userName}");
                                        }
                                    }

                                    if (!found)
                                    {
                                        CSUtil.SendObject(networkStream, "DPGET-NOTFOUND");
                                    }
                                }
                                else
                                {
                                    Console.Out.WriteLine(s);
                                }
                            }
                            else
                            {
                                Console.Out.WriteLine($"UnknownObject is Unknown: '{objUnknown.GetType()}'");
                            }

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
            Console.Out.WriteLine($"Closed connection with {userName}");
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
