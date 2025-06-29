using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PFE_ABM
{
    public partial class Acceuil : Form
    {
        public Acceuil()
        {
            InitializeComponent();
        }

        private void Acceuil_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Pr_Click(object sender, EventArgs e)
        {
            this.Close();
            Produits p = new Produits();
            p.Show();
        }

        private void Btn_Ct_Click(object sender, EventArgs e)
        {
            this.Close();
            Categorie c = new Categorie();
            c.Show();
        }

        private void Btn_cl_Click(object sender, EventArgs e)
        {
            this.Close();
            Clients cl = new Clients();
            cl.Show();
        }

        private void Btn_cm_Click(object sender, EventArgs e)
        {
            this.Close();
            Commande cmd = new Commande();
            cmd.Show();
        }

        private void Btn_em_Click(object sender, EventArgs e)
        {
            this.Close();
            Employees em = new Employees();
            em.Show();
        }

        private void Btn_dec_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 a = new Form1();
            a.Show();
        }
    }
}
