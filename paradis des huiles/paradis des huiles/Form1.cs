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
using System.Security.Cryptography;

namespace paradis_des_huiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection("Server='.'; Database= DB_Gestionn ;Integrated Security = true");

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select pass from Accounts where username = '" + TxtLogin.Text.ToString()+"'",cn);
            cn.Open();
            if (cmd.ExecuteScalar() != null)
            {
                string s = cmd.ExecuteScalar().ToString();
                string s1 = CalculateMD5Hash(gunaTextBox1.Text).ToString();
                if ( s1.ToString() == s.ToString())
                {
                    menu menu = new menu();
                    menu.Show();
                    menu.Text = "Menu Principal Session : [" + TxtLogin.Text.ToString() + "]";
                    this.Hide();
                }
                else
                {
                    txtWrong.Text = "Mot de passe ou nom d'utilisateur inconnu veuillez verifier vos information";
                    txtWrong.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                txtWrong.Text = "Mot de passe ou nom d'utilisateur inconnu veuillez verifier vos information";
                txtWrong.ForeColor = System.Drawing.Color.Red;
            }
            cn.Close();

        }
        public string CalculateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString().ToLower();
        }

    }
}
