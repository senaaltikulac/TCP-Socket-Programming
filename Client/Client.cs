using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        public static string veri = null;

        static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];
            IPEndPoint ipend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(ipend);
                Console.WriteLine("Socket Connection to {0}", sender.RemoteEndPoint.ToString());

                while(true)
                {
                    Console.Write("Client-me:");
                    string str = Console.ReadLine();
                    byte[] msg = Encoding.ASCII.GetBytes(str);
                    int byteSend = sender.Send(msg);
                    int byteRec = sender.Receive(bytes);
                    if (str.IndexOf("<eof>") > -1)
                    {
                        break;
                    }
                    veri = null;
                    veri += Encoding.ASCII.GetString(bytes, 0, byteRec);
                    Console.WriteLine("Server-you :{0}", veri);
                }
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();



               


            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
