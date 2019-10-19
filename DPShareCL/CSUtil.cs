using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace DP
{
    public class CSUtil
    {
        //ss
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

        public static void SendObject(NetworkStream networkStream, Object obj)
        {
            if (obj != null)
            {

                byte[] prefixSend = BitConverter.GetBytes(ObjectToByteArray(obj).Length);
                byte[] bytesSend = ObjectToByteArray(obj);
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

        public static Byte[] ReadObject(NetworkStream networkStream)
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
                return bufferRead;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        //public class Message
        //{
        //    public byte[] Data { get; set; }
        //}

        //public static class SD
        //{
        //    public static Message Serialize(object anySerializableObject)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
        //            return new Message { Data = memoryStream.ToArray() };
        //        }
        //    }

        //    public static object Deserialize(Message message)
        //    {
        //        using (var memoryStream = new MemoryStream(message.Data))
        //            return (new BinaryFormatter()).Deserialize(memoryStream);
        //    }
        //}
    }
}
