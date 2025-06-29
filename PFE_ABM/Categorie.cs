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
    public partial class Categorie : Form
    {
        ADO ado = new ADO();

        public static int res = 0;
        public Categorie()
        {
            InitializeComponent();
        }

        private void Categorie_Load(object sender, EventArgs e)
        {
            ado.Ds = new DataSetAchat();

            CategorieTableAdapter cat = new CategorieTableAdapter();
            cat.Fill(ado.Ds.Categorie);

            guna2DataGridView1.DataSource = ado.Ds.Tables["Categorie"];
        }

        private void btn_acc_Click(object sender, EventArgs e)
        {
            this.Close();
            Acceuil ac = new Acceuil();
            ac.Show();
        }

        private void Btn_ajt_Click(object sender, EventArgs e)
        {
            try
            {
                ado.Ds = new DataSetAchat();

                CategorieTableAdapter cat = new CategorieTableAdapter();
                cat.Fill(ado.Ds.Categorie);

                ado.Ds.Tables["Categorie"].Rows.Add(int.Parse(TextBox_idC.Text), TextBox_Nom.Text);

                Builder();
                guna2DataGridView1.DataSource = ado.Ds.Tables["Categorie"];
                string msg = "La categorie ajouté avec succès!";
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
        private void Builder()
        {
            ado.Dadap = new System.Data.SqlClient.SqlDataAdapter("select *from Categorie", ado.Con);
            ado.Builder = new System.Data.SqlClient.SqlCommandBuilder(ado.Dadap);
            ado.Dadap.Update(ado.Ds, "Categorie");

        }

        private void Btn_mod_Click(object sender, EventArgs e)
        {
            res = 0;
            string msg = "Modifier la categorie";
            Message m = new Message(msg);
            m.ShowDialog();
            if (res == 1)
            {
                ado.Ds = new DataSetAchat();

                CategorieTableAdapter cat = new CategorieTableAdapter();
                cat.Fill(ado.Ds.Categorie);
                ado.Bs = new BindingSource();
                ado.Bs.DataSource = ado.Ds.Categorie;
                var p = guna2DataGridView1.CurrentCell.RowIndex;
                ado.Ds.Tables["Categorie"].Rows[p][1] = TextBox_Nom.Text;

                Builder();
                guna2DataGridView1.DataSource = ado.Ds.Tables["Categorie"];

            }
        }

        private void Btn_sup_Click(object sender, EventArgs e)
        {
            res = 0;
            string msg = "Supprimer la categorie";
            Message m = new Message(msg);
            m.ShowDialog();
            if (res == 1)
            {
                int r = guna2DataGridView1.CurrentCell.RowIndex;
                guna2DataGridView1.Rows.RemoveAt(r);

                ado.Ds.Tables["Categorie"].Rows[r].Delete();
                Builder();
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id_cat = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            var nom_cat = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            TextBox_idC.Text = id_cat;
            TextBox_Nom.Text = nom_cat;
        }
    }
}
