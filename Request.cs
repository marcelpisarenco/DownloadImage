using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lab1
{
    class Request
    {
        public string Headers { get; set; }

        public Request(string server, string path)
        {
            if (path == null)
            {
                path = "/";
            }

            Headers =
                "Get " + path + " HTTP/1.1\r\n" +
                "Host: " + server + "\r\n" +
                "Accept:  */*\r\n" +
                "Accept Encoding: gzip, deflate\r\n" +
                "Accept Language: ru-Ru,ru;q=0.9, en-Us;q=0.8,en;q=0.7\r\n" +
                "Connection: close\r\n\r\n";
        }
    }
}
