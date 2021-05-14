using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "me.utm.md";
            int port = 80;

            CreateSocket socket = new HTTP(host, port);
            Urls urls = new Urls(socket, GetMatchCollection(socket.SendRequest(null)));

            StartThreads(urls, 4);
            Console.ReadLine();
        }

        static MatchCollection GetMatchCollection(string page)
        {
            string imgFormat = @"<img.+?src=[""'](.+?(.jpg|.png|.gif|.jpeg))[""'].*?>";
            Regex regex = new Regex(imgFormat);

            return regex.Matches(page);
        }

        static void StartThreads(Urls urls, int threadsNr)
        {
            Thread[] threads = new Thread[threadsNr];
            Thread.CurrentThread.Name = "main";
            for (int i = 0; i < threadsNr; i++)
            {
                Thread t = new Thread(new ThreadStart(urls.DownloadImage));
                t.Name = "T" + (i + 1).ToString();
                threads[i] = t;
            }
            for (int i = 0; i < threadsNr; i++)
            {
                threads[i].Start();
                Console.WriteLine("Thread "+ threads[i].Name +" Alive: " + threads[i].IsAlive);
            }
        }
    }
}
