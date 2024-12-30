using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using WebShopLibrary;

namespace WebShop
{
    class Program
    {
        static void Main(string[] args)
        {
            string sUrl = "https://fakestoreapi.com/products";
            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(sUrl);
            oRequest.Method = "GET";
            oRequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse oResponse = (HttpWebResponse)oRequest.GetResponse();
            Encoding oEncoding = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(oResponse.GetResponseStream(), oEncoding);
            string sResult = string.Empty;
            sResult = responseStream.ReadToEnd();
            oResponse.Close();
            Class1 newClass1 = new Class1();
            newClass1.DohvatiProdukte();
            foreach (produkt p in newClass1.DohvatiProdukte())
            {
                Console.WriteLine(p.title);
            }
            Console.ReadKey();
        }
    }
}
