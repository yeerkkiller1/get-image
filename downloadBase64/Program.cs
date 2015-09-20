using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace downloadBase64
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = args.Length > 0 ? args[0] : "http://www.google.com/favicon.ico";

            byte[] image = new WebClient().DownloadData(url);

            string dataUrl = Convert.ToBase64String(image);

            Console.WriteLine(dataUrl);
        }
    }
}
