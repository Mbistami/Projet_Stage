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
            cn.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select nomClt + ' ' + ISNULL( prenomClt, '') as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
            dataAdapter.Fill(DataSet1, "Client");

            dataAdapter = new SqlDataAdapter("select nomFournisseur [Nom Fourni] ,numTel [Num Tel] , adresse [Adresse] , email [Mail], typeF [Type] , RCF [RC] from Fournisseur", cn);
            dataAdapter.Fill(DataSet1, "Fournisseur");

            dataAdapter = new SqlDataAdapter("select idEm  + codeEM [Code Emballage], nomFournisseur [Nom Fournisseur] , qteEM [Quantite] , NomE [Type] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
            dataAdapter.Fill(DataSet1, "Emballage");

            dataAdapter = new SqlDataAdapter("select idMP  + codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , NomE [Type] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
            dataAdapter.Fill(DataSet1, "MatiereP");

            dataAdapter = new SqlDataAdapter("select numAchat [Num Achat],NumFac [Num FActure] , nomMP [Nom Produit],nomFournisseur [Fournisseur], qteA[Quantite],prix [Prix],dateA [Date Achat] from Historique_Achat inner join Fournisseur on Fournisseur.RCF = Historique_Achat.RCF inner join Matiere_premiere on Fournisseur.RCF = Matiere_premiere.RCF", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueA");

            dataAdapter = new SqlDataAdapter("select numVente [Num Vente],NumFac [Num Facture], Produit_finis.idPF + codePF [Nom Produit], nomClt + ' ' + ISNULL(prenomClt,'') [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
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
        }

        private void TxtRchDgrid_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["Client"]);
            dataView.RowFilter = "NomClient like '%" + TxtRchDgrid.Text + "%'";
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
    }
}
