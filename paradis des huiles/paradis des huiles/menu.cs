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
        
        DataSet DataSet1 = new DataSet();
        private void menu_Load(object sender, EventArgs e)
        {

            //combobox rechercher afficher clinet 
            CmbRechClientAfficher.Items.Add("NomClient");
            CmbRechClientAfficher.Items.Add("Num Tel");
            CmbRechClientAfficher.Items.Add("Adresse");
            CmbRechClientAfficher.Items.Add("Mail");
            CmbRechClientAfficher.Items.Add("Type");
            CmbRechClientAfficher.Items.Add("RC / CIN");
            CmbRechClientAfficher.Text = CmbRechClientAfficher.Items[0].ToString();

            //cmbtypecltadd type client add
            cmbtypecltadd.Items.Add("infidele");
            cmbtypecltadd.Items.Add("fidele");
            cmbtypecltadd.Text = cmbtypecltadd.Items[0].ToString();

            //cmbTypeAddFourni type fournisseur Add



            cn.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select nomClt + ' ' + ISNULL( prenomClt, '') as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
            dataAdapter.Fill(DataSet1, "Client");

            dataAdapter = new SqlDataAdapter("select nomFournisseur [Nom Fourni] ,numTel [Num Tel] , adresse [Adresse] , email [Mail] , RCF [RC] from Fournisseur", cn);
            dataAdapter.Fill(DataSet1, "Fournisseur");

            dataAdapter = new SqlDataAdapter("select idEm  + codeEM [Code Emballage], nomFournisseur [Nom Fournisseur] , qteEM [Quantite] , NomE [Type] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
            dataAdapter.Fill(DataSet1, "Emballage");

            dataAdapter = new SqlDataAdapter("select idMP  + codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , NomE [Type] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
            dataAdapter.Fill(DataSet1, "MatiereP");

            dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(nomMP, '') + ISNULL(codeEM + Emballage.idE, '') [Nom Produit], ISNULL(nomFournisseur, '') + ISNULL(nomFournisseur, '') [Fournisseur], qtea [Quantite], prix  [Prix], dateA [Date Achat]from historique_achat inner join Matiere_premiere on historique_achat.idMP=Matiere_premiere.idmp/* or */ inner join Emballage on Emballage.idEM = historique_achat.idEM inner join Fournisseur on Fournisseur.RCF = Emballage.RCF  or  Fournisseur.RCF=Matiere_premiere.RCF", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueA");

            dataAdapter = new SqlDataAdapter("select numVente [Num Vente], Produit_finis.idPF + codePF [Nom Produit], nomClt + ' ' + ISNULL(prenomClt,'') [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueV");

            cn.Close();

            dataGridClient.DataSource = DataSet1.Tables["Client"];
            dataGridFourni.DataSource = DataSet1.Tables["Fournisseur"];
            dataGridEmballage.DataSource = DataSet1.Tables["Emballage"];
            dataGridMatiereP.DataSource = DataSet1.Tables["MatiereP"];
            dataGrideHistoriqueA.DataSource = DataSet1.Tables["HistoriqueA"];
            dataGridHistoAUpdate.DataSource = DataSet1.Tables["HistoriqueA"];
            dataGrideHistoriqueV.DataSource = DataSet1.Tables["HistoriqueV"];
            dataGridHistoVUpdate.DataSource = DataSet1.Tables["HistoriqueV"];

            //CmbRechFournisseurAfficher afficher rechercher fournisseur
            for (int i = 0; i < DataSet1.Tables["Fournisseur"].Columns.Count; i++)
            {
                CmbRechFournisseurAfficher.Items.Add(DataSet1.Tables["Fournisseur"].Columns[i].ColumnName.ToString());
            }
            CmbRechFournisseurAfficher.Text = CmbRechFournisseurAfficher.Items[0].ToString();

        }

        private void TxtRchDgrid_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["Client"]);
            dataView.RowFilter = "[" + CmbRechClientAfficher.SelectedItem.ToString() + "]like '%" + TxtRchDgrid.Text + "%'";

            //if (CmbRechClientAfficher.SelectedIndex == 0)
            //{
            //    dataView.RowFilter = "NomClient like '%" + TxtRchDgrid.Text + "%'";
            //}
            //if (CmbRechClientAfficher.SelectedIndex == 1)
            //{
            //    dataView.RowFilter = "[Num Tel] like '%" + TxtRchDgrid.Text + "%'";
            //}
            //if (CmbRechClientAfficher.SelectedIndex == 2)
            //{
            //    dataView.RowFilter = "Adresse like '%" + TxtRchDgrid.Text + "%'";
            //}
            //if (CmbRechClientAfficher.SelectedIndex == 3)
            //{
            //    dataView.RowFilter = "Mail like '%" + TxtRchDgrid.Text + "%'";
            //}
            //if (CmbRechClientAfficher.SelectedIndex == 5)
            //{
            //    dataView.RowFilter = "[RC / CIN] like '%" + TxtRchDgrid.Text + "%'";
            //}
            //if (CmbRechClientAfficher.SelectedIndex == 4)
            //{
            //    dataView.RowFilter = "Type like '%" + TxtRchDgrid.Text + "%'";
            //}

            dataGridClient.DataSource = dataView;
        }

        private void txtRechAffichForni_TextChanged_1(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["Fournisseur"]);
            dataView.RowFilter = "[Nom Fourni] like '%" + txtRechAffichForni.Text + "%'";
            dataGridFourni.DataSource = dataView;
        }

        private void gunaTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["Emballage"]);
            dataView.RowFilter = "[Code Emballage] like '%" + gunaTextBox1.Text + "%'";
            dataGridEmballage.DataSource = dataView;
        }

        private void gunaTextBox3_TextChanged_1(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["MatiereP"]);
            dataView.RowFilter = "[Code] like '%" + gunaTextBox3.Text + "%'";
            dataGridMatiereP.DataSource = dataView;
        }

        private void gunaTextBox4_TextChanged_1(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["HistoriqueA"]);
            dataView.RowFilter = "[Nom Produit] like '%" + gunaTextBox4.Text + "%'";
            dataGrideHistoriqueA.DataSource = dataView;
        }

        private void gunaTextBox5_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["HistoriqueV"]);
            dataView.RowFilter = "[Nom Produit] like '%" + gunaTextBox5.Text + "%'";
            dataGrideHistoriqueV.DataSource = dataView;
        }

        private void txtRechAffichForni_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["Fournisseur"]);
            dataView.RowFilter = "[" + CmbRechFournisseurAfficher.SelectedItem.ToString() + "]like '%" + txtRechAffichForni.Text + "%'";
            dataGridFourni.DataSource = dataView;
        }
    }
}
