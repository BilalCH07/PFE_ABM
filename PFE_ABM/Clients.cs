using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PFE_ABM
{
    public partial class Clients : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=.;Initial Catalog=PFE_data;Integrated Security=True");
        public Clients()
        {
            InitializeComponent();
        }
        public void charger()
        {

            DataSet ds = new DataSet();
            SqlDataAdapter dp = new SqlDataAdapter("select * from Clients", cn);
            dp.Fill(ds, "clt");
            guna2DataGridView1.DataSource = ds.Tables["clt"];
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            charger();
        }
        public void vider()
        {
            TextBox_nom.Text = "";
            TextBox_tele.Text = "";
        }

        private void Btn_ajt_Click(object sender, EventArgs e)
        {
            if (TextBox_nom.Text != "" && TextBox_tele.Text != "")
            {
                DataSet ds = new DataSet();
                SqlDataAdapter dr = new SqlDataAdapter("select * from Clients", cn);
                dr.Fill(ds, "clt");

                DataRow drr = ds.Tables["clt"].NewRow();
                drr[1] = TextBox_nom.Text;
                drr[2] = TextBox_tele.Text;
                ds.Tables["clt"].Rows.Add(drr);
                SqlCommandBuilder dn = new SqlCommandBuilder(dr);
                dr.Update(ds, "clt");
                guna2DataGridView1.DataSource = "";
                charger();
                string msg = " Client ajouté avec succès!";
                Message m = new Message(msg);
                m.btn_cancel.Visible = false;
                m.ShowDialog();
                vider();
            }
            else
            {
                string msg = "Saisir infos";
                Message m = new Message(msg);
                m.ShowDialog();
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter("select * from Clients", cn);
            dr.Fill(ds, "clt");

            var nom = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            var tele = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();

            TextBox_nom.Text = nom;
            TextBox_tele.Text = tele;
        }

        private void Btn_sup_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter("select * from Clients", cn);
            dr.Fill(ds, "clt");
            var id = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int pos = -1;
            for (int i = 0; i < ds.Tables["clt"].Rows.Count; i++)
            {
                if (id == ds.Tables["clt"].Rows[i][0].ToString() && TextBox_nom.Text != "")
                {
                    pos = i;
                }
            }
            if (pos == -1)
            {
                string msg = "selectionner un ligne";
                Message m = new Message(msg);
                m.btn_Ok.Visible = false;
                m.ShowDialog();
            }
            else
            {
                ds.Tables["clt"].Rows[pos].Delete();
                SqlCommandBuilder dn = new SqlCommandBuilder(dr);
                dr.Update(ds, "clt");
                guna2DataGridView1.DataSource = "";
                charger();
                string msg = "bien Supprimer";
                Message m = new Message(msg);
                m.btn_cancel.Visible = false;
                m.ShowDialog();
                vider();
            }
        }

        private void Btn_mod_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter("select * from Clients", cn);
            dr.Fill(ds, "clt");
            var id = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int p = -1;

            for (int i = 0; i < ds.Tables["clt"].Rows.Count; i++)
            {
                if (id == ds.Tables["clt"].Rows[i][0].ToString() && TextBox_nom.Text != ""&&TextBox_tele.Text!="")
                {
                    ds.Tables["clt"].Rows[i][1] = TextBox_nom.Text;
                    ds.Tables["clt"].Rows[i][2] = TextBox_tele.Text;
                    SqlCommandBuilder dn = new SqlCommandBuilder(dr);
                    dr.Update(ds, "clt");
                    guna2DataGridView1.DataSource = "";
                    charger();
                    vider();
                    p = 1;


                }

            }
            if (p == 1)
            {
                string msg = "bien modifier";
                Message m = new Message(msg);
                m.ShowDialog();

            }
            else
            {
                string msg = "selectionner un ligne";
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
