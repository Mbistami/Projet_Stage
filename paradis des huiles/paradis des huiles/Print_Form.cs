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
            else if(data.TableName == "Fournisseur")
            {

            }

            
        }
        /*public string crypt(string pass)
        {
            MD5 mD = new MD5CryptoServiceProvider();
            mD.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass));

            byte[] result = mD.Hash;
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 0; i < result.Length; i++)
            {
                stringBuilder.Append(result[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }*/
        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
