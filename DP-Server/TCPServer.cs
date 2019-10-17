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
        public static List<DatumPrakker> datumPrakkers;
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
                TcpClient user = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted user at {DateTime.Now}");


                Thread thread = new Thread(HandleClientThread);
                thread.Start(user);
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
                            else if (objUnknown.GetType().ToString().Equals("DP.DatumPrakker+DPAnswer"))
                            {
                                DatumPrakker.DPAnswer dpAnswer = (DatumPrakker.DPAnswer)CSUtil.ByteArrayToObject(byteA);
                                Console.Out.WriteLine($"DPAnswer: {dpAnswer}");
                            }
                            else if (objUnknown.GetType().ToString().Equals("System.String"))
                            {
                                String s = (String)CSUtil.ByteArrayToObject(byteA);
                                Console.Out.WriteLine($"String: {s}");

                                if (s.StartsWith("N^^M"))
                                {
                                    cTcpClient.userName = s.Remove(0, 4);
                                    Console.WriteLine($"Accepted {cTcpClient.userName}");
                                    //CSUtil.SendMessage(networkStream, "OK Name");
                                    networkStream.Flush();

                                    //networkStream.Close();
                                    //networkStream = null;
                                }
                                else if (s.StartsWith("GETDP"))
                                {
                                    String dpID = s.Remove(0, 5);
                                    foreach (DatumPrakker dp in datumPrakkers)
                                    {
                                        if (dp.id.Equals(dpID))
                                        {
                                            CSUtil.SendObject(networkStream, dp);
                                        }
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

                        //Console.Out.WriteLine($"Test i: {test.i}");
                        ////CSUtil.Message obj = CSUtil.ReadObject(networkStream);
                        //DatumPrakker dp = CSUtil.readDP(client);
                        //Console.Out.WriteLine($"Name: {dp.name}");
                        ////Console.Out.WriteLine(received.GetType().ToString());
                        ////Console.WriteLine($"Received: {received} from {cTcpClient.userName}");
                    }

                    //done = false;
                    //if (done)
                    //{
                    //    Console.WriteLine("done=1");
                    //    //Console.WriteLine($"TEST: {streamReader.ReadToEnd()}");
                    //    CSUtil.SendMessage(networkStream, "BYE");
                    //    Console.WriteLine("sended BYE");
                    //}
                    //else
                    //{
                    //if (received.GetType().ToString() == "String")
                    //{
                    //    received = (String)received as String;
                    //    String s = (String) received;
                    //    if (s.StartsWith("N^^M"))
                    //    {
                    //        cTcpClient.userName = s.Remove(0, 4);
                    //        Console.WriteLine($"Accepted {cTcpClient.userName}");
                    //        CSUtil.SendMessage(networkStream, "OK Name");
                    //        networkStream.Flush();

                    //        //networkStream.Close();
                    //        //networkStream = null;
                    //    }
                    //    else
                    //    {
                    //        Console.Out.WriteLine(s);
                    //    }

                    //} else if (received.GetType().ToString() == "DatumPrakker")
                    //{
                    //    DatumPrakker dp = (DatumPrakker)received;
                    //    Console.Out.WriteLine(dp);

                    //}

                    //Name verification 

                    //else if (received.StartsWith("DP-O"))
                    //{
                    //    String s = received.Remove(0, 4);
                    //    Console.WriteLine($"Received Datumprakker: {s}");
                    //    CSUtil.SendMessage(networkStream, "OK DatumPrakker Object");
                    //    networkStream.Flush();

                    //}
                    //else if (received.StartsWith("DP-A"))
                    //{

                    //}
                    //else if (received.StartsWith("DP-G"))
                    //{

                    //}
                    //else
                    //{
                    //    CSUtil.SendMessage(networkStream, "OK");
                    //    Console.WriteLine("sended OK");
                    //}
                    //}
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
