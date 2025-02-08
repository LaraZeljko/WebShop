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
    public partial class Kosarica : Form
    {
        public List<produkt> DodanoKosarica = new List<produkt>();
        public BindingList<produkt> bindingList = new BindingList<produkt>();
        Class1 webshop2;
        public Kosarica(List<int>listaId,List<produkt>lprodukta,Class1 webshop)
        {
            InitializeComponent();
            webshop2 = webshop;
            foreach (int id in listaId)
            {
                foreach (produkt p in lprodukta)
                {
                    if (id==p.id)
                    {
                        DodanoKosarica.Add(p);
                    }
                }
            }
            List<int> lid = new List<int>();
            for (int i = 0; i < DodanoKosarica.Count; i++)
            {
                int br = 1;
                decimal ukupnaCijena = DodanoKosarica[i].price;
                for (int j = i+1; j < DodanoKosarica.Count; j++)
                {
                    if (DodanoKosarica[i].id==DodanoKosarica[j].id&&!lid.Contains(DodanoKosarica[i].id))
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

        private void Form2_Load(object sender, EventArgs e)
        {

            DataGridViewButtonColumn makniButton = new DataGridViewButtonColumn();
            makniButton.UseColumnTextForButtonValue = true;
            
            dataGridView1.Columns.Add(makniButton);
            dataGridView1.AutoGenerateColumns = false;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                if (dataGridView1.CurrentCell.ColumnIndex == 4)
                {
                    for (int i = 0; i < DodanoKosarica.Count; i++)
                    {
                        if (id==DodanoKosarica[i].id)
                        {
                            webshop2.kosarica.RemoveAt(i);
                            DodanoKosarica.RemoveAt(i);
                            label1.Text = id.ToString();
                            bindingList.Clear();
                            foreach (produkt p in DodanoKosarica)
                            {
                                bindingList.Add(p);
                            }
                            dataGridView1.Rows.Clear();
                            List<int> lid = new List<int>();
                            for (int k  = 0; k < DodanoKosarica.Count; k++)
                            {
                                int br = 1;
                                decimal ukupnaCijena = DodanoKosarica[k].price;
                                for (int j = k + 1; j < DodanoKosarica.Count; j++)
                                {
                                    if (DodanoKosarica[k].id == DodanoKosarica[j].id && !lid.Contains(DodanoKosarica[k].id))
                                    {
                                        br++;
                                        ukupnaCijena += DodanoKosarica[k].price;
                                    }
                                }
                                if (!lid.Contains(DodanoKosarica[k].id))
                                {
                                    dataGridView1.Rows.Add(DodanoKosarica[k].id, DodanoKosarica[k].title, DodanoKosarica[k].category, br, ukupnaCijena);

                                }
                                lid.Add(DodanoKosarica[k].id);
                            }
                            break; 

                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                Racun kosarica = new Racun(DodanoKosarica);
                kosarica.Show();
            
        }
    }
}
