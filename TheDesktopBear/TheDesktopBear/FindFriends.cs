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
        public static List<string> friendList = new List<string>();

        public static void WaitPing()
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
            return localIP;
        }

        private static void CreateIcmpSocket()
        {
            icmpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            icmpSocket.Bind(new IPEndPoint(IPAddress.Parse(GetMyLocalIP()), 0));
            icmpSocket.IOControl(IOControlCode.ReceiveAll, new byte[] { 1, 0, 0, 0 }, new byte[] { 1, 0, 0, 0 });
            BeginReceiveFrom();
            
        }
        private static void CloseSocket()
        {
            icmpSocket.Close();
        }
        private static void BeginReceiveFrom()
        {
            icmpSocket.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ref remoteEndPoint, ReceiveCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            int len = icmpSocket.EndReceiveFrom(ar, ref remoteEndPoint);
            LogIcmp(receiveBuffer, len);
            BeginReceiveFrom();
        }

        private static void LogIcmp(byte[] buffer, int length)
        {
            IPHeader ipHeader = new IPHeader(buffer, length);
            string bearMsg = "";
            for (int i = 8; i < int.Parse(ipHeader.MessageLength); i++)
            {
                bearMsg += String.Format("{0}", Convert.ToChar(ipHeader.Data[i]));
            }

            //자신에게서 온 ping
            if(allignPingMsg("Bear-" + GetMyLocalIP()) == bearMsg)
            {
                Console.WriteLine("from me:" + bearMsg);
            }
            //다른 프로그램으로부터 온 ping
            else
            {
                if(bearMsg.Contains("Bear") == true)
                {
                    Console.WriteLine("from other bear:" + bearMsg);
                    friendList.Add(bearMsg);
                }
                else
                {
                    Console.WriteLine("from other program:" + bearMsg);
                }
            }
        }
        public static string allignPingMsg(string msg)
        {
            if (msg.Length > 32)
                return msg;

            int temp = msg.Length;

            for (int i = 0; i < (32 - temp); i++)
            {
                msg += "#";
            }
            return msg;
        }
    }
}
