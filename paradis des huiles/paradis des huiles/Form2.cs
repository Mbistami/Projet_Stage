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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
        
        }

        public void crstl(DataTable dt1)
        {
            CrystalReport1 cr = new CrystalReport1();
            CrystalReport2 cf = new CrystalReport2();
            if (dt1.TableName.ToString() == "Fournisseur")
            {
                cf.SetDataSource(dt1);
                crystalReportViewer1.ReportSource = cf;
            }
            else if (dt1.TableName.ToString() == "Client")
            {
                cr.SetDataSource(dt1);
                crystalReportViewer1.ReportSource = cr;
            }
            else if(dt1.TableName.ToString() == "Emballage")
            {

            }
        }
    }
}
