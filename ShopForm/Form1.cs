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
    public partial class Form1 : Form
    {
        Class1 webshop = new Class1();
        BindingSource tableBinding = new BindingSource();
        
        public Form1()
        {
            InitializeComponent();
            tableBinding.DataSource = webshop.DohvatiProdukte();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tableBinding;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
