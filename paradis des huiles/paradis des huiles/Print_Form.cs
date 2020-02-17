using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace paradis_des_huiles
{
    public partial class Print_Form : Form
    {
        public Print_Form()
        {
            InitializeComponent();
        }
        public void Printer(DataTable data)
        {
            if (data.TableName == "Client")
            {
                CrystalReport4 cr = new CrystalReport4();
                crystalReportViewer1.ReportSource = cr;
                cr.SetDataSource(data);
            }
            else if (data.TableName == "Fournisseur")
            {
                CrystalReport5 cr = new CrystalReport5();
                crystalReportViewer1.ReportSource = cr;
                cr.SetDataSource(data);
            }
            else if (data.TableName == "Emballage")
            {
                CrystalReport6 cr = new CrystalReport6();
                crystalReportViewer1.ReportSource = cr;
                cr.SetDataSource(data);

            }
            else if (data.TableName == "HistoriqueV")
            {
                CrystalReport7 cr = new CrystalReport7();
                crystalReportViewer1.ReportSource = cr;
                cr.SetDataSource(data);
            }
            else if (data.TableName == "MatiereP")
            {
                CrystalReport8 cr = new CrystalReport8();
                crystalReportViewer1.ReportSource = cr;
                cr.SetDataSource(data);
            }
            else if (data.TableName == "HistoriqueA")
            {
                CrystalReport9 cr = new CrystalReport9();
                crystalReportViewer1.ReportSource = cr;
                cr.SetDataSource(data);
            }
            else if (data.TableName == "Produit_finis")
            {
                CrystalReport10 cr = new CrystalReport10();
                crystalReportViewer1.ReportSource = cr;
                cr.SetDataSource(data);
            }

            
        }
    }
}
