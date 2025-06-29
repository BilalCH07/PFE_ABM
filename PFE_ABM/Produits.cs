using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PFE_ABM.DataSetAchatTableAdapters;

namespace PFE_ABM
{
    public partial class Produits : Form
    {
        DataSetAchat ds = new DataSetAchat();
        ProductsTableAdapter pr = new ProductsTableAdapter();
        CategorieTableAdapter cat = new CategorieTableAdapter();
        public Produits()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }
        public void rempligrid()
        {
            var qr = from pr in ds.Products
                     join c in ds.Categorie on pr.idCat equals c.idCat
                     select new
                     {
                         pr.idProd,
                         pr.nomProd,
                         pr.QtProd,
                         pr.PrixProd,
                         c.nomCat
                     };
            guna2DataGridView1.DataSource = qr.ToList();
        }

        private void Produits_Load(object sender, EventArgs e)
        {
            pr.Fill(ds.Products);
            cat.Fill(ds.Categorie);

            ComboBox_cat.DataSource = ds.Categorie;
            ComboBox_cat.DisplayMember = "nomCat";
            ComboBox_cat.ValueMember = "idCat";

            rempligrid();
        }

        private void Btn_ajt_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = ds.Products.NewRow();
                row[0] = TextBox_id.Text;
                row[1] = TextBox_nom.Text;
                row[2] = TextBox_qty.Text;
                row[3] = TextBox_prix.Text;
                row[4] = ComboBox_cat.SelectedValue.ToString();
                ds.Products.Rows.Add(row);
                pr.Update(ds.Products);
                ds.AcceptChanges();
                rempligrid();
                string msg = "La Produit ajouté avec succès!";
                Message m = new Message(msg);
                m.btn_cancel.Visible = false;
                m.ShowDialog();
            }
            catch
            {
                string msg = "Saisir infos";
                Message m = new Message(msg);
                m.ShowDialog();
            }
        }
        public void vider()
        {
            TextBox_id.Text = TextBox_nom.Text = TextBox_prix.Text = TextBox_qty.Text = null;
            TextBox_id.Enabled = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            vider();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2DataGridView1.CurrentRow.Selected = true;
            String num = guna2DataGridView1.Rows[e.RowIndex].Cells["idProd"].Value.ToString();
            for(int i = 0; i < ds.Products.Count; i++)
            {
                if (num == ds.Products.Rows[i][0].ToString())
                {
                    TextBox_id.Text = ds.Products.Rows[i][0].ToString();
                    TextBox_nom.Text = ds.Products.Rows[i][1].ToString();
                    TextBox_qty.Text = ds.Products.Rows[i][2].ToString();
                    TextBox_prix.Text = ds.Products.Rows[i][3].ToString();
                    ComboBox_cat.Text= guna2DataGridView1.Rows[e.RowIndex].Cells["nomCat"].Value.ToString();
                    TextBox_id.Enabled = false;
                    break;
                }
            }
        }

        private void Btn_mod_Click(object sender, EventArgs e)
        {
            try
            {
                Categorie.res = 0;
                string msg = "Modifier la categorie";
                Message m = new Message(msg);
                m.ShowDialog();
                if (Categorie.res == 1)
                {
                    for (int i = 0; i < ds.Products.Count; i++)
                    {
                        if (TextBox_id.Text == ds.Products.Rows[i][0].ToString())
                        {
                            ds.Products.Rows[i][1] = TextBox_nom.Text;
                            ds.Products.Rows[i][2] = TextBox_qty.Text;
                            ds.Products.Rows[i][3] = TextBox_prix.Text;
                            ds.Products.Rows[i][4] = ComboBox_cat.SelectedValue.ToString();
                            pr.Update(ds.Products);
                            ds.AcceptChanges();
                            rempligrid();
                        }
                    }
                }
            }
            catch
            {
                string msg = "Error des infos";
                Message m = new Message(msg);
                m.ShowDialog();
            }
        }

        private void Btn_sup_Click(object sender, EventArgs e)
        {
            try
            {
                Categorie.res = 0;
                string msg = "Supprimer la categorie";
                Message m = new Message(msg);
                m.ShowDialog();
                if (Categorie.res == 1)
                {
                    for (int i = 0; i < ds.Products.Count; i++)
                    {
                        if (TextBox_id.Text == ds.Products.Rows[i][0].ToString())
                        {
                            ds.Products.Rows[i].Delete();
                            pr.Update(ds.Products);
                            ds.AcceptChanges();
                            rempligrid();
                            vider();
                        }
                    }
                }
            }
            catch
            {
                string msg = "impossible de supprission";
                Message m = new Message(msg);
                m.btn_Ok.Visible = false;
                m.ShowDialog();
            }
        }

        private void btn_acc_Click(object sender, EventArgs e)
        {
            this.Close();
            Acceuil ac = new Acceuil();
            ac.Show();
        }
    }
}
