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
    public partial class Message : Form
    {
        string m;
        public Message()
        {
            InitializeComponent();
        }
        public Message(string m)
        {
            InitializeComponent();
            this.m = m;
        }

        private void Message_Load(object sender, EventArgs e)
        {
            Messagefrm.Text = m;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            Categorie.res = 1;
            this.Close();
        }
    }
}
