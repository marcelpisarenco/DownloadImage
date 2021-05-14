using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace Lab1
{
    public class CreateSocket
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public CreateSocket(string host, int port)
        {
            Host = host;
            Port = port;
        }

        protected Socket ConnectSocket()
        {
            Socket s = null;
            IPHostEntry hostEntry = null;
            hostEntry = Dns.GetHostEntry(Host); 

            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipEndpoint = new IPEndPoint(address, Port);
                Socket tempSocket = new Socket(ipEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tempSocket.Connect(ipEndpoint);

                if (tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return s;
        }

        protected string ReadResponseMessage(Socket socket)
        {
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = socket.Receive(buffer, buffer.Length, 0);
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
            } while (bytes != 0);

            return messageData.ToString();
        }

        public virtual string SendRequest(string path) { return "SendReceive Not Defined"; }
    }
}
