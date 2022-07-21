using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DummyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry iPHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = iPHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(endPoint);
                Console.WriteLine($"Connected To {socket.RemoteEndPoint.ToString()}");

                byte[] sendBuff = Encoding.UTF8.GetBytes("Hello World!");
                int sendBytes = socket.Send(sendBuff);

                byte[] recvBuff = new byte[1024];
                int recvBytes = socket.Receive(recvBuff);
                string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                Console.WriteLine($"[From Server] {recvData}");

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}