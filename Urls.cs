using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lab1
{
    public class Urls
    {
        private Object MatchLock = new object();
        private int Index { get; set; }
        private string[] Matches { get; set; }
        public CreateSocket CurrentSocket { get; set; }
        public Urls(CreateSocket socket, MatchCollection mathes)
        {
            CurrentSocket = socket;
            Matches = mathes.Cast<Match>().Select(MatchSelect).ToArray();
            Index = -1;
        }

        public string GetFileUrl()
        {
            if (Index + 1 >= Matches.Length)
            {
                return null;
            }

            lock (MatchLock)
            {
                if (Index + 1 < Matches.Length)
                {
                    Index += 1;
                }
                return Matches.ElementAt(Index);
            }
        }

        public void DownloadImage()
        {
            string fileUrl = null;
            do
            {
                fileUrl = GetFileUrl();
                if (fileUrl != null)
                {
                    Console.WriteLine("Thread:"+ Thread.CurrentThread.Name + ", file:" + FileName(fileUrl));
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri(fileUrl), @"D:\University\Semestru 6\PR\DownloadedImage\" + FileName(fileUrl));
                    }
                }
            } while (fileUrl != null);
        }

        private string MatchSelect(Match m)
        {
            string baseUrl = "";
            string value = m.Groups[1].Value;
            if (value.Contains("http://"))
            {
                return value;
            }
            baseUrl = "http://";
            baseUrl += CurrentSocket.Host;

            if (value.IndexOf("/") == 0)
            {
                return baseUrl + value;
            }

            return baseUrl + '/' + value;
        }

        private string FileName(string url)
        {
            string substr = url.Substring(url.LastIndexOf('/') + 1);
            return Regex.Replace(substr, @"(\?|\|/)", "_");
        }

    }
}
