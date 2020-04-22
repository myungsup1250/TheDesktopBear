using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TheDesktopBear
{
    class FindFriends
    {
        private static Socket icmpSocket;
        private static byte[] receiveBuffer = new byte[256];
        private static EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void WaitPing()
        {
            CreateIcmpSocket();
            while (true) { Thread.Sleep(10); }
        }
        private static string GetMyLocalIP()
        {
            string localIP = "Not available, please check your network seetings!";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            //Console.WriteLine("myIP : " + localIP);
            return localIP;
        }

        private static void CreateIcmpSocket()
        {
            icmpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            icmpSocket.Bind(new IPEndPoint(IPAddress.Parse(GetMyLocalIP()), 0));
            icmpSocket.IOControl(IOControlCode.ReceiveAll, new byte[] { 1, 0, 0, 0 }, new byte[] { 1, 0, 0, 0 });
            //icmpSocket.Bind(new IPEndPoint(IPAddress.Any, 0));
            // Uncomment to receive all ICMP message (including destination unreachable).
            // Requires that the socket is bound to a particular interface. With mono,
            // fails on any OS but Windows.
            //if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            //{
            //    icmpSocket.IOControl(IOControlCode.ReceiveAll, new byte[] { 1, 0, 0, 0 }, new byte[] { 1, 0, 0, 0 });
            //}
            BeginReceiveFrom();
        }

        private static void BeginReceiveFrom()
        {
            icmpSocket.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None,
                ref remoteEndPoint, ReceiveCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            int len = icmpSocket.EndReceiveFrom(ar, ref remoteEndPoint);
            Console.WriteLine(string.Format("{0} Received {1} bytes from ({2})", DateTime.Now, len, remoteEndPoint));
            LogIcmp(receiveBuffer, len);
            BeginReceiveFrom();
        }

        private static void LogIcmp(byte[] buffer, int length)
        {
            IPHeader ipHeader = new IPHeader(buffer, length);

            //Console.WriteLine("Ver: " + ipHeader.Version);
            //Console.WriteLine("Header Length: " + ipHeader.HeaderLength);
            //Console.WriteLine("Total Length: " + ipHeader.TotalLength);
            //Console.WriteLine("MessageLength : " + ipHeader.MessageLength);
            //Console.WriteLine("Differntiated Services: " + ipHeader.DifferentiatedServices);
            //Console.WriteLine("Identification: " + ipHeader.Identification);
            //Console.WriteLine("Flags: " + ipHeader.Flags);
            //Console.WriteLine("Fragmentation Offset: " + ipHeader.FragmentationOffset);
            //Console.WriteLine("Time to live: " + ipHeader.TTL);
            //Console.WriteLine("Checksum: " + ipHeader.Checksum);
            //switch (ipHeader.ProtocolType)
            //{
            //    case Protocol.TCP:
            //        Console.WriteLine("Protocol: " + "TCP");
            //        break;
            //    case Protocol.UDP:
            //        Console.WriteLine("Protocol: " + "UDP");
            //        break;
            //    case Protocol.Unknown:
            //        Console.WriteLine("Protocol: " + "Unknown");
            //        break;
            //}

            Console.WriteLine("Source: " + ipHeader.SourceAddress.ToString());
            Console.WriteLine("Destination: " + ipHeader.DestinationAddress.ToString());
            Console.Write("Data: ");
            for (int i = 8; i < int.Parse(ipHeader.MessageLength); i++)
            {
                Console.Write(String.Format("{0}", Convert.ToChar(ipHeader.Data[i])));
            }
            Console.WriteLine("");

            // Checks RAW ICMP data.
            //for (int i = 0; i < length; i++)
            //{
            //    Console.Write(String.Format("{0} ", buffer[i].ToString()));
            //    if(i==11 || i==15 || i==19 || i==27)
            //        Console.WriteLine("");
            //    //Console.Write(String.Format("{0:X2} ", buffer[i]));
            //}
            //Console.WriteLine("");
        }
    }
}
