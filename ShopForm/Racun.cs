using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebShopLibrary;
using System.Data.SqlClient;


namespace ShopForm
{
    public partial class Racun : Form
    {
        public List<produkt> racun = new List<produkt>();
        public BindingList<produkt> bindingList = new BindingList<produkt>();
        public decimal ukupnaUkupnaCijena = 0;
        
        public Racun(List<produkt> DodanoKosarica)
        {
            InitializeComponent();
            foreach (produkt p in DodanoKosarica)
            {
                racun.Add(p);
                bindingList.Add(p);
                ukupnaUkupnaCijena += p.price;
            }

            label4.Text = ukupnaUkupnaCijena.ToString();
            List<int> lid = new List<int>();
            for (int i = 0; i < DodanoKosarica.Count; i++)
            {
                int br = 1;
                decimal ukupnaCijena = DodanoKosarica[i].price;
                
                for (int j = i + 1; j < DodanoKosarica.Count; j++)
                {
                    if (DodanoKosarica[i].id == DodanoKosarica[j].id && !lid.Contains(DodanoKosarica[i].id))
                    {
                        br++;
                        ukupnaCijena += DodanoKosarica[i].price;
                        
                    }
                }

                if (!lid.Contains(DodanoKosarica[i].id))
                {
                    dataGridView1.Rows.Add(DodanoKosarica[i].id, DodanoKosarica[i].title, DodanoKosarica[i].category, br, ukupnaCijena);

                }
                lid.Add(DodanoKosarica[i].id);

            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            string customerName = textBoxCustomerName.Text;
            string address = textBoxAddress.Text;
            string email = textBoxEmail.Text;
            DateTime purchaseDate = dateTimePicker1.Value.Date;// DateTime.Now;
            decimal totalAmount = Convert.ToDecimal(label4.Text);

            if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Molimo unesite sve potrebne podatke o kupcu.");
                return;
            }

            if (racun.Count == 0)
            {
                MessageBox.Show("Košarica je prazna.");
                return;
            }

            SavePurchaseToDatabase(racun, customerName, address, email, purchaseDate,totalAmount);
            //MessageBox.Show("Kupovina je uspješno završena!");
            gatto g1 = new gatto();
            g1.ShowDialog();
            ClearCart(); // kad zavrsim kupovinu cistim cart s ovim
        }
        public void SavePurchaseToDatabase(List<produkt> racun, string customerName, string address, string email, DateTime purchaseDate,decimal totalAmount)
        {
            string connectionString = "Data Source=193.198.57.183; Initial Catalog=STUDENTI_PIN; User ID=pin; Password=Vsmti1234!";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // spremam u tablicu purchace podatke o kupcu
                SqlCommand command = new SqlCommand("INSERT INTO Purchases_Zeljko (CustomerName, Address, Email, PurchaseDate,TotalAmount) VALUES (@CustomerName, @Address, @Email, @PurchaseDate,@TotalAmount); SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.Add(new SqlParameter("@CustomerName", SqlDbType.NVarChar) { Value = customerName });
                command.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar) { Value = address });
                command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email });
                command.Parameters.Add(new SqlParameter("@PurchaseDate", SqlDbType.DateTime) { Value = purchaseDate });
                command.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal) { Value = totalAmount });

                int purchaseId = Convert.ToInt32(command.ExecuteScalar());


                List<produkt> uniqueProducts = racun.Distinct().ToList();
                foreach (produkt item in uniqueProducts)
                {
                    SqlCommand itemCommand = new SqlCommand("INSERT INTO PurchaseItems_Zeljko (PurchaseId, ProductId, Quantity,Category) VALUES (@PurchaseId, @ProductId, @Quantity,@Category)", connection);
                    itemCommand.Parameters.Add(new SqlParameter("@PurchaseId", SqlDbType.Int) { Value = purchaseId });
                    itemCommand.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = item.id }); // Pretpostavljam da 'id' predstavlja ID proizvoda
                    itemCommand.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = racun.Count(x=>x.id==item.id) }); // Ako želite pratiti količinu, promijenite ovo prema potrebi
                    itemCommand.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar) { Value = item.category });

                    itemCommand.ExecuteNonQuery();
                }
                // spremam u puschaseItems
                foreach (produkt item in racun)
                {
                   
                }
            }
        }
        private void ClearCart()
        {
            racun.Clear();
            dataGridView1.DataSource = null; 
            dataGridView1.Rows.Clear(); 
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
