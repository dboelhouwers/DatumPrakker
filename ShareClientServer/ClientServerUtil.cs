using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ShareClientServer
{
    public class ClientServerUtil
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
    }
}
