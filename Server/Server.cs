using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public static string veri = null;
        static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];
            IPEndPoint ipend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(ipend);
                listener.Listen(10);
                while(true)
                {
                    Console.WriteLine("Waiting for connection !!");
                    Socket handler = listener.Accept();
                    veri = null;
                    while(true)
                    {
                        veri = null;
                        bytes = new byte[1024];
                        int byteRec = handler.Receive(bytes);
                        veri += Encoding.ASCII.GetString(bytes, 0, byteRec);
                        Console.WriteLine("Client- You :{0}", veri);
                        if(veri.IndexOf("<eof>")> -1)
                        {
                            break;
                        }
                        veri = null;
                        Console.Write("Server- Me :");
                        veri = Console.ReadLine();
                        byte[] msg = Encoding.ASCII.GetBytes(veri);
                        handler.Send(msg);
                    }
                   
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();



                }
            }
            catch
            {

            }
        }
    }
}
