using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace WebShopLibrary
{
    public class produkt
    {
        public int id { get; set; }//atributi
        public string title { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public rating rating { get; set; }

    }
    public class rating//rating sadrzi rate i count
    {
        public float rate { get; set; }
        public int count { get; set; }
    }
    public class racun
    {
        public int PurchaseId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class Class1//webshop
    {
        public List<int> kosarica = new List<int>();
        public List<produkt> lprodukta = new List<produkt>();
        public Class1()//konstruktor
        {
            lprodukta = DohvatiProdukte();
        }

        public List<racun> dohvatiRacuneIzBaze(DateTime pocetniDatum,DateTime zavrsniDatum)
        {
            List<racun> lracun = new List<racun>();
            string connectionString = "Data Source=193.198.57.183; Initial Catalog = " +
                "STUDENTI_PIN;User ID = pin; Password = Vsmti1234!";
            using (DbConnection oConnection = new SqlConnection(connectionString))
            using (DbCommand oCommand = oConnection.CreateCommand())
            {
                oCommand.CommandText = "SELECT * FROM Purchases_Zeljko";
                oConnection.Open();
                using (DbDataReader oReader = oCommand.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        DateTime datumKupovine = (DateTime)oReader["PurchaseDate"];

                        racun racunZaDodat = new racun {
                        PurchaseId=(int)oReader["PurchaseId"],
                        CustomerName=(string)oReader["CustomerName"],
                        Address=(string)oReader["Address"],
                        Email=(string)oReader["Email"],
                        PurchaseDate=(DateTime)oReader["PurchaseDate"],
                        TotalAmount=(decimal)oReader["TotalAmount"]
                        };

                        if (datumKupovine >= pocetniDatum && datumKupovine < zavrsniDatum)
                        {
                        lracun.Add(racunZaDodat);
                        }
                    }
                }

            }
            return lracun;
        }
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

        public List<produkt> DohvatiProdukte()
        {
            List<produkt> listaProdukta = new List<produkt>();
            string json = dohvatiPodatke();
            listaProdukta = JsonConvert.DeserializeObject<List<produkt>>(json);
            return listaProdukta;
        }
        public void UpdateProdukts()//spremanje produkta u bazu---
        {
            string connectionString = "Data Source=193.198.57.183; Initial Catalog = " +
                "STUDENTI_PIN;User ID = pin; Password = Vsmti1234!";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (produkt p in lprodukta)
                {
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = "INSERT INTO WebShop_Zeljko (id,title,price,description_,category,image_,rate, count_)" +
                        "VALUES (@id,@title,@price,@description,@category,@image,@rate,@count )";

                    SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 100);
                    id.Value = p.id;
                    command.Parameters.Add(id);
                    SqlParameter title = new SqlParameter("@title", SqlDbType.Text, 100);
                    title.Value = p.title;
                    command.Parameters.Add(title);
                    SqlParameter price = new SqlParameter("@price", SqlDbType.Float, 100);
                    price.Value = p.price;
                    command.Parameters.Add(price);
                    SqlParameter description = new SqlParameter("@description", SqlDbType.Text, 100);
                    description.Value = p.description;
                    command.Parameters.Add(description);
                    SqlParameter category = new SqlParameter("@category", SqlDbType.Text, 100);
                    category.Value = p.category;
                    command.Parameters.Add(category);
                    SqlParameter image = new SqlParameter("@image", SqlDbType.Text, 100);
                    image.Value = p.image;
                    command.Parameters.Add(image);
                    SqlParameter rate = new SqlParameter("@rate", SqlDbType.Float, 100);
                    rate.Value = p.rating.rate;
                    command.Parameters.Add(rate);
                    SqlParameter count = new SqlParameter("@count", SqlDbType.Int, 100);
                    count.Value = p.rating.count;
                    command.Parameters.Add(count);

                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
