using System;
using System.Collections.Generic;
using System.IO;
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

            string fileName = url.GetHashCode() + ".dataurl.cache";
            string urlMarker = fileName + ".marker.cache";

            if (File.Exists(urlMarker) && File.ReadAllText(urlMarker) == url)
            {
                Console.Write(File.ReadAllText(fileName));
            }
            else
            {
                string dataUrl;
                try
                {
                    byte[] image = new WebClient().DownloadData(url);
                    dataUrl = Convert.ToBase64String(image);
                }
                catch (Exception e)
                {
                    //Cache the error! Because if the favicon.ico is dead, I don't want to waste time verifying it...
                    dataUrl = e.ToString();
                }

                //TODO: Do some cache eviction... for now... well they are just favicons, so it shouldn't matter.
                File.WriteAllText(fileName, dataUrl);
                File.WriteAllText(urlMarker, url);

                Console.Write(dataUrl);
            }
        }
    }
}
