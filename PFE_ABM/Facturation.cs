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
    public partial class Facturation : Form
    {
        DataSetAchat ds = new DataSetAchat();
        ProductsTableAdapter pr = new ProductsTableAdapter();
        commandeTableAdapter cmd = new commandeTableAdapter();
        details_commTableAdapter det = new details_commTableAdapter();
        ClientsTableAdapter cl = new ClientsTableAdapter();
        int id;
        public Facturation()
        {
            InitializeComponent();
        }
        public Facturation(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Facturation_Load(object sender, EventArgs e)
        {
            pr.Fill(ds.Products);
            cmd.Fill(ds.commande);
            det.Fill(ds.details_comm);
            cl.Fill(ds.Clients);
            Factura f = new Factura();
            f.SetDataSource(ds);
            String filter = "{commande.idCmd} = "+id+"";
            crystalReportViewer1.ReportSource = f;
            crystalReportViewer1.SelectionFormula = filter;
            crystalReportViewer1.Refresh();
        }
    }
}
