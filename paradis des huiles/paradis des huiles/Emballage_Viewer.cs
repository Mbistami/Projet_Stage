using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace paradis_des_huiles
{
    public partial class Emballage_Viewer : Form
    {
        public Emballage_Viewer()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection("Server='.';Database=DB_Gestion;Integrated Security = true");
        DataSet DataSet = new DataSet();
        int Check;

        private void Emballage_Viewer_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox1.Checked = false;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from emballage where idEM = '" + textBox1.Text.ToString().Substring(2) + "'", cn);
            cn.Open();
            sqlDataAdapter.Fill(DataSet, "Emballage");
            sqlDataAdapter = new SqlDataAdapter("select * from Etat where idE = '" + DataSet.Tables["Emballage"].Rows[0][0].ToString() + "'", cn);
            sqlDataAdapter.Fill(DataSet, "Etat");
            sqlDataAdapter = new SqlDataAdapter("select * from fournisseur where RCF = '" + DataSet.Tables["Emballage"].Rows[0][1].ToString() + "'", cn);
            sqlDataAdapter.Fill(DataSet, "Fournisseur");
            cn.Close();
            SqlCommand cmd = new SqlCommand("select img from emballage where idEM = '" + textBox1.Text.ToString().Substring(2) + "'", cn);
            cn.Open();
            SqlDataReader sql = cmd.ExecuteReader();
            sql.Read();
            if (sql[0].Equals(System.DBNull.Value))
                MessageBox.Show("Aucune image disponible pour l'emballage veullez cliquer sur modifier* pour ajouter une nouvelle", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                byte[] vs = (byte[])sql[0];
                using (var ms = new MemoryStream(vs))
                {
                    Image image = Image.FromStream(ms);
                    this.picEMV.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.picEMV.Image = image;
                }
            }
            cn.Close();
            this.textBox2.Text = DataSet.Tables["Emballage"].Rows[0][3].ToString();
            if (DataSet.Tables["Etat"].Rows[0][0].ToString() == "1")
                label8.Text = "Support total en mL :";
            else
                label8.Text = "Support total en kG :";

            this.textBox3.Text = DataSet.Tables["Emballage"].Rows[0][4].ToString();
            this.textBox4.Text = DataSet.Tables["Emballage"].Rows[0][6].ToString();
            this.richTextBox1.Text = DataSet.Tables["Emballage"].Rows[0][5].ToString();
            this.label1.Text = "RCF fournisseur : " + DataSet.Tables["Fournisseur"].Rows[0][0];
            this.label2.Text = "Nom : " + DataSet.Tables["Fournisseur"].Rows[0][1];
            this.label3.Text = "Numéro : " + DataSet.Tables["Fournisseur"].Rows[0][2];
            this.label4.Text = "Adresse : " + DataSet.Tables["Fournisseur"].Rows[0][3];
            this.label5.Text = "Adresse Mail : " + DataSet.Tables["Fournisseur"].Rows[0][4];
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.textBox2.Enabled = true;
                this.textBox3.Enabled = true;
                this.textBox4.Enabled = true;
                this.richTextBox1.Enabled = true;
            }
            else
            {
                this.textBox1.Enabled = false;
                this.textBox2.Enabled = false;
                this.textBox3.Enabled = false;
                this.textBox4.Enabled = false;
                this.richTextBox1.Enabled = false;
            }
            Check += 1;
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                picEMV.SizeMode = PictureBoxSizeMode.StretchImage;
                picEMV.Image = new Bitmap(open.FileName);
                lblLink.Text = open.FileName;
            }
            FileStream stream = File.OpenRead(open.FileName);
            byte[] vs = new byte[stream.Length];
            stream.Read(vs, 0, vs.Length);
            stream.Close();
            DataSet.Tables["Emballage"].Rows[0][2] = vs;
        }

        private void Emballage_Viewer_FormClosing(object sender, FormClosingEventArgs e)
        { 
            if(Check > 2)
            {
                DialogResult result = MessageBox.Show("Vous avez activer la modification n'oubliez pas de sauvgarder les modifications", "Rappel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result == DialogResult.Yes)
                {
                    this.gunaButton4.PerformClick();
                }
            }
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from emballage where idEM = '" + textBox1.Text.ToString().Substring(2) + "'", cn);
            DataSet.Tables["Emballage"].Rows[0][3] = this.textBox2.Text.ToString();
            DataSet.Tables["Emballage"].Rows[0][4] = this.textBox3.Text.ToString();
            DataSet.Tables["Emballage"].Rows[0][5] = this.richTextBox1.Text.ToString();
            DataSet.Tables["Emballage"].Rows[0][6] = this.textBox4.Text.ToString();
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            cn.Open();
            sqlDataAdapter.Update(DataSet, "Emballage");
            cn.Close();
            MessageBox.Show("Modification Enregistrer","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Check = 2;
        }
    }
}
