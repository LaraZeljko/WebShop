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
    public partial class Kolicina : Form
    {
        int br = 0;
        Class1 webshop2;
        int id;


        public Kolicina(Class1 webshop,int id2,string url)
        {
            InitializeComponent();
            webshop2 = webshop;
            id = id2;
            label1.Text = br.ToString();
            pictureBox1.Load(url);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            webshop2.kosarica.Add(id);
            br++;
            label1.Text = br.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            webshop2.kosarica.Remove(id);
            if (br>0)
            {
            br--;
            }
            label1.Text = br.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Kolicina_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
