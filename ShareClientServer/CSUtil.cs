using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace DP
{
    public class CSUtil
    {
        public static void WriteTextMessage
           (TcpClient client, string message)
        {
            var stream
            = new StreamWriter(client.GetStream(), Encoding.ASCII);
            stream.WriteLine(message);
            stream.Flush();
        }

        public static string ReadTextMessage(TcpClient client)
        {
            StreamReader stream
            = new StreamReader(client.GetStream(), Encoding.ASCII);
            string line = stream.ReadLine();

            return line;
        }

        public static string ReadMessage(NetworkStream networkStream)
        {
            byte[] bufferRead; // = new byte[256];
            int totalRead = 0;
            byte[] messagebytesRead = new byte[4];

            while (totalRead < messagebytesRead.Length)
            {
                try
                {
                    totalRead += networkStream.Read(messagebytesRead, totalRead, messagebytesRead.Length - totalRead);
                    //Console.WriteLine($"T: {totalRead}");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.StackTrace);
                    return null;
                }
            }

            totalRead = 0;

            try
            {
                var size = BitConverter.ToInt32(messagebytesRead, 0);
                bufferRead = new byte[size];
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return null;
            }

            while (totalRead < bufferRead.Length)
            {
                try
                {
                    totalRead += networkStream.Read(bufferRead, totalRead, bufferRead.Length - totalRead);
                    //Console.WriteLine($"B: {totalRead}");
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }

            try
            {
                return Encoding.UTF8.GetString(bufferRead);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static void SendMessage(NetworkStream networkStream, string message)
        {

            if (message.Length != 0)
            {

                byte[] prefixSend = BitConverter.GetBytes(message.Length);
                byte[] bytesSend = Encoding.UTF8.GetBytes(message);
                byte[] bufferSend = new byte[prefixSend.Length + bytesSend.Length];

                prefixSend.CopyTo(bufferSend, 0);
                bytesSend.CopyTo(bufferSend, prefixSend.Length);

                networkStream.Write(bufferSend, 0, bufferSend.Length);
                //Console.WriteLine("Time: " + DateTime.Now);
                networkStream.Flush();
            }
            else
            {
                Console.WriteLine("Message is empty");
            }
        }
    }
}
