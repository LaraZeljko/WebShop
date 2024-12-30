using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WebShopLibrary
{
    public class produkt
    {
        public string id { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public rating rating { get; set; }
    }
    public class rating
    {
        public string rate { get; set; }
        public string count { get; set; }
    }
    public class Class1
    {
        public string dohvatiPodatke()
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
            return sResult;
        }

        public List<produkt>DohvatiProdukte()
        {
            List<produkt> listaProdukta = new List<produkt>();
            string json = dohvatiPodatke();
            listaProdukta = JsonConvert.DeserializeObject<List<produkt>>(json);
            return listaProdukta;
        }
    }
}
