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

namespace paradis_des_huiles
{
    public partial class menu : Form
    {
        SqlConnection cn = new SqlConnection("Server=.;Database=DB_Gestion;Integrated Security = true");
        public menu()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabAddFourni_Click(object sender, EventArgs e)
        {

        }

        private void tabUpdate_Click(object sender, EventArgs e)
        {

        }
        DataSet DataSet1 = new DataSet();
        private void menu_Load(object sender, EventArgs e)
        {
            
            cn.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter("select nomClt + ' ' + ISNULL( prenomClt, '') as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);

            dataAdapter1.Fill(DataSet1, "Client");
            cn.Close();           
            dataGridClient.DataSource = DataSet1.Tables["Client"];

        }

        private void TxtRchDgrid_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["Client"]);
            dataView.RowFilter = "NomClient like '%" + TxtRchDgrid.Text + "%'";

            dataGridClient.DataSource = dataView;
        }
    }
}
