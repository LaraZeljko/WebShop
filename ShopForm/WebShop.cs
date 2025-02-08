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

namespace ShopForm
{
    public partial class WebShop : Form
    {
        Class1 webshop = new Class1();
        BindingSource tableBinding = new BindingSource();//da mogu prikazivat
        int br = 0;
        BindingSource tableBinding2 = new BindingSource();

        public WebShop()
        {
            InitializeComponent();
            tableBinding.DataSource = webshop.DohvatiProdukte();
            tableBinding2.DataSource = webshop.dohvatiRacuneIzBaze(DateTime.Now.AddYears(-10),DateTime.Now);
            LoadCategories();
        }
        private void LoadCategories()
        {
            var categories = new List<string> { "All", "electronics", "jewelery", "men's clothing", "women's clothing" };
            comboBoxCategories.DataSource = categories;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tableBinding;
            dataGridView2.DataSource = tableBinding2;
            DataGridViewButtonColumn dodajButton = new DataGridViewButtonColumn();
            DataGridViewButtonColumn oduzmiButton = new DataGridViewButtonColumn();
            
            dodajButton.UseColumnTextForButtonValue = true;
            dodajButton.Text = "dodaj";
            dataGridView1.Columns.Add(dodajButton);
            dataGridView1.AutoGenerateColumns = false;//pravilan redosljed
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
               
            if (e.RowIndex>=0)
            {
                int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                if (dataGridView1.CurrentCell.ColumnIndex==7)
                {
                    /*Form2 form2 = new Form2();
                    form2.Show();*/
                    string url = " ";
                    foreach (produkt item in webshop.lprodukta)
                    {
                        if (item.id==id)
                        {
                            url = item.image;
                        }
                    }
                    Kolicina kolicina = new Kolicina(webshop,id,url);
                    kolicina.Show();

                   
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kosarica kosarica = new Kosarica(webshop.kosarica,webshop.lprodukta,webshop);
            kosarica.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearch.Text.ToLower();
            string birajKategoriju = comboBoxCategories.SelectedItem.ToString();
            decimal minPrice = numericUpDownMinPrice.Value;
            decimal maxPrice = numericUpDownMaxPrice.Value;

            var filteredProducts = FilterProdukta(searchTerm, birajKategoriju, minPrice, maxPrice);
            dataGridView1.DataSource = filteredProducts;
        }
        private List<produkt> FilterProdukta(string searchTerm,string category, decimal minPrice, decimal maxPrice)
        {
            List<produkt> allProducts = webshop.DohvatiProdukte(); // tu kao vatam proizvode al nmp jel to dobro

            var filteredProducts = allProducts.Where(p =>
                (category == "All" || p.category == category) &&
                p.price >= minPrice &&
                p.price <= maxPrice && 
                p.title.ToLower().Contains(searchTerm)).ToList();

            return filteredProducts;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = webshop.dohvatiRacuneIzBaze(dateTimePicker1.Value, dateTimePicker2.Value);
        }
    }
}
