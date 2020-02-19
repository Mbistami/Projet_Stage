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
        // perm level [1 = read only / 2 = update read / 3 = read update insert / 4 = fullperms]
        SqlConnection cn = new SqlConnection("Server='192.168.1.30, 1433'; Database= DB_Gestion ;user id='Admin';password='admin'");

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
                    SqlCommand cmd1 = new SqlCommand("Select permLevel from Accounts where username = '" + TxtLogin.Text.ToString() + "'", cn);
                    int permlvl = Convert.ToInt32(cmd1.ExecuteScalar());
                    menu menu = new menu();
                    menu.permleve = permlvl;
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

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void gunaTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gunaGradientButton1_Click(sender, e);
            }
        }
    }
}
