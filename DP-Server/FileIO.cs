using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using DP;

namespace DP_Server
{
    static class FileIO
    {
        private static string path = $@"{Directory.GetCurrentDirectory()}\DatumPrakkers.txt";
        public static async void WriteDPs()
        {
            Console.Out.WriteLine($@"{Directory.GetCurrentDirectory()}\DatumPrakkers.txt");

            // Create a file to write to.
            await using (FileStream fs = File.Create(path))
            {
                fs.Write(ObjectToByteArray(TCPServer.datumPrakkers));
            }
        }

        public static List<DatumPrakker> ReadDPs()
        {
            List<DatumPrakker> DPS = new List<DatumPrakker>();

            if (File.Exists(path))
            {
                Byte[] b = File.ReadAllBytes(path);
                DPS = (List<DatumPrakker>)ByteArrayToObject(b);
            }
            else
            {
                TCPServer.datumPrakkers = new List<DatumPrakker>();
                FileIO.WriteDPs();
            }
            
            return DPS;
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
    }
}
