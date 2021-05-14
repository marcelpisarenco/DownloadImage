using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Lab1
{
    public class HTTP : CreateSocket
    {
        public HTTP(string host, int port) : base(host, port) { }

        public override string SendRequest(string path)
        {
            Byte[] bytesSend = Encoding.ASCII.GetBytes(new Request(Host, path).Headers);
            string page = "";
            using (Socket s = ConnectSocket()) 
            {
                if (s == null)
                {
                    return ("Connection failed");
                }

                s.Send(bytesSend, bytesSend.Length, 0);

                page = ReadResponseMessage(s);

                s.Close();
            }
            return page;
        }
    }
}
