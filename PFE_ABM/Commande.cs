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
    public partial class Commande : Form
    {
        DataSetAchat ds = new DataSetAchat();
        ClientsTableAdapter cl = new ClientsTableAdapter();
        ProductsTableAdapter pr = new ProductsTableAdapter();
        CategorieTableAdapter ct = new CategorieTableAdapter();
        commandeTableAdapter cmd = new commandeTableAdapter();
        details_commTableAdapter det = new details_commTableAdapter();
        public Commande()
        {
            InitializeComponent();
        }
        public void recherch(int id)
        {
            var qr = from pr in ds.Products
                     join c in ds.Categorie on pr.idCat equals c.idCat
                     where pr.idCat==id
                     select new
                     {
                         pr.idProd,
                         pr.nomProd,
                         pr.QtProd,
                         pr.PrixProd,
                         c.nomCat
                     };
            DataGridView_prod.DataSource = qr.ToList();
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
            DataGridView_prod.DataSource = qr.ToList();
        }
        private void Commande_Load(object sender, EventArgs e)
        {
            cl.Fill(ds.Clients);
            pr.Fill(ds.Products);
            ct.Fill(ds.Categorie);
            cmd.Fill(ds.commande);
            det.Fill(ds.details_comm);

            ComboBox_client.DataSource = ds.Clients;
            ComboBox_client.DisplayMember = "NomClient";
            ComboBox_client.ValueMember = "idClient";

            ComboBox_cat.DataSource = ds.Categorie;
            ComboBox_cat.DisplayMember = "nomCat";
            ComboBox_cat.ValueMember = "idCat";

            rempligrid();
        }

        private void btn_ajt_Click(object sender, EventArgs e)
        {
            string id_prod = DataGridView_prod.SelectedRows[0].Cells[0].Value.ToString();
            string id_client = ComboBox_client.SelectedValue.ToString();
            string qty = TextBox_qty.Text;
            double prix_t = double.Parse(qty) * double.Parse(DataGridView_prod.SelectedRows[0].Cells[3].Value.ToString());
            double tl = double.Parse(totaldh.Text);
            if(int.Parse(qty)<= int.Parse(DataGridView_prod.SelectedRows[0].Cells[2].Value.ToString()))
            {
                tl += prix_t;
                totaldh.Text = tl.ToString();

                for (int i = 0; i < ds.Products.Count; i++)
                {
                    if (id_prod == ds.Products.Rows[i][0].ToString())
                    {
                        int f = int.Parse(ds.Products.Rows[i][2].ToString());
                        ds.Products.Rows[i][2] = f - int.Parse(qty);
                        rempligrid();
                    }
                }
                DataGridView_panier.Rows.Add(id_prod, id_client, qty, prix_t.ToString());
            }
            else
            {
                string msg = " La quantité n'existe pas";
                Message m = new Message(msg);
                m.btn_cancel.Visible = false;
                m.ShowDialog();
            }
        }

        private void btn_chercher_Click(object sender, EventArgs e)
        {
            recherch(int.Parse(ComboBox_cat.SelectedValue.ToString()));
        }

        private void btn_val_Click(object sender, EventArgs e)
        {
            bool p=false;
            bool liv = false;
            if(ComboBox_etat.SelectedItem.ToString()== "Payé")
            {
                p = true;
            }
            else
            {
                p = false;
            }
            if (ComboBox_etat_liv.SelectedItem.ToString() == "sur place")
            {
                liv = true;
            }
            else
            {
                liv = false;
            }
            int idcmd = ds.commande.Count + 1;
            DataRow row_cmd = ds.commande.NewRow();
            row_cmd[0] = idcmd;
            row_cmd[1]= ComboBox_client.SelectedValue.ToString();
            row_cmd[2]=DateTime.Now.Date;
            row_cmd[3]=liv;
            row_cmd[4]=p;
            row_cmd[5] = TextBox_adrs.Text;
            ds.commande.Rows.Add(row_cmd);
            for (int i = 0; i < DataGridView_panier.Rows.Count-1; i++)
            {

                if (DataGridView_panier.Rows.Count>0)
                {
                    DataRow row = ds.details_comm.NewRow();
                    row[0]=idcmd;
                    row[1]= DataGridView_panier.Rows[i].Cells[0].Value.ToString();
                    row[2]= DataGridView_panier.Rows[i].Cells[2].Value.ToString();
                    ds.details_comm.Rows.Add(row);
                    
                }
            }
            pr.Update(ds.Products);
            cmd.Update(ds.commande);
            det.Update(ds.details_comm);
            ds.AcceptChanges();
            totaldh.Text = "0";
            string msg = "bien commandé";
            Message m = new Message(msg);
            m.ShowDialog();
            DataGridView_panier.Rows.Clear();

        }

        private void btn_acc_Click(object sender, EventArgs e)
        {
            this.Close();
            Acceuil ac = new Acceuil();
            ac.Show();
        }

        private void btn_mes_Click(object sender, EventArgs e)
        {
            this.Close();
            mescommandes mc = new mescommandes();
            mc.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string id_prod = DataGridView_panier.SelectedRows[0].Cells[0].Value.ToString();
                int indix = DataGridView_panier.CurrentCell.RowIndex;
                string qty = DataGridView_panier.SelectedRows[0].Cells[2].Value.ToString();
                for (int i = 0; i < ds.Products.Count; i++)
                {
                    if (id_prod == ds.Products.Rows[i][0].ToString())
                    {
                        int f = int.Parse(ds.Products.Rows[i][2].ToString());
                        ds.Products.Rows[i][2] = f + int.Parse(qty);
                        rempligrid();
                    }
                }


                DataGridView_panier.Rows.RemoveAt(indix);
            }
            catch
            {
                string msg = " select un ligne";
                Message m = new Message(msg);
                m.btn_cancel.Visible = false;
                m.ShowDialog();
            }
        }

        private void DataGridView_prod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
