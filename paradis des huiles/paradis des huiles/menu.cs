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
        SqlConnection cn = new SqlConnection("Server='.';Database=DB_Gestion;Integrated Security = true");
        public menu()
        {
            InitializeComponent();
        }

        DataSet DataSet1 = new DataSet();
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
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
            dataAdapter = new SqlDataAdapter("select nomClt as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
            dataAdapter.Fill(DataSet1, "Client");

            dataAdapter = new SqlDataAdapter("select nomFournisseur [Nom Fourni] ,numTel [Num Tel] , adresse [Adresse] , email [Mail] , RCF [RC] from Fournisseur", cn);
            dataAdapter.Fill(DataSet1, "Fournisseur");

            dataAdapter = new SqlDataAdapter("select  'AG'+convert(varchar(50),idEM) [Code Emballage],nomFournisseur [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
            dataAdapter.Fill(DataSet1, "Emballage");

            dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
            dataAdapter.Fill(DataSet1, "MatiereP");

            dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueA");

            dataAdapter = new SqlDataAdapter("Select * from Historique_Achat", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueAchatPure");

            dataAdapter = new SqlDataAdapter("select numVente [Num Vente], Produit_finis.idPF + codePF [Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueV");

            dataAdapter = new SqlDataAdapter("Select * from Etat", cn);
            dataAdapter.Fill(DataSet1, "Etat");

            /*dataAdapter = new SqlDataAdapter("select * from Produit_finis",cn);
            dataAdapter.Fill(DataSet1, "Produit_finis");*/

            dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], RCF [RCF] , idE [idE] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée], idMP from Matiere_premiere ", cn);
            dataAdapter.Fill(DataSet1, "MPPure");

            dataAdapter = new SqlDataAdapter("Select 'AG'+convert(varchar(50),idEM) [CODEEM],idEM from Emballage ", cn);
            dataAdapter.Fill(DataSet1, "EMCMBOX");
            //dataAdapter = new SqlDataAdapter("select [type].nomType + codePF [Code Produit] ", cn);
            //dataAdapter.Fill(DataSet1, "Produit_finis");


            cn.Close();

            dataGridClient.DataSource = DataSet1.Tables["Client"];
            dataGridFourni.DataSource = DataSet1.Tables["Fournisseur"];
            dataGridEmballage.DataSource = DataSet1.Tables["Emballage"];
            dataGridMatiereP.DataSource = DataSet1.Tables["MatiereP"];
            dataGrideHistoriqueV.DataSource = DataSet1.Tables["HistoriqueV"];
            dataGrideHistoriqueA.DataSource = DataSet1.Tables["HistoriqueA"];
            datagridAffProduitF.DataSource = DataSet1.Tables["Produit_finis"];

            rbtnAddHistoAMatierePrem.Checked = true;

            //CmbRechFournisseurAfficher afficher rechercher fournisseur
            for (int i = 0; i < DataSet1.Tables["Fournisseur"].Columns.Count; i++)
            {
                CmbRechFournisseurAfficher.Items.Add(DataSet1.Tables["Fournisseur"].Columns[i].ColumnName.ToString());
            }
            CmbRechFournisseurAfficher.Text = CmbRechFournisseurAfficher.Items[0].ToString();

            for (int i = 0; i < DataSet1.Tables["Emballage"].Columns.Count; i++)
            {
                CmbAfficheEmballageRech.Items.Add(DataSet1.Tables["Emballage"].Columns[i].ColumnName.ToString());
            }
            CmbAfficheEmballageRech.Text = CmbAfficheEmballageRech.Items[0].ToString();

            for (int i = 0; i < DataSet1.Tables["MatiereP"].Columns.Count; i++)
            {
                CmbAfficherMatierpfind.Items.Add(DataSet1.Tables["MatiereP"].Columns[i].ColumnName.ToString());
            }
            CmbAfficherMatierpfind.Text = CmbAfficherMatierpfind.Items[0].ToString();

            for (int i = 0; i < DataSet1.Tables["HistoriqueA"].Columns.Count; i++)
            {
                CmbAfficherHistoriqueAfind.Items.Add(DataSet1.Tables["HistoriqueA"].Columns[i].ColumnName.ToString());
            }
            CmbAfficherHistoriqueAfind.Text = CmbAfficherHistoriqueAfind.Items[0].ToString();

            for (int i = 0; i < DataSet1.Tables["HistoriqueV"].Columns.Count; i++)
            {
                CmbAfficherHistVfind.Items.Add(DataSet1.Tables["HistoriqueV"].Columns[i].ColumnName.ToString());
            }
            CmbAfficherHistVfind.Text = CmbAfficherHistVfind.Items[0].ToString();
            /*for (int i = 0; i < DataSet1.Tables["HistoriqueV"].Columns.Count; i++)
            {
                CmbAfficherHistVfind.Items.Add(DataSet1.Tables["HistoriqueV"].Columns[i].ColumnName.ToString());
            }
            CmbAfficherHistVfind.Text = CmbAfficherHistVfind.Items[0].ToString();*/


            //cmbboxs
            this.cmbAddNomFourniMatierP.DisplayMember = "Nom Fourni";
            this.cmbAddNomFourniMatierP.ValueMember = "RC";
            this.cmbAddNomFourniMatierP.DataSource = DataSet1.Tables["Fournisseur"];
            this.cmbAddNomFournisseurEmballage.DisplayMember = "Nom Fourni";
            this.cmbAddNomFournisseurEmballage.ValueMember = "RC";
            this.cmbAddNomFournisseurEmballage.DataSource = DataSet1.Tables["Fournisseur"];
            this.cmbAddTypeEmballge.DisplayMember = "UniteE";
            this.cmbAddTypeEmballge.ValueMember = "idE";
            this.cmbAddTypeEmballge.DataSource = DataSet1.Tables["Etat"];
            this.cmbAddTypeMatierP.DisplayMember = "UniteE";
            this.cmbAddTypeMatierP.ValueMember = "idE";
            this.cmbAddTypeMatierP.DataSource = DataSet1.Tables["Etat"];
            this.cmbAddHabituelHistorA.DisplayMember = "Nom Fourni";
            this.cmbAddHabituelHistorA.ValueMember = "RC";
            this.cmbAddHabituelHistorA.DataSource = DataSet1.Tables["Fournisseur"];
            /*this.cmbnomproduitaddhistoriquedachat.DisplayMember = "nomProduit";
            this.cmbnomproduitaddhistoriquedachat.ValueMember = "idPF";
            this.cmbnomproduitaddhistoriquedachat.DataSource = DataSet1.Tables["Produit_finis"];*/
            this.gunaComboBox3.DisplayMember = "CODEEM";
            this.gunaComboBox3.ValueMember = "idEM";
            this.gunaComboBox3.DataSource = DataSet1.Tables["EMCMBOX"];
            //labbels
            this.Labelcount.Text = "Nombre de clients : " + dataGridClient.Rows.Count;
            this.label8.Text = "Nombre de Fournisseurs : " + dataGridFourni.Rows.Count;
        }

        private void TxtRchDgrid_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["Client"]);
            dataView.RowFilter = "[" + CmbRechClientAfficher.SelectedItem.ToString() + "]like '%" + TxtRchDgrid.Text + "%'";

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
            dataView.RowFilter = "[Code Emballage] like '%" + TxtEmbafficherfind.Text + "%'";
            dataGridEmballage.DataSource = dataView;
        }

        private void gunaTextBox3_TextChanged_1(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["MatiereP"]);
            dataView.RowFilter = "[Code] like '%" + TxtMatierpfind.Text + "%'";
            dataGridMatiereP.DataSource = dataView;
        }



        private void txtRechAffichForni_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["Fournisseur"]);
            dataView.RowFilter = "[" + CmbRechFournisseurAfficher.SelectedItem.ToString() + "]like '%" + txtRechAffichForni.Text + "%'";
            dataGridFourni.DataSource = dataView;
        }

        private void TxtEmbafficherfind_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["Emballage"]);
            dataView.RowFilter = "[" + CmbAfficheEmballageRech.SelectedItem.ToString() + "]like '%" + TxtEmbafficherfind.Text + "%'";
            dataGridEmballage.DataSource = dataView;
        }

        private void TxtMatierpfind_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["MatierP"]);
            dataView.RowFilter = "[" + CmbAfficherMatierpfind.SelectedItem.ToString() + "]like '%" + TxtMatierpfind.Text + "%'";
            dataGridEmballage.DataSource = dataView;
        }

        private void TxtAfficherHistAfind_TextChanged(object sender, EventArgs e)
        {
            
            DataView dataView = new DataView(DataSet1.Tables["HistoriqueA"]);
            dataView.RowFilter = "[" + CmbAfficherHistoriqueAfind.SelectedItem.ToString() + "]like '%" + TxtAfficherHistAfind.Text + "%'";
            dataGridEmballage.DataSource = dataView;
        }

        private void TxtAffHistVfind_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show(CmbAfficherHistVfind.SelectedItem.ToString());
            DataView dataView = new DataView(DataSet1.Tables["HistoriqueV"]);
            dataView.RowFilter = "[" + CmbAfficherHistVfind.SelectedItem.ToString() + "]like '%" + TxtAffHistVfind.Text.ToString() + "%'";
            dataGrideHistoriqueV.DataSource = dataView;
        }

        int fdl; //fidele val
        private void BtnAddClt_Click(object sender, EventArgs e)
        {
            try
            {
                //Ajouter mode Déco using dataset
                DataSet1.Tables["Client"].Rows.Add(TxtNomaddclt.Text, txtaddnumclt.Text, txtaddadressclt.Text, txtaddmailclt.Text, cmbtypecltadd.SelectedItem.ToString(), txtaddrcclt.Text);
                dataAdapter = new SqlDataAdapter("select nomClt as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["Client"]);
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TxtModifClientfind_TextChanged(object sender, EventArgs e)
        {

        }
        private void DatagridModifClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void BtnModifierclt_Click(object sender, EventArgs e)
        {
        }

        private void btnAddFourni_Click(object sender, EventArgs e)
        {
            DataSet1.Tables["Fournisseur"].Rows.Add(txtNomAddFourni.Text, txtTelAddFourni.Text, txtAdresseAddFourni.Text, txtMailAddFourni.Text, txtRcAddFourni.Text);
            dataAdapter = new SqlDataAdapter("select nomFournisseur [Nom Fourni] ,numTel [Num Tel] , adresse [Adresse] , email [Mail] , RCF [RC] from Fournisseur", cn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            cn.Open();
            dataAdapter.Update(DataSet1.Tables["Fournisseur"]);
            cn.Close();
        }

        private void btnAddImageEmballage_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBoxEM.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxEM.Image = new Bitmap(open.FileName);
                FilePathEM.Text = open.FileName;
            }
        }

        private void btnAddEmballage_Click(object sender, EventArgs e)
        {
            dataAdapter = new SqlDataAdapter("select RCF [RCsF] , qteEM [Quantite] , idE [idE] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage ", cn);
            cn.Open();
            dataAdapter.Fill(DataSet1, "EmballagePure");
            cn.Close();
            MessageBox.Show(DataSet1.Tables["EmballagePure"].Rows.Count.ToString());
            int x = DataSet1.Tables["EmballagePure"].Rows.Count - 1;
            DataSet1.Tables["EmballagePure"].Rows.Add(cmbAddNomFournisseurEmballage.SelectedValue.ToString(), txtAddquantiteEmballage.Text, cmbAddTypeEmballge.SelectedValue.ToString(), txtAddSupportEmballage.Text, txtAddDescriptionEmballage.Text, cmbAddEtageEmballage.SelectedItem.ToString() + cmbAddSalleEmballage.SelectedItem.ToString());
            SqlCommand cmd = new SqlCommand("update emballage set img=(SELECT BulkColumn FROM Openrowset( Bulk '" + this.FilePathEM.Text + "', Single_Blob) as img) where idEM='" + x.ToString() + "'", cn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);

            cn.Open();
            dataAdapter.Update(DataSet1.Tables["EmballagePure"]);
            cmd.ExecuteNonQuery();
            dataAdapter = new SqlDataAdapter("select 'AG'+convert(varchar(50),idEM) [Code Emballage], nomFournisseur [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet1.Tables["Emballage"].Clear();
            dataAdapter.Fill(DataSet1, "Emballage");
            cn.Close();
            MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void batnAddMatiereP_Click(object sender, EventArgs e)
        {
            try
            {
                //Etage Salle non ajouter /!\
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], RCF [RCF] , idE [idE] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée], idMP from Matiere_premiere ", cn);
                cn.Open();
                dataAdapter.Fill(DataSet1, "MPPure");
                cn.Close();
                DataSet1.Tables["MPPure"].Rows.Add(txtaddcodemp.Text, txtAddNomMatiereP.Text, cmbAddNomFourniMatierP.SelectedValue.ToString(), cmbAddTypeMatierP.SelectedValue.ToString(), txtAddQuantiteMatiereP.Text, txtAddDescMatierP.Text, "Test");
                DataSet1.Tables["MatiereP"].Rows.Add(txtaddcodemp.Text, txtAddNomMatiereP.Text, cmbAddNomFourniMatierP.SelectedText.ToString());
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["MPPure"]);
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
                DataSet1.Tables["MatiereP"].Clear();
                dataAdapter.Fill(DataSet1, "MatiereP");
                cn.Close();
                MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        private void tabControl5_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnaddhistoriquedachat_Click(object sender, EventArgs e)
        {
            try
            {
                dataAdapter = new SqlDataAdapter("select RCF,qteA,prix,dateA,idEM,idMP from historique_achat", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                cn.Open();
                dataAdapter.Fill(DataSet1, "HistoriqueAP");
                cn.Close();
                if (rbtnAddHistoAMatierePrem.Checked)
                    DataSet1.Tables["HistoriqueAP"].Rows.Add(cmbAddHabituelHistorA.SelectedValue.ToString(), txtquantiteaddhistoriquedachat.Text, prixaddhistoriquedachat.Text, dateventeaddhistoriquedachat.Value.ToString(), cmbnomproduitaddhistoriquedachat.SelectedValue.ToString());
                else
                    DataSet1.Tables["HistoriqueAP"].Rows.Add(cmbAddHabituelHistorA.SelectedValue.ToString(), txtquantiteaddhistoriquedachat.Text, prixaddhistoriquedachat.Text, dateventeaddhistoriquedachat.Value.ToString(), null, cmbnomproduitaddhistoriquedachat.SelectedValue.ToString().Substring(2));
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["HistoriqueAP"]);

                cn.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void gunaGradientButton6_Click(object sender, EventArgs e)
        {
            dataAdapter = new SqlDataAdapter("select nomClt as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            cn.Open();
            dataAdapter.Update(DataSet1, "Client");

            cn.Close();
        }

        private void ClickDroitMouseSupprimer_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-Vous supprimer le client '" + dataGridClient.SelectedRows[0].Cells[0].Value.ToString() + "' de la base de donnée", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGridClient.Rows.RemoveAt(dataGridClient.SelectedRows[0].Index);
                MessageBox.Show("Suppression effectuer! Veuillez Sauvgardez les modifications.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridClient_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowindex = dataGridClient.HitTest(e.X, e.Y).RowIndex;
                if (rowindex >= 0)
                {
                    ClickDroitMouseClt.Show(dataGridClient, new Point(e.X, e.Y));
                    dataGridClient.ClearSelection();
                    dataGridClient.Rows[rowindex].Selected = true;
                }
            }
        }

        private void dataGridClient_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            this.Labelcount.Text = "Nombre de clients : " + dataGridClient.Rows.Count;
        }

        private void ClickDroirMouseVA_Click(object sender, EventArgs e)
        {
            CmbAfficherHistVfind.SelectedIndex = CmbAfficherHistVfind.FindString("Nom Client");
            TxtAffHistVfind.Text = dataGridClient.SelectedRows[0].Cells[0].Value.ToString();
            btnSaveProdF.SelectedIndex = 5;
        }

        private void ClickDroitMouseModifier_Click(object sender, EventArgs e)
        {
            if (ClickDroitMouseModifier.Checked)
                dataGridClient.ReadOnly = false;
            else
                dataGridClient.ReadOnly = true;
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-Vous supprimer le Frounisseur '" + dataGridFourni.SelectedRows[0].Cells[0].Value.ToString() + "' de la base de donnée", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGridFourni.Rows.RemoveAt(dataGridFourni.SelectedRows[0].Index);
                MessageBox.Show("Suppression effectuer! Veuillez Sauvgardez les modifications.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridFourni_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridFourni_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowindex = dataGridFourni.HitTest(e.X, e.Y).RowIndex;
                if (rowindex >= 0)
                {
                    ClickDroitMouseFourni.Show(dataGridFourni, e.X, e.Y);
                    dataGridFourni.ClearSelection();
                    dataGridFourni.Rows[rowindex].Selected = true;
                }
            }
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (modifierToolStripMenuItem.Checked)
                dataGridFourni.ReadOnly = false;
            else
                dataGridFourni.ReadOnly = true;
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            dataAdapter = new SqlDataAdapter("select nomFournisseur [Nom Fourni] ,numTel [Num Tel] , adresse [Adresse] , email [Mail] , RCF [RC] from Fournisseur", cn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            cn.Open();
            dataAdapter.Update(DataSet1, "Fournisseur");
            cn.Close();
        }

        private void dataGridFourni_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            this.label8.Text = "Nombre de Fournisseurs : " + dataGridFourni.Rows.Count;
        }

        private void rbtnAddHistoAMatierePrem_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnAddHistoAMatierePrem.Checked)
            {
                this.cmbnomproduitaddhistoriquedachat.DataSource = DataSet1.Tables["MPPure"];
                cmbnomproduitaddhistoriquedachat.DisplayMember = "nom";
                cmbnomproduitaddhistoriquedachat.ValueMember = "idMP";
            }
            else
            {
                this.cmbnomproduitaddhistoriquedachat.DataSource = DataSet1.Tables["Emballage"];
                this.cmbnomproduitaddhistoriquedachat.DisplayMember = "Code Emballage";
                this.cmbnomproduitaddhistoriquedachat.ValueMember = "Code Emballage";
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void cmbAddTypeEmballge_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            dataAdapter = new SqlDataAdapter("Select * from Produit_finis", cn);
            cn.Open();
            dataAdapter.Fill(DataSet1, "ProduitsFPure");
            cn.Close();
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet1.Tables["ProduitsFPure"].Rows.Add(string.Join(null, System.Text.RegularExpressions.Regex.Split(gunaTextBox4.Text, "[^\\d]")), this.gunaTextBox5.Text.ToString(), this.gunaComboBox3.SelectedValue.ToString(), "E0S0", richTextBox1.Text.ToString(), this.gunaTextBox2.Text.ToString(), string.Join(null, System.Text.RegularExpressions.Regex.Split(gunaTextBox4.Text, "[^a-zA-Z]")));
            cn.Open();
            dataAdapter.Update(DataSet1, "ProduitsFPure");
            cn.Close();
        }

        private void btnaddhistoriqueV_Click(object sender, EventArgs e)
        {

        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridMatiereP_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowindex = dataGridMatiereP.HitTest(e.X, e.Y).RowIndex;
                if (rowindex >= 0)
                {
                    ClickDroitMouseMatiereP.Show(dataGridMatiereP, e.X, e.Y);
                    dataGridMatiereP.ClearSelection();
                    dataGridMatiereP.Rows[rowindex].Selected = true;
                }
            }
        }

        private void supprimerToolStripMenuItemMP_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-Vous supprimer cette Matiere Premiere '" + dataGridMatiereP.SelectedRows[0].Cells[1].Value.ToString() + "' de la base de donnée", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGridMatiereP.Rows.RemoveAt(dataGridMatiereP.SelectedRows[0].Index);
                MessageBox.Show("Suppression effectuer! Veuillez Sauvgardez les modifications.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void modifierToolStripMenuItemMP_Click(object sender, EventArgs e)
        {
            if (modifierToolStripMenuItemMP.Checked)
                dataGridMatiereP.ReadOnly = false;
            else
                dataGridMatiereP.ReadOnly = true;
        }

        private void dataGridEmballage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowindex = dataGridEmballage.HitTest(e.X, e.Y).RowIndex;
                if (rowindex >= 0)
                {
                    ClickDroitMouseEmballage.Show(dataGridEmballage, e.X, e.Y);
                    dataGridEmballage.ClearSelection();
                    dataGridEmballage.Rows[rowindex].Selected = true;
                }
            }
        }

        private void supprimerToolStripMenuItemEmba_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-Vous supprimer l'emballage '" + dataGridEmballage.SelectedRows[0].Cells[0].Value.ToString() + "' de la base de donnée", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGridEmballage.Rows.RemoveAt(dataGridEmballage.SelectedRows[0].Index);
                MessageBox.Show("Suppression effectuer! Veuillez Sauvgardez les modifications.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void modifierToolStripMenuItemEmba_Click(object sender, EventArgs e)
        {
            if (modifierToolStripMenuItemEmba.Checked)
                dataGridEmballage.ReadOnly = false;
            else
                dataGridEmballage.ReadOnly = true;
        }

        private void dataGrideHistoriqueA_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowindex = dataGrideHistoriqueA.HitTest(e.X, e.Y).RowIndex;
                if (rowindex >= 0)
                {
                    ClickDroitMouseHistoA.Show(dataGrideHistoriqueA, e.X, e.Y);
                    dataGrideHistoriqueA.ClearSelection();
                    dataGrideHistoriqueA.Rows[rowindex].Selected = true;
                }
            }
        }

        private void supprimerToolStripMenuItemHistoA_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-Vous supprimer l'istorique '" + dataGrideHistoriqueA.SelectedRows[0].Cells[1].Value.ToString() + "' de la base de donnée", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGrideHistoriqueA.Rows.RemoveAt(dataGrideHistoriqueA.SelectedRows[0].Index);
                MessageBox.Show("Suppression effectuer! Veuillez Sauvgardez les modifications.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void modifierToolStripMenuItemHistoA_Click(object sender, EventArgs e)
        {
            if (modifierToolStripMenuItemHistoA.Checked)
                dataGrideHistoriqueA.ReadOnly = false;
            else
                dataGrideHistoriqueA.ReadOnly = true;
        }

        private void dataGrideHistoriqueV_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowindex = dataGrideHistoriqueV.HitTest(e.X, e.Y).RowIndex;
                if (rowindex >= 0)
                {
                    ClickDroitMouseHistoV.Show(dataGrideHistoriqueV, e.X, e.Y);
                    dataGrideHistoriqueV.ClearSelection();
                    dataGrideHistoriqueV.Rows[rowindex].Selected = true;
                }
            }
        }

        private void supprimerToolStripMenuItemHistoV_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-Vous supprimer l'istorique '" + dataGrideHistoriqueV.SelectedRows[0].Cells[1].Value.ToString() + "' de la base de donnée", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGrideHistoriqueV.Rows.RemoveAt(dataGrideHistoriqueV.SelectedRows[0].Index);
                MessageBox.Show("Suppression effectuer! Veuillez Sauvgardez les modifications.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void modifierToolStripMenuItemHistoV_Click(object sender, EventArgs e)
        {
            if (modifierToolStripMenuItemHistoV.Checked)
                dataGrideHistoriqueV.ReadOnly = false;
            else
                dataGrideHistoriqueV.ReadOnly = true;
        }

        private void datagridAffProduitF_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowindex = datagridAffProduitF.HitTest(e.X, e.Y).RowIndex;
                if (rowindex >= 0)
                {
                    ClickDroitMouseProdF.Show(datagridAffProduitF, e.X, e.Y);
                    datagridAffProduitF.ClearSelection();
                    datagridAffProduitF.Rows[rowindex].Selected = true;
                }
            }
        }

        private void supprimerToolStripMenuItemProdF_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-Vous supprimer le produit '" + datagridAffProduitF.SelectedRows[0].Cells[0].Value.ToString() + "' de la base de donnée", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                datagridAffProduitF.Rows.RemoveAt(datagridAffProduitF.SelectedRows[0].Index);
                MessageBox.Show("Suppression effectuer! Veuillez Sauvgardez les modifications.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void modifierToolStripMenuItemProdF_Click(object sender, EventArgs e)
        {
            if (modifierToolStripMenuItemProdF.Checked)
                datagridAffProduitF.ReadOnly = false;
            else
                datagridAffProduitF.ReadOnly = true;
        }

        private void ClickDroirMouseFiindAchat_Click(object sender, EventArgs e)
        {
            TxtAfficherHistAfind.Text = dataGridFourni.SelectedRows[0].Cells[0].Value.ToString();
            btnSaveProdF.SelectedIndex = 4;
        }
        bool b,b2;
        private void btnSaveEmballage_Click(object sender, EventArgs e)
        {
            cn.Open();

            cmd = new SqlCommand("select RCF ,idE from Fournisseur,Etat where nomFournisseur = @a and UniteE = @b'", cn);

            for (int i = 0; i < dataGridEmballage.Rows.Count; i++)
            {
                cmd = new SqlCommand("update Emballage set RCF = (select RCF from Fournisseur where nomFournisseur = @b),qteEM = @c, idE = (select idE from Etat where UniteE = @d) , supportEM = @e,descEM = @f,cordoEM = @j where idEM = @a", cn);
                cmd.Parameters.AddWithValue("@a", dataGridEmballage.Rows[i].Cells[0].Value.ToString().Remove(0, 2));
                cmd.Parameters.AddWithValue("@b", dataGridEmballage.Rows[i].Cells[1].Value);
                cmd.Parameters.AddWithValue("@c", dataGridEmballage.Rows[i].Cells[2].Value);
                cmd.Parameters.AddWithValue("@d", dataGridEmballage.Rows[i].Cells[3].Value.ToString());
                cmd.Parameters.AddWithValue("@e", dataGridEmballage.Rows[i].Cells[4].Value);
                cmd.Parameters.AddWithValue("@f", dataGridEmballage.Rows[i].Cells[5].Value);
                cmd.Parameters.AddWithValue("@j", dataGridEmballage.Rows[i].Cells[6].Value);


                b = false;
                for (int j = 0; j < DataSet1.Tables["Fournisseur"].Rows.Count; j++)
                {

                    if (dataGridEmballage.Rows[i].Cells[1].Value.ToString() == DataSet1.Tables["Fournisseur"].Rows[j][0].ToString() && (dataGridEmballage.Rows[i].Cells[3].Value.ToString().ToLower() == "kg" || dataGridEmballage.Rows[i].Cells[3].Value.ToString().ToLower() == "l"))
                    {
                        b = true;
                        break;

                    }
                }
                if (b)
                    cmd.ExecuteNonQuery();
                else
                    MessageBox.Show("la valeur " + dataGridEmballage.Rows[i].Cells[1].Value.ToString() + " ou " + dataGridEmballage.Rows[i].Cells[3].Value.ToString() + " de la lign " + (i + 1).ToString() + " est invalide");

            }
            DataSet1.Tables["Emballage"].Clear();
            dataAdapter = new SqlDataAdapter("select ISNULL('AG'+convert(varchar(50),idEM) ,'')[Code Emballage],ISNULL(nomFournisseur,'notfound') [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
            dataAdapter.Fill(DataSet1, "Emballage");
            cn.Close();


        }
        SqlCommand cmd = new SqlCommand();

        private void btnSaveMatiereP_Click(object sender, EventArgs e)
        {
            cn.Open();
            for (int i = 0; i < dataGridMatiereP.Rows.Count; i++)
            {
                cmd = new SqlCommand("update Matiere_premiere set nomMP = @b , RCF = (select RCF from Fournisseur where nomFournisseur = @c), idE = (select idE from Etat where UniteE = @d ) ,qteMP = @e,descMP = @f, cordoMP = @j where codeMP = @a ", cn);
                cmd.Parameters.AddWithValue("@a", dataGridMatiereP.Rows[i].Cells[0].Value.ToString());
                cmd.Parameters.AddWithValue("@b", dataGridMatiereP.Rows[i].Cells[1].Value);
                cmd.Parameters.AddWithValue("@c", dataGridMatiereP.Rows[i].Cells[2].Value);
                cmd.Parameters.AddWithValue("@d", dataGridMatiereP.Rows[i].Cells[3].Value.ToString());
                cmd.Parameters.AddWithValue("@e", dataGridMatiereP.Rows[i].Cells[4].Value);
                cmd.Parameters.AddWithValue("@f", dataGridMatiereP.Rows[i].Cells[5].Value);
                cmd.Parameters.AddWithValue("@j", dataGridMatiereP.Rows[i].Cells[6].Value);
                b = false;
                for (int j = 0; j < DataSet1.Tables["Fournisseur"].Rows.Count; j++)
                {

                    if (dataGridMatiereP.Rows[i].Cells[2].Value.ToString() == DataSet1.Tables["Fournisseur"].Rows[j][0].ToString() && (dataGridMatiereP.Rows[i].Cells[3].Value.ToString().ToLower() == "kg" || dataGridMatiereP.Rows[i].Cells[3].Value.ToString().ToLower() == "l"))
                    {
                        b = true;
                        break;

                    }
                }
                if (b)
                    cmd.ExecuteNonQuery();
                else
                    MessageBox.Show("la valeur " + dataGridMatiereP.Rows[i].Cells[2].Value.ToString() + " ou " + dataGridMatiereP.Rows[i].Cells[3].Value.ToString() + " de la lign " + (i + 1).ToString() + "  est invalide");



            }
            DataSet1.Tables["MatiereP"].Clear();
            dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
            dataAdapter.Fill(DataSet1, "MatiereP");
            cn.Close();
        }

        private void plusDinformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Emballage_Viewer EM = new Emballage_Viewer();
            TextBox textBox = new TextBox();
            textBox = (TextBox)EM.Controls["groupBox2"].Controls["textBox1"];
            textBox.Text = dataGridEmballage.SelectedRows[0].Cells[0].Value.ToString();
            EM.Show();
        }

        private void gunaGradientButton3_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("PrintC.xml");
            Form2 frm = new Form2();
            frm.crstl(DataSet1.Tables["Client"]);
            frm.Show();
        }

        private void gunaGradientButton9_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("PrintC.xml");
            Form2 frm = new Form2();
            frm.crstl(DataSet1.Tables["Fournisseur"]);
            frm.Show();
        }

        private void gunaGradientButton13_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("PrintC.xml");
            Form2 frm = new Form2();
            frm.crstl(DataSet1.Tables["Emballage"]);
            frm.Show();
        }

        private void gunaGradientButton17_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveHistoA_Click(object sender, EventArgs e)
        {
            cn.Open();
            for (int i = 0; i < dataGrideHistoriqueA.Rows.Count; i++)
            {
                b = false;
                b2 = false;

                if (dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().Contains("AG"))
                {
                    cmd = new SqlCommand("update historique_achat set idMP = NULL, idEM = @b , RCF = (select RCF from Fournisseur where nomFournisseur = @c),qteA = @d ,prix = @e , dateA = @f where numAchat = @a", cn);
                    cmd.Parameters.AddWithValue("@b", dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().Remove(0,2));
                    for (int k = 0; k < DataSet1.Tables["Emballage"].Rows.Count; k++)
                    {
                        if (dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().Remove(0, 2) == DataSet1.Tables["Emballage"].Rows[k][0].ToString().Remove(0, 2))
                        {
                            b2 = true;
                            break;
                        }
                    }
                }
                else
                {
                    cmd = new SqlCommand("update historique_achat set idEM = NULL, idMP = (select idmp from Matiere_premiere where nomMP = @b) , RCF = (select RCF from Fournisseur where nomFournisseur = @c),qteA = @d ,prix = @e , dateA = @f where numAchat = @a", cn);
                    cmd.Parameters.AddWithValue("@b", dataGrideHistoriqueA.Rows[i].Cells[1].Value);
                    for (int k = 0; k < DataSet1.Tables["MPPure"].Rows.Count; k++)
                    {
                        if (dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().ToLower() == DataSet1.Tables["MPPure"].Rows[k][1].ToString().ToLower())
                        {
                            b2 = true;
                            break;
                        }
                    }
                }
                cmd.Parameters.AddWithValue("@a", dataGrideHistoriqueA.Rows[i].Cells[0].Value.ToString());              
                cmd.Parameters.AddWithValue("@c", dataGrideHistoriqueA.Rows[i].Cells[2].Value);
                cmd.Parameters.AddWithValue("@d", dataGrideHistoriqueA.Rows[i].Cells[3].Value.ToString());
                cmd.Parameters.AddWithValue("@e", dataGrideHistoriqueA.Rows[i].Cells[4].Value);
                cmd.Parameters.AddWithValue("@f", dataGrideHistoriqueA.Rows[i].Cells[5].Value);
                
                for (int j = 0; j < DataSet1.Tables["Fournisseur"].Rows.Count; j++)
                {

                    if (dataGrideHistoriqueA.Rows[i].Cells[2].Value.ToString() == DataSet1.Tables["Fournisseur"].Rows[j][0].ToString())
                    {
                        b = true;
                        break;
                    }
                }
                if (b && b2)
                    cmd.ExecuteNonQuery();
                else
                    MessageBox.Show("la valeur " + dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString() + " ou " + dataGrideHistoriqueA.Rows[i].Cells[2].Value.ToString() + " d'achat numero : " + dataGrideHistoriqueA.Rows[i].Cells[0].Value.ToString() + " est invalide");
            }
            DataSet1.Tables["HistoriqueA"].Clear();
            dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueA");
            cn.Close();
        }
    }
}
