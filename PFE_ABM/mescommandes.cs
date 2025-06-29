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
    public partial class mescommandes : Form
    {
        DataSetAchat ds = new DataSetAchat();
        commandeTableAdapter cmd = new commandeTableAdapter();
        ClientsTableAdapter cl = new ClientsTableAdapter();
        int num = -1;
        bool f = false;
        public mescommandes()
        {
            InitializeComponent();
        }

        private void btn_acc_Click(object sender, EventArgs e)
        {
            this.Close();
            Acceuil ac = new Acceuil();
            ac.Show();
        }
        public void remplirgrid()
        {
            var qr = from cmd in ds.commande
                     join c in ds.Clients on cmd.idClient equals c.idClient
                     select new
                     {
                         cmd.idCmd,
                         c.NomClient,
                         cmd.etat_pays,
                         cmd.etat_liv,
                         cmd.adresse
                     };
            DataGridView_prod.DataSource = qr.ToList();
        }
        public void rech(int id)
        {
            var qr = from cmd in ds.commande
                     join c in ds.Clients on cmd.idClient equals c.idClient
                     where cmd.idClient==id
                     select new
                     {
                         cmd.idCmd,
                         c.NomClient,
                         cmd.etat_pays,
                         cmd.etat_liv,
                         cmd.adresse
                     };
            DataGridView_prod.DataSource = qr.ToList();
        }

        private void mescommandes_Load(object sender, EventArgs e)
        {
            cmd.Fill(ds.commande);
            cl.Fill(ds.Clients);
            ComboBox_cat.DataSource = ds.Clients;
            ComboBox_cat.DisplayMember = "NomClient";
            ComboBox_cat.ValueMember = "idClient";

            remplirgrid();
            f = true;
        }

        private void ComboBox_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (f)
            {
                rech(int.Parse(ComboBox_cat.SelectedValue.ToString()));
            }
        }

        private void DataGridView_prod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView_prod.CurrentRow.Selected = true;
             num = int.Parse(DataGridView_prod.Rows[e.RowIndex].Cells["idCmd"].Value.ToString());
        }

        private void btn_val_Click(object sender, EventArgs e)
        {
            if (num != -1)
            {
                Facturation fc = new Facturation(num);
                fc.Show();
            }
            else
            {
                string msg = "Selection un ligne";
                Message m = new Message(msg);
                m.ShowDialog();
            }
        }
    }   
}
