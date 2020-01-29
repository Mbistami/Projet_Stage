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

        private void menu_Load(object sender, EventArgs e)
        {
            /*DataSet DataSet1 = new DataSet();
            cn.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter("Select * from Client", cn);
            SqlCommandBuilder commandBuilder1 = new SqlCommandBuilder(dataAdapter1);
            DataSet1.Tables.Add("Client");
            dataAdapter1.Fill(DataSet1, "Client");
            cn.Close();
            dataGridClient.DataSource = DataSet1;
            dataGridClient.DataMember = "Client";*/
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select * from Client", cn);
            SqlDataReader dtR = cmd.ExecuteReader();
            while(dtR.Read())
            {
                string s;
                if (dtR[6].Equals(1))
                    s = "Fidéle";
                else
                    s = "Non Fidéle";
                dataGridClient.Rows.Add(dtR[1].ToString() + ' ' + dtR[2].ToString(), dtR[3].ToString(), dtR[5].ToString(), dtR[4].ToString(),s.ToString(), dtR[0].ToString());
            }
            cn.Close();
        }

        private void TxtRchDgrid_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView();
            //dataView.
        }
    }
}
