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
    public partial class Employees : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=.;Initial Catalog=PFE_data;Integrated Security=True");

        public Employees()
        {
            InitializeComponent();
        }
        public void charger()
        {

            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter("select * from Adminn", cn);
            dr.Fill(ds, "admn");
            guna2DataGridView1.DataSource = ds.Tables["admn"];
        }
        public void vider()
        {
            TextBox_log.Text = "";
            TextBox_nom.Text = "";
            TextBox_mot.Text = "";
            ComboBox_prof.Text = "";
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            charger();
        }

        private void Btn_ajt_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter("select * from Adminn", cn);
            dr.Fill(ds, "admn");
            try
            {
                if (TextBox_log.Text != "" && TextBox_nom.Text != "" && TextBox_mot.Text != "" && ComboBox_prof.Text != "")
                {
                    DataRow drr = ds.Tables["admn"].NewRow();
                    drr[0] = TextBox_log.Text;
                    drr[1] = TextBox_nom.Text;
                    drr[2] = TextBox_mot.Text;
                    drr[3] = ComboBox_prof.Text;

                    ds.Tables["admn"].Rows.Add(drr);
                    SqlCommandBuilder dn = new SqlCommandBuilder(dr);
                    dr.Update(ds, "admn");
                    guna2DataGridView1.DataSource = "";
                    charger();
                    string msg = " Employee ajouté avec succès!";
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
            catch
            {
                MessageBox.Show("deja exist");
            }
        }

        private void Btn_sup_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter("select * from Adminn", cn);
            dr.Fill(ds, "admn");
            int pos = -1;
            bool tt = false;
            if (TextBox_log.Text != "")

                for (int i = 0; i < ds.Tables["admn"].Rows.Count; i++)
                {
                    if (TextBox_log.Text == ds.Tables["admn"].Rows[i][0].ToString())
                    {
                        pos = i;
                        tt = true;
                    }


                }
            if (tt == true)
            {
                ds.Tables["admn"].Rows[pos].Delete();
                tt = false;
                pos = -1;

                SqlCommandBuilder dn = new SqlCommandBuilder(dr);
                dr.Update(ds, "admn");
                guna2DataGridView1.DataSource = "";
                charger();
                string msg = "bien Supprimer";
                Message m = new Message(msg);
                m.btn_cancel.Visible = false;
                m.ShowDialog();
                vider();
            }
            else
            {
                string msg = "selectionner un ligne";
                Message m = new Message(msg);
                m.btn_Ok.Visible = false;
                m.ShowDialog();
            }
        }

        private void Btn_mod_Click(object sender, EventArgs e)
        {
            int p = -1;
            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter("select * from Adminn", cn);
            dr.Fill(ds, "admn");

            for (int i = 0; i < ds.Tables["admn"].Rows.Count; i++)
            {
                if (TextBox_log.Text == ds.Tables["admn"].Rows[i][0].ToString())
                {
                    ds.Tables["admn"].Rows[i][1] = TextBox_nom.Text;
                    ds.Tables["admn"].Rows[i][2] = TextBox_mot.Text;
                    ds.Tables["admn"].Rows[i][3] = ComboBox_prof.Text;
                    SqlCommandBuilder scd = new SqlCommandBuilder(dr);
                    dr.Update(ds, "admn");
                    p = 1;


                }


            }
            if (p == -1)
            {
                string msg = "selectionner un ligne";
                Message m = new Message(msg);
                m.btn_Ok.Visible = false;
                m.ShowDialog();
            }
            else
            {
                string msg = "bien modifier";
                Message m = new Message(msg);
                m.ShowDialog();
            }



            vider();
            guna2DataGridView1.DataSource = "";
            charger();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dr = new SqlDataAdapter("select * from Adminn", cn);
            dr.Fill(ds, "admn");

            var log = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            var nom = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            var mot = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            var prfl = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();

            TextBox_log.Text = log;
            TextBox_nom.Text = nom;
            TextBox_mot.Text = mot;
            ComboBox_prof.Text = prfl;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Acceuil ac = new Acceuil();
            ac.Show();
        }
    }
}
