using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using PFE_ABM.DataSetAchatTableAdapters;

namespace PFE_ABM
{
    class ADO
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=PFE_data;Integrated Security=True");
        DataSetAchat ds;
        SqlDataAdapter dadap;
        BindingSource bs = new BindingSource();
        SqlCommandBuilder builder;



        public ADO(SqlConnection con, DataSetAchat ds, SqlDataAdapter dadap, BindingSource bs, SqlCommandBuilder builder)
        {
            this.con = con;
            this.ds = ds;
            this.dadap = dadap;
            this.bs = bs;
            this.builder = builder;

        }

        public SqlConnection Con { get => con; set => con = value; }
        public DataSetAchat Ds { get => ds; set => ds = value; }
        public SqlDataAdapter Dadap { get => dadap; set => dadap = value; }
        public BindingSource Bs { get => bs; set => bs = value; }
        public SqlCommandBuilder Builder { get => builder; set => builder = value; }


        public ADO()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=PFE_data;Integrated Security=True");
            DataSetAchat ds = new DataSetAchat();
            SqlDataAdapter dadap = new SqlDataAdapter();
            BindingSource bs = new BindingSource();
            SqlCommandBuilder builder = new SqlCommandBuilder();

        }
    }
}
