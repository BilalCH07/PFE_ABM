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
    public partial class Form1 : Form
    {
        ADO ado = new ADO();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TextBoxpass.UseSystemPasswordChar = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Acceuil a = new Acceuil();

                ado.Dadap = new System.Data.SqlClient.SqlDataAdapter("select *from Adminn where loginn='" + TextBox_log.Text + "' and MotPass='" + TextBoxpass.Text + "'", ado.Con);
                DataTable dt = new DataTable();
                ado.Dadap.Fill(dt);

                ado.Ds = new DataSetAchat();
                AdminnTableAdapter admin = new AdminnTableAdapter();
                admin.Fill(ado.Ds.Adminn);
                if (dt.Rows.Count > 0)
                {
                    //this.Hide();
                    a.Show();
                }
                else
                {
                    Message m = new Message();
                    m.Show();
                    m.Messagefrm.Text = "Not Found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                TextBoxpass.UseSystemPasswordChar = true;
            }
            else { TextBoxpass.UseSystemPasswordChar = false; }
        }
    }
}
