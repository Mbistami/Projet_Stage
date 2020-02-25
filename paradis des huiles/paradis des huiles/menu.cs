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
        /*<<<<<<< HEAD
                SqlConnection cn = new SqlConnection("Server='R_230_ROG-PC\\SQLEXPRESS';Database=DB_Gestionn;Integrated Security = true");
        =======*/
        //SqlConnection cn = new SqlConnection("Server='.';Database=DB_Gestionn;Integrated Security = true");
         SqlConnection cn = new SqlConnection("Server='192.168.1.30, 1433'; Database= DB_Gestion ;user id='admin';password='admin'");
        //>>>>>>> 8906389a96d07d69ed7732bc3cfd94170d581025
        public menu()
        {
            InitializeComponent();
        }

        DataSet DataSet1 = new DataSet();
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        public static int permleve;
        private void menu_Load(object sender, EventArgs e)
        {

            try
            {

            if (permleve != 4)
                btnSaveProdF.TabPages.RemoveAt(btnSaveProdF.TabPages.Count - 1);
            String[] etage = { "E0", "E1", "E2" };
            String[] salle = { "S1", "S2", "S3" };

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


            if (ConnectionState.Open == cn.State)
            {
                cn.Close();
            }
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

            dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueV");

            dataAdapter = new SqlDataAdapter("select nomPF + ' ' +CONVERT(varchar(50),codePF) [Code Prodiuit],'AG' +CONVERT(varchar(50),Emballage.idEM )[Emballage],qteEM [Quantite],Etat.NomE [Etat],cordoEM [Cordonnees],descPF [Descrption], idPF from Produit_finis inner join Emballage on Emballage.idEM = Produit_finis.idEM inner join Etat on Produit_finis.idE = Etat.idE", cn);
            dataAdapter.Fill(DataSet1, "Produit_finis");

            dataAdapter = new SqlDataAdapter("Select * from Historique_Achat", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueAchatPure");

            dataAdapter = new SqlDataAdapter("Select * from Etat", cn);
            dataAdapter.Fill(DataSet1, "Etat");

            dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], RCF [RCF] , idE [idE] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée], idMP from Matiere_premiere ", cn);
            dataAdapter.Fill(DataSet1, "MPPure");

            dataAdapter = new SqlDataAdapter("Select 'AG'+convert(varchar(50),idEM) [CODEEM],idEM from Emballage ", cn);
            dataAdapter.Fill(DataSet1, "EMCMBOX");


            cn.Close();

            dataGridClient.DataSource = DataSet1.Tables["Client"];
            dataGridFourni.DataSource = DataSet1.Tables["Fournisseur"];
            dataGridEmballage.DataSource = DataSet1.Tables["Emballage"];
            dataGridMatiereP.DataSource = DataSet1.Tables["MatiereP"];
            dataGrideHistoriqueV.DataSource = DataSet1.Tables["HistoriqueV"];
            dataGrideHistoriqueA.DataSource = DataSet1.Tables["HistoriqueA"];
            datagridAffProduitF.DataSource = DataSet1.Tables["Produit_finis"];
            datagridAffProduitF.Columns[datagridAffProduitF.Columns["idPF"].Index].Visible = false;

                rbtnAddHistoAMatierePrem.Checked = true;

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

                for (int i = 0; i < DataSet1.Tables["Produit_finis"].Columns.Count; i++)
                {
                    cmbProduitFinis.Items.Add(DataSet1.Tables["Produit_finis"].Columns[i].ColumnName.ToString());
                }
                cmbProduitFinis.Text = cmbProduitFinis.Items[0].ToString();

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
                this.gunaComboBox5.DisplayMember = "UniteE";
                this.gunaComboBox5.ValueMember = "idE";
                this.gunaComboBox5.DataSource = DataSet1.Tables["Eatat"];
                this.cmbAddTypeMatierP.DisplayMember = "UniteE";
                this.cmbAddTypeMatierP.ValueMember = "idE";
                this.cmbAddTypeMatierP.DataSource = DataSet1.Tables["Etat"];
                this.gunaComboBox3.DisplayMember = "CODEEM";
                this.gunaComboBox3.ValueMember = "idEM";
                this.gunaComboBox3.DataSource = DataSet1.Tables["EMCMBOX"];
                //labbels
                this.LabelcountCl.Text = "Nombre de Clients : " + dataGridClient.Rows.Count;
                this.lblCountFourni.Text = "Nombre de Fournisseurs : " + dataGridFourni.Rows.Count;
                this.lblCountEmballage.Text = "Nombre des Emballages : " + dataGridEmballage.Rows.Count;
                this.lblCountMP.Text = "Nombre des Matieres Premieres : " + dataGridMatiereP.Rows.Count;
                this.lblCountHistoA.Text = "Nombre des Historique : " + dataGrideHistoriqueA.Rows.Count;
                this.lblCountHistorV.Text = "Nombre des Historique : " + dataGrideHistoriqueV.Rows.Count;
                this.lblCountProdF.Text = "Nombre des Produits : " + datagridAffProduitF.Rows.Count;
                this.gunaComboBox1.Items.Add("Voire");
                this.gunaComboBox1.Items.Add("Lire et voire");
                this.gunaComboBox1.Items.Add("Voir, lire modifier");
                this.gunaComboBox1.Items.Add("Administrateur");
            this.cmbAddHabituelHistorV.DataSource = DataSet1.Tables["produit_finis"];
                cmbAddHabituelHistorV.DisplayMember = "nomPF";
                cmbAddHabituelHistorV.ValueMember = "idPF";                
                this.gunaComboBox6.DataSource = DataSet1.Tables["Client"];
                this.gunaComboBox6.DisplayMember = "NomClient";
                this.gunaComboBox6.ValueMember = "NomClient";
                this.gunaComboBox5.DisplayMember = "UniteE";
                this.gunaComboBox5.ValueMember = "idE";
                this.gunaComboBox5.DataSource = DataSet1.Tables["Etat"];
            if(DataSet1.Tables["MatiereP"].Rows.Count != 0)
                gunaTextBox3.Text = DataSet1.Tables["MatiereP"].Rows[0][2].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtRchDgrid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dataView = new DataView(DataSet1.Tables["Client"]);
                dataView.RowFilter = "[" + CmbRechClientAfficher.SelectedItem.ToString() + "]like '%" + TxtRchDgrid.Text + "%'";

                dataGridClient.DataSource = dataView;
            }
            catch (Exception)
            {
                
                return;
            }
            
        }

        private void txtRechAffichForni_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                DataView dataView = new DataView(DataSet1.Tables["Fournisseur"]);
                dataView.RowFilter = "[Nom Fourni] like '%" + txtRechAffichForni.Text + "%'";
                dataGridFourni.DataSource = dataView;
            }
            catch (Exception)
            {                
                return;
            }
            
        }

        private void gunaTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                DataView dataView = new DataView(DataSet1.Tables["Emballage"]);
                dataView.RowFilter = "[Code Emballage] like '%" + TxtEmbafficherfind.Text + "%'";
                dataGridEmballage.DataSource = dataView;
            }
            catch (Exception)
            {
                
                return
                    ;
            }
            
        }

        private void gunaTextBox3_TextChanged_1(object sender, EventArgs e)
        {
            DataView dataView = new DataView(DataSet1.Tables["MatiereP"]);
            dataView.RowFilter = "[Code] like '%" + TxtMatierpfind.Text + "%'";
            dataGridMatiereP.DataSource = dataView;
        }



        private void txtRechAffichForni_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dataView = new DataView(DataSet1.Tables["Fournisseur"]);
                dataView.RowFilter = "[" + CmbRechFournisseurAfficher.SelectedItem.ToString() + "]like '%" + txtRechAffichForni.Text + "%'";
                dataGridFourni.DataSource = dataView;
            }
            catch (Exception)
            {                
                return;
            }
            
        }

        private void TxtEmbafficherfind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dataView = new DataView(DataSet1.Tables["Emballage"]);
                if (CmbAfficheEmballageRech.SelectedItem.ToString().ToLower() == "quantite" || CmbAfficheEmballageRech.SelectedItem.ToString().ToLower() == "support")
                {
                    if (TxtEmbafficherfind.Text == "")
                        dataGridEmballage.DataSource = DataSet1.Tables["Emballage"];
                    else
                        dataView.RowFilter = "[" + CmbAfficheEmballageRech.SelectedItem.ToString() + "] = " + TxtEmbafficherfind.Text;
                    
                }
                else
                    dataView.RowFilter = "[" + CmbAfficheEmballageRech.SelectedItem.ToString() + "]like '%" + TxtEmbafficherfind.Text + "%'";
                                
                dataGridEmballage.DataSource = dataView;
            }
            catch (Exception)
            {
                
                return;
            }
            
        }

        private void TxtMatierpfind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dataView = new DataView(DataSet1.Tables["MatiereP"]);

                if (CmbAfficherMatierpfind.SelectedItem.ToString().ToLower() == "quantite" || CmbAfficherMatierpfind.SelectedItem.ToString().ToLower() == "code")
                {
                    if (TxtMatierpfind.Text == "")
                        dataGridMatiereP.DataSource = DataSet1.Tables["MatiereP"];
                    else
                        dataView.RowFilter = "[" + CmbAfficherMatierpfind.SelectedItem.ToString() + "] = " + TxtMatierpfind.Text;

                }
                else
                dataView.RowFilter = "[" + CmbAfficherMatierpfind.SelectedItem.ToString() + "]like '%" + TxtMatierpfind.Text + "%'";
                dataGridMatiereP.DataSource = dataView;
            }
            catch (Exception)
            {                
                return;
            }
        }
        char sharp;
        private void TxtAfficherHistAfind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dataView = new DataView(DataSet1.Tables["HistoriqueA"]);

                sharp = ' ';
                if (CmbAfficherHistoriqueAfind.SelectedItem.ToString().ToLower() == "date achat")
                    sharp = '#';

                if (CmbAfficherHistoriqueAfind.SelectedItem.ToString().ToLower() == "quantite" || CmbAfficherHistoriqueAfind.SelectedItem.ToString().ToLower() == "num achat" || CmbAfficherHistoriqueAfind.SelectedItem.ToString().ToLower() == "prix" || CmbAfficherHistoriqueAfind.SelectedItem.ToString().ToLower() == "date achat")
                {
                    if (TxtAfficherHistAfind.Text == "")
                        dataGrideHistoriqueA.DataSource = DataSet1.Tables["HistoriqueA"];
                    else
                        dataView.RowFilter = "[" + CmbAfficherHistoriqueAfind.SelectedItem.ToString() + "] = "+ sharp + TxtAfficherHistAfind.Text + sharp;

                }
                else
                dataView.RowFilter = "[" + CmbAfficherHistoriqueAfind.SelectedItem.ToString() + "]like '%" + TxtAfficherHistAfind.Text + "%'";
                dataGrideHistoriqueA.DataSource = dataView;
            }
            catch (Exception)
            {
                return;
            }
            
        }

        private void TxtAffHistVfind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dataView = new DataView(DataSet1.Tables["HistoriqueV"]);
                
                sharp = ' ';
                if (CmbAfficherHistVfind.SelectedItem.ToString().ToLower() == "date vente")
                    sharp = '#';

                if (CmbAfficherHistVfind.SelectedItem.ToString().ToLower() == "quantite" || CmbAfficherHistVfind.SelectedItem.ToString().ToLower() == "num vente" || CmbAfficherHistVfind.SelectedItem.ToString().ToLower() == "prix" || CmbAfficherHistVfind.SelectedItem.ToString().ToLower() == "date vente")
                {
                    if (TxtAffHistVfind.Text == "")
                        dataGrideHistoriqueV.DataSource = DataSet1.Tables["HistoriqueV"];
                    else
                        dataView.RowFilter = "[" + CmbAfficherHistVfind.SelectedItem.ToString() + "] = " + sharp + TxtAffHistVfind.Text + sharp;

                }
                else
                dataView.RowFilter = "[" + CmbAfficherHistVfind.SelectedItem.ToString() + "]like '%" + TxtAffHistVfind.Text.ToString() + "%'";
                dataGrideHistoriqueV.DataSource = dataView;
            }
            catch (Exception)
            {                
                return;
            }

        }

        private void BtnAddClt_Click(object sender, EventArgs e)
        {
            try
            {
                //Ajouter mode Déco using dataset
                DataSet1.Tables["Client"].Rows.Add(TxtNomaddclt.Text, txtaddnumclt.Text, txtaddadressclt.Text, txtaddmailclt.Text, cmbtypecltadd.SelectedItem.ToString(), txtaddrcclt.Text);
                dataAdapter = new SqlDataAdapter("select nomClt as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["Client"]);
                TxtNomaddclt.Text = "";
                TxtNomaddclt.Focus();
                txtaddnumclt.Text = "";
                txtaddadressclt.Text = "";
                txtaddmailclt.Text = "";
                txtaddrcclt.Text = "";
                MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }

        private void TxtModifClientfind_TextChanged(object sender, EventArgs e)
        {

        }
        private void BtnModifierclt_Click(object sender, EventArgs e)
        {
        }

        private void btnAddFourni_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet1.Tables["Fournisseur"].Rows.Add(txtNomAddFourni.Text, txtTelAddFourni.Text, txtAdresseAddFourni.Text, txtMailAddFourni.Text, txtRcAddFourni.Text);
                dataAdapter = new SqlDataAdapter("select nomFournisseur [Nom Fourni] ,numTel [Num Tel] , adresse [Adresse] , email [Mail] , RCF [RC] from Fournisseur", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["Fournisseur"]);
                txtNomAddFourni.Text = "";
                txtNomAddFourni.Focus();
                txtTelAddFourni.Text = "";
                txtAdresseAddFourni.Text = "";
                txtMailAddFourni.Text = "";
                txtRcAddFourni.Text = "";
                MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            
        }

        private void btnAddImageEmballage_Click(object sender, EventArgs e)
        {
            gunaTransition1.Hide(gunaElipsePanel1);
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBoxEM.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxEM.Image = new Bitmap(open.FileName);
                FilePathEM.Text = open.FileName;
                gunaTransition1.Show(gunaElipsePanel1);
            }
            else
                gunaTransition1.Show(gunaElipsePanel1);
        }

        private void btnAddEmballage_Click(object sender, EventArgs e)
        {
            try
            {
                
                //DataSet1.Tables["EmballagePure"].Rows.Add("s","ss","sss","sss","sss","sss");
                dataAdapter = new SqlDataAdapter("select RCF [RCsF] , qteEM [Quantite] , idE [idE] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage ", cn);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                //DataSet1.Tables["EmballagePure"].Clear();
                cn.Open();
                dataAdapter.Fill(DataSet1, "EmballagePure");
                cn.Close();
                int x = DataSet1.Tables["EmballagePure"].Rows.Count + 1;
                MessageBox.Show(x.ToString());
                DataSet1.Tables["EmballagePure"].Rows.Add(cmbAddNomFournisseurEmballage.SelectedValue.ToString(), txtAddquantiteEmballage.Text, cmbAddTypeEmballge.SelectedValue.ToString(), txtAddSupportEmballage.Text, txtAddDescriptionEmballage.Text, txtAddEmballageCordonn.Text);
                SqlCommand cmd = new SqlCommand("update emballage set img=(SELECT BulkColumn FROM Openrowset( Bulk '" + this.FilePathEM.Text + "', Single_Blob) as img) where idEM='" + x.ToString() + "'", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                MessageBox.Show(this.FilePathEM.Text);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["EmballagePure"]);
                if (FilePathEM.Text != null)
                    cmd.ExecuteNonQuery();
                if (DataSet1.Tables["EmballagePure"].Rows.Count > 0)
                    DataSet1.Tables["EmballagePure"].Clear();
                dataAdapter = new SqlDataAdapter("select 'AG'+convert(varchar(50),idEM) [Code Emballage], nomFournisseur [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataSet1.Tables["Emballage"].Clear();
                dataAdapter.Fill(DataSet1, "Emballage");

                MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                txtAddquantiteEmballage.Text = "";
                //txtNomAddFourni.Focus();
                txtAddSupportEmballage.Text = "";
                txtAddDescriptionEmballage.Text = "";

                DataSet1.Tables["Emballage"].Clear();
                dataAdapter = new SqlDataAdapter("select ISNULL('AG'+convert(varchar(50),idEM) ,'')[Code Emballage],ISNULL(nomFournisseur,'notfound') [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
                dataAdapter.Fill(DataSet1, "Emballage");
                gunaTransition1.Hide(gunaElipsePanel1);
                cn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
                
            
            
        }

        private void batnAddMatiereP_Click(object sender, EventArgs e)
        {
            try
            {
                //Etage Salle non ajouter /!\
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], RCF [RCF] , idE [idE] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée], idMP from Matiere_premiere ", cn);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Fill(DataSet1, "MPPure");
                cn.Close();
                DataSet1.Tables["MPPure"].Rows.Add(txtaddcodemp.Text, txtAddNomMatiereP.Text, cmbAddNomFourniMatierP.SelectedValue.ToString(), cmbAddTypeMatierP.SelectedValue.ToString(), txtAddQuantiteMatiereP.Text, txtAddDescMatierP.Text, txtAddMatierPCordo.Text);
                DataSet1.Tables["MatiereP"].Rows.Add(txtaddcodemp.Text, txtAddNomMatiereP.Text, cmbAddNomFourniMatierP.SelectedText.ToString());
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["MPPure"]);
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
                DataSet1.Tables["MatiereP"].Clear();
                dataAdapter.Fill(DataSet1, "MatiereP");
                
                MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                txtaddcodemp.Text = "";
                txtaddcodemp.Focus();
                txtAddNomMatiereP.Text = "";
                txtAddQuantiteMatiereP.Text = "";
                txtAddDescMatierP.Text = "";

                DataSet1.Tables["MatiereP"].Clear();
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
                dataAdapter.Fill(DataSet1, "MatiereP");
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }

        }

        private void tabControl5_MouseClick(object sender, MouseEventArgs e)
        {

        }
        string rc;
        private void btnaddhistoriquedachat_Click(object sender, EventArgs e)
        {
            
            try
            {

                for (int i = 0; i < DataSet1.Tables["Fournisseur"].Rows.Count; i++)
                {
                    if (gunaTextBox3.Text == DataSet1.Tables["Fournisseur"].Rows[i][0].ToString())
                    {
                        rc = DataSet1.Tables["Fournisseur"].Rows[i][4].ToString();
                    }
                }

                dataAdapter = new SqlDataAdapter("select RCF,qteA,prix,dateA,idEM,idMP from historique_achat", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Fill(DataSet1, "HistoriqueAP");
                cn.Close();
                if (rbtnAddHistoAMatierePrem.Checked)
                    DataSet1.Tables["HistoriqueAP"].Rows.Add(rc, txtquantiteaddhistoriquedachat.Text, prixaddhistoriquedachat.Text, dateventeaddhistoriquedachat.Value.ToString(), null, cmbnomproduitaddhistoriquedachat.SelectedValue.ToString());
                else
                    DataSet1.Tables["HistoriqueAP"].Rows.Add(rc, txtquantiteaddhistoriquedachat.Text, prixaddhistoriquedachat.Text, dateventeaddhistoriquedachat.Value.ToString(), cmbnomproduitaddhistoriquedachat.SelectedValue.ToString().Substring(2));
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["HistoriqueAP"]);                
                MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                txtquantiteaddhistoriquedachat.Text = "";
                txtquantiteaddhistoriquedachat.Focus();
                prixaddhistoriquedachat.Text = "";

                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");
                cn.Close();

                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ;
            }
        }
        
        private void gunaGradientButton6_Click(object sender, EventArgs e)
        {
            try
            {
                dataAdapter = new SqlDataAdapter("select nomClt as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1, "Client");
                MessageBox.Show("Modification effectuée avec succès");
                DataSet1.Tables["HistoriqueV"].Clear();
                dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueV");
                actualiser();
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("quelque chose a mal tourné");
                return;
            }

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
            this.LabelcountCl.Text = "Nombre de clients : " + dataGridClient.Rows.Count;
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
            try
            {
                dataAdapter = new SqlDataAdapter("select nomFournisseur [Nom Fourni] ,numTel [Num Tel] , adresse [Adresse] , email [Mail] , RCF [RC] from Fournisseur", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1, "Fournisseur");
                MessageBox.Show("Modification effectuée avec succès");

                DataSet1.Tables["Emballage"].Clear();
                dataAdapter = new SqlDataAdapter("select  'AG'+convert(varchar(50),idEM) [Code Emballage],nomFournisseur [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
                dataAdapter.Fill(DataSet1, "Emballage");

                DataSet1.Tables["MatiereP"].Clear();
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
                dataAdapter.Fill(DataSet1, "MatiereP");

                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");
                actualiser();
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("quelque chose a mal tourné");
                return;
            }
            
        }

        private void dataGridFourni_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            this.lblCountFourni.Text = "Nombre de Fournisseurs : " + dataGridFourni.Rows.Count;
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
            /*try
            {*/
                

                dataAdapter = new SqlDataAdapter("Select * from Produit_finis", cn);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Fill(DataSet1, "ProduitsFPure");
                cn.Close();
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataSet1.Tables["ProduitsFPure"].Rows.Add(string.Join(null, System.Text.RegularExpressions.Regex.Split(gunaTextBox4.Text, "[^\\d]")), this.gunaTextBox5.Text.ToString(), this.gunaComboBox3.SelectedValue.ToString(), txtAddProdFCordo.Text, richTextBox1.Text.ToString(), this.gunaTextBox2.Text.ToString(), string.Join(null, System.Text.RegularExpressions.Regex.Split(gunaTextBox4.Text, "[^a-zA-Z]")),"1", gunaComboBox5.SelectedValue.ToString());
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1, "ProduitsFPure");
                gunaTextBox4.Text = "";
                gunaTextBox4.Focus();
                gunaTextBox5.Text = "";
                richTextBox1.Text = "";
                gunaTextBox2.Text = "";
                MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                DataSet1.Tables["Produit_finis"].Clear();
                dataAdapter = new SqlDataAdapter("select nomPF + ' ' +CONVERT(varchar(50),codePF) [Code Prodiuit],'AG' +CONVERT(varchar(50),Emballage.idEM )[Emballage],qteEM [Quantite],Etat.NomE [Etat],cordoEM [Cordonnees],descEM [Descrption], idPF from Produit_finis inner join Emballage on Emballage.idEM = Produit_finis.idEM inner join Etat on Produit_finis.idE = Etat.idE", cn);
                dataAdapter.Fill(DataSet1, "Produit_finis");

                cn.Close();
            /*}
            catch (Exception)
            {
                MessageBox.Show("quelque chose a mal tourné");
                return;
            }*/


        }

        private void btnaddhistoriqueV_Click(object sender, EventArgs e)
        {
            try
            {
                dataAdapter = new SqlDataAdapter("select idPF , RC_CIN,qteV,prix,dateV from historique_vente", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Fill(DataSet1, "HistoriqueVP");
                cn.Close();
                
                for (int i = 0; i < DataSet1.Tables["Client"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["Client"].Rows[i][0].ToString() == gunaComboBox6.SelectedValue.ToString())
                    {
                        DataSet1.Tables["HistoriqueVP"].Rows.Add(cmbAddHabituelHistorV.SelectedValue.ToString(), DataSet1.Tables["Client"].Rows[i][5].ToString(), txtquantiteaddhistoriqueVent.Text, prixaddhistoriqueVent.Text, dateventeaddhistoriqueV.Value.ToString());
                        break;
                    }
                }
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();                    
                }
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["HistoriqueVP"]);
                MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                txtquantiteaddhistoriqueVent.Text = "";
                txtquantiteaddhistoriqueVent.Focus();
                prixaddhistoriqueVent.Text = "";

                DataSet1.Tables["HistoriqueV"].Clear();
                dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueV");

                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dataView = new DataView(DataSet1.Tables["Produit_finis"]);
                if (cmbProduitFinis.SelectedItem.ToString().ToLower() == "quantite")
                {
                    if (gunaTextBox1.Text == "")
                        datagridAffProduitF.DataSource = DataSet1.Tables["Produit_finis"];
                    else
                        dataView.RowFilter = "[" + cmbProduitFinis.SelectedItem.ToString() + "] = " + gunaTextBox1.Text;

                }
                else
                dataView.RowFilter = "[" + cmbProduitFinis.SelectedItem.ToString() + "]like '%" + gunaTextBox1.Text.ToString() + "%'";
                datagridAffProduitF.DataSource = dataView;
            }
            catch (Exception)
            {
                return;
            }
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
        bool b,b2,nom;
        private void btnSaveEmballage_Click(object sender, EventArgs e)
        {
            try
            {
                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();

                cmd = new SqlCommand("select RCF ,idE from Fournisseur,Etat where nomFournisseur = @a and UniteE = @b'", cn);
                

                for (int i = 0; i < dataGridEmballage.Rows.Count; i++)
                {
                    cmd = new SqlCommand("update Emballage set RCF = (select RCF from Fournisseur where nomFournisseur = @b),qteEM = @c, idE = (select idE from Etat where UniteE = @d) , supportEM = @e,descEM = @f,cordoEM = @j where idEM = @a", cn);
                   // SqlCommand cmd2 = new SqlCommand("delete from emballage where idEM = @a", cn);
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

                        if (dataGridEmballage.Rows[i].Cells[1].Value.ToString() == DataSet1.Tables["Fournisseur"].Rows[j][0].ToString() && (dataGridEmballage.Rows[i].Cells[3].Value.ToString().ToLower() == "g" || dataGridEmballage.Rows[i].Cells[3].Value.ToString().ToLower() == "ml"))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            MessageBox.Show("Modification effectuée avec succès");
                            msgbx = false;
                        }
                    }
                    else
                        MessageBox.Show("la valeur " + dataGridEmballage.Rows[i].Cells[1].Value.ToString() + " ou " + dataGridEmballage.Rows[i].Cells[3].Value.ToString() + " de la lign " + (i + 1).ToString() + " est invalide");

                }

                for (int i = 0; i < DataSet1.Tables["Emballage"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["Emballage"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from emballage where idEM ="+DataSet1.Tables["Emballage"].Rows[i][0,DataRowVersion.Original].ToString().Remove(0,2),cn);
                        cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["Emballage"].Clear();
                dataAdapter = new SqlDataAdapter("select ISNULL('AG'+convert(varchar(50),idEM) ,'')[Code Emballage],ISNULL(nomFournisseur,'notfound') [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
                dataAdapter.Fill(DataSet1, "Emballage");

                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");

                DataSet1.Tables["Produit_finis"].Clear();
                dataAdapter = new SqlDataAdapter("select nomPF + ' ' +CONVERT(varchar(50),codePF) [Code Prodiuit],'AG' +CONVERT(varchar(50),Emballage.idEM )[Emballage],qteEM [Quantite],Etat.NomE [Etat],cordoEM [Cordonnees],descEM [Descrption],idpf from Produit_finis inner join Emballage on Emballage.idEM = Produit_finis.idEM inner join Etat on Produit_finis.idE = Etat.idE", cn);
                dataAdapter.Fill(DataSet1, "Produit_finis");
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("quelque chose a mal tourné");
                DataSet1.Tables["Emballage"].Clear();
                dataAdapter = new SqlDataAdapter("select ISNULL('AG'+convert(varchar(50),idEM) ,'')[Code Emballage],ISNULL(nomFournisseur,'notfound') [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
                dataAdapter.Fill(DataSet1, "Emballage");
                return;
            }
        }
        SqlCommand cmd = new SqlCommand();

        private void btnSaveMatiereP_Click(object sender, EventArgs e)
        {
            try
            {
                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
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

                        if (dataGridMatiereP.Rows[i].Cells[2].Value.ToString() == DataSet1.Tables["Fournisseur"].Rows[j][0].ToString() && (dataGridMatiereP.Rows[i].Cells[3].Value.ToString().ToLower() == "g" || dataGridMatiereP.Rows[i].Cells[3].Value.ToString().ToLower() == "ml"))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    { 
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            MessageBox.Show("Modification effectuée avec succès");
                            msgbx = false;
                        }
                    }
                    else
                        MessageBox.Show("la valeur " + dataGridMatiereP.Rows[i].Cells[2].Value.ToString() + " ou " + dataGridMatiereP.Rows[i].Cells[3].Value.ToString() + " de la lign " + (i + 1).ToString() + "  est invalide");



                }
                for (int i = 0; i < DataSet1.Tables["MatiereP"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["MatiereP"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from Matiere_premiere where codeMP =" + DataSet1.Tables["MatiereP"].Rows[i][0, DataRowVersion.Original].ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["MatiereP"].Clear();
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
                dataAdapter.Fill(DataSet1, "MatiereP");

                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("quelque chose a mal tourné");
                DataSet1.Tables["MatiereP"].Clear();
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
                dataAdapter.Fill(DataSet1, "MatiereP");
                return;
            }

            
        }

        private void plusDinformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Emballage_Viewer EM = new Emballage_Viewer();
            TextBox textBox = new TextBox();
            textBox = (TextBox)EM.Controls["groupBox2"].Controls["textBox1"];
            textBox.Text = dataGridEmballage.SelectedRows[0].Cells[0].Value.ToString();
            EM.ShowDialog();
        }

        private void btnSaveHistoA_Click(object sender, EventArgs e)
        {
            try
            {
                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                for (int i = 0; i < dataGrideHistoriqueA.Rows.Count; i++)
                {
                    b = false;
                    b2 = false;
                    nom = false;

                    if (dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().Contains("AG"))
                    {
                        cmd = new SqlCommand("update historique_achat set idMP = NULL, idEM = @b , RCF = (select RCF from Fournisseur where nomFournisseur = @c),qteA = @d ,prix = @e , dateA = @f where numAchat = @a", cn);
                        cmd.Parameters.AddWithValue("@b", dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().Remove(0, 2));
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
                        cmd = new SqlCommand("update historique_achat set idEM = NULL, idMP = (select idmp from Matiere_premiere where codeMP = @b) , RCF = (select RCF from Fournisseur where nomFournisseur = @c),qteA = @d ,prix = @e , dateA = @f where numAchat = @a", cn);
                        cmd.Parameters.AddWithValue("@b", dataGrideHistoriqueA.Rows[i].Cells[1].Value);
                        for (int k = 0; k < DataSet1.Tables["MPPure"].Rows.Count; k++)
                        {
                            if (dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString() == DataSet1.Tables["MPPure"].Rows[k][0].ToString())
                            {
                                b2 = true;
                                break;
                            }
                        }
                        for (int k = 0; k < DataSet1.Tables["MPPure"].Rows.Count; k++)
                        {
                            if (dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().ToLower() == DataSet1.Tables["MPPure"].Rows[k][1].ToString().ToLower())
                            {
                                nom = true;
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
                    if (!b2 && nom)
                        //hna ghi mal9it manktb 3liha drt nom = true , ghi bach maidozch l dok les ifs lokhrin
                        nom = true;
                    else if (b && b2)
                    {
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            MessageBox.Show("Modification effectuée avec succès");
                            msgbx = false;
                        }
                    }
                    else if (!b && !b2)
                        MessageBox.Show("la valeur '" + dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString() + "' et '" + dataGrideHistoriqueA.Rows[i].Cells[2].Value.ToString() + "' d'achat numero : " + dataGrideHistoriqueA.Rows[i].Cells[0].Value.ToString() + " est invalide");
                    else if (!b)
                        MessageBox.Show("la valeur '" + dataGrideHistoriqueA.Rows[i].Cells[2].Value.ToString() + "' d'achat numero : " + dataGrideHistoriqueA.Rows[i].Cells[0].Value.ToString() + " est invalide");
                    else if (!b2)
                        MessageBox.Show("la valeur '" + dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString() + "' d'achat numero : " + dataGrideHistoriqueA.Rows[i].Cells[0].Value.ToString() + " est invalide");
                }

                for (int i = 0; i < DataSet1.Tables["HistoriqueA"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["HistoriqueA"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from historique_achat where numAchat = " + DataSet1.Tables["HistoriqueA"].Rows[i][0, DataRowVersion.Original].ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("quelque chose a mal tourné");
                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");
                return;
            }

            
        }

        private void gunaGradientButton22_Click(object sender, EventArgs e)
        {
            
            try
            {
                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                for (int i = 0; i < dataGrideHistoriqueV.Rows.Count; i++)
                {
                    b2 = false;
                    b = false;
                    String[] str = dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString().Split(' ');
                    if (str.Length != 2)
                    {
                        MessageBox.Show("le Nom de produit de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString() + " est incorrect , veuillez respecter la syntaxe d'insertion 'Nom'+ espace + 'Code'");
                        break;
                    }
                    cmd = new SqlCommand("update historique_vente set  idPF = (select idPF from Produit_finis where nomPF = @b and codePF = @c) , RC_CIN = (select RC_CIN from Client where nomClt = @d) , qteV = @e , prix = @f , dateV  = @j where numVente = @a", cn);
                    cmd.Parameters.AddWithValue("@a", dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                    cmd.Parameters.AddWithValue("@b", str[0]);
                    cmd.Parameters.AddWithValue("@c", str[1]);
                    cmd.Parameters.AddWithValue("@d", dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString());
                    cmd.Parameters.AddWithValue("@e", dataGrideHistoriqueV.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@f", dataGrideHistoriqueV.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@j", dataGrideHistoriqueV.Rows[i].Cells[5].Value);

                    for (int k = 0; k < DataSet1.Tables["Produit_finis"].Rows.Count; k++)
                    {
                        if (dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString().ToLower() == DataSet1.Tables["Produit_finis"].Rows[k][0].ToString().ToLower())
                        {
                            b2 = true;
                            break;
                        }
                    }

                    for (int k = 0; k < DataSet1.Tables["Client"].Rows.Count; k++)
                    {
                        if (dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString().ToLower() == DataSet1.Tables["Client"].Rows[k][0].ToString().ToLower())
                        {
                            b = true;
                            break;
                        }
                    }


                    if (b & b2)
                    {
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            MessageBox.Show("Modification effectuée avec succès");
                            msgbx = false;
                        }
                    }
                    else if (!b && !b2)
                        MessageBox.Show("le Produit '" + dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString() + "' et le Client '" + dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString() + "' est introuvable de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                    else if (!b)
                        MessageBox.Show("le Client '" + dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString() + "' est introuvable de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                    else if (!b2)
                        MessageBox.Show("le Produit '" + dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString() + "' est introuvable de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                }

                for (int i = 0; i < DataSet1.Tables["HistoriqueV"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["HistoriqueV"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from historique_vente where numVente = " + DataSet1.Tables["HistoriqueV"].Rows[i][0, DataRowVersion.Original].ToString(), cn);
                         cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["HistoriqueV"].Clear();
                dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueV");
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("quelque chose a mal tourné");
                DataSet1.Tables["HistoriqueV"].Clear();
                dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueV");
                return;
            }
            
            
        }

        private void gunaGradientButton3_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("DBshema.xml");
            Print_Form print_Form = new Print_Form();
            print_Form.Printer(DataSet1.Tables["Client"]);
            print_Form.Show();
        }
        bool msgbx;

        private void gunaGradientButton9_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("DBshema.xml");
            Print_Form print_Form = new Print_Form();
            print_Form.Printer(DataSet1.Tables["Fournisseur"]);
            print_Form.Show();
        }

        private void gunaGradientButton13_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("DBshema.xml");
            Print_Form print_Form = new Print_Form();
            print_Form.Printer(DataSet1.Tables["Emballage"]);
            print_Form.Show();
        }

        private void gunaGradientButton17_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("DBshema.xml");
            Print_Form print_Form = new Print_Form();
            print_Form.Printer(DataSet1.Tables["MatiereP"]);
            print_Form.Show();
        }

        private void gunaGradientButton21_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("DBshema.xml");
            Print_Form print_Form = new Print_Form();
            print_Form.Printer(DataSet1.Tables["HistoriqueA"]);
            print_Form.Show();
        }

        private void gunaGradientButton25_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("DBshema.xml");
            Print_Form print_Form = new Print_Form();
            print_Form.Printer(DataSet1.Tables["HistoriqueV"]);
            print_Form.Show();
        }

        private void gunaGradientButton29_Click(object sender, EventArgs e)
        {
            DataSet1.WriteXmlSchema("DBshema.xml");
            Print_Form print_Form = new Print_Form();
            print_Form.Printer(DataSet1.Tables["Produit_finis"]);
            print_Form.Show();
        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void gunaGradientButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addacc", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", gunaTextBox6.Text.ToString());
                cmd.Parameters.AddWithValue("@password", gunaTextBox7.Text.ToString());
                cmd.Parameters.AddWithValue("@permLevl", (gunaComboBox1.SelectedIndex + 1).ToString());
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void gunaGradientButton4_Click(object sender, EventArgs e)
        {
            actualiser();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gunaGradientButton4.PerformClick();
        }

        private void gunaGradientButton5_Click(object sender, EventArgs e)
        {
            actualiser();
        }

        private void gunaGradientButton26_Click(object sender, EventArgs e)
        {
            
            try
            {
                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                for (int i = 0; i < datagridAffProduitF.Rows.Count; i++)
                {
                    b2 = false;
                    b = false;
                    String[] str = datagridAffProduitF.Rows[i].Cells[0].Value.ToString().Split(' ');
                    cmd = new SqlCommand("update Produit_finis set idEM = @c , QtePF = @d , idE = (select idE from Etat where NomE = @e) , cordoPF = @f , descPF = @j where nomPF = @a and codePF = @b", cn);
                    cmd.Parameters.AddWithValue("@a", str[0]);
                    cmd.Parameters.AddWithValue("@b", str[1]);
                    cmd.Parameters.AddWithValue("@c", datagridAffProduitF.Rows[i].Cells[1].Value.ToString().Remove(0, 2));
                    cmd.Parameters.AddWithValue("@d", datagridAffProduitF.Rows[i].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@e", datagridAffProduitF.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@f", datagridAffProduitF.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@j", datagridAffProduitF.Rows[i].Cells[5].Value);

                    for (int k = 0; k < DataSet1.Tables["Emballage"].Rows.Count; k++)
                    {
                        if (datagridAffProduitF.Rows[i].Cells[1].Value.ToString().Remove(0, 2) == DataSet1.Tables["Emballage"].Rows[k][0].ToString().Remove(0, 2))
                        {
                            b2 = true;
                            break;
                        }
                    }
                    if (datagridAffProduitF.Rows[i].Cells[3].Value.ToString().ToLower() == "solide" || datagridAffProduitF.Rows[i].Cells[3].Value.ToString().ToLower() == "liquide")
                        b = true;

                    if (!b && !b2)
                        MessageBox.Show("l'Emballage et l'Etat de Produit est introuvable de Produit : " + datagridAffProduitF.Rows[i].Cells[0].Value.ToString());
                    else if (!b2)
                        MessageBox.Show("l'Emballage est introuvable");
                    else if (!b)
                        MessageBox.Show("l'etat est introuvable");
                    else
                    {
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            MessageBox.Show("Modification effectuée avec succès");
                            msgbx = false;
                        }
                    }
                }

                for (int i = 0; i < DataSet1.Tables["Produit_finis"].Rows.Count; i++)
                {
                    String[] str = DataSet1.Tables["Produit_finis"].Rows[i][0].ToString().Split(' ');
                    if (DataSet1.Tables["Produit_finis"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from Produit_finis where nomPF = "+ str[0] +" and codePF = @b " +str[1], cn);
                        MessageBox.Show(str[0] + " " + str[1]);
                        cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["Produit_finis"].Clear();
                dataAdapter = new SqlDataAdapter("select nomPF + ' ' +CONVERT(varchar(50),codePF) [Code Prodiuit],'AG' +CONVERT(varchar(50),Emballage.idEM )[Emballage],qteEM [Quantite],Etat.NomE [Etat],cordoEM [Cordonnees],descEM [Descrption],idpf from Produit_finis inner join Emballage on Emballage.idEM = Produit_finis.idEM inner join Etat on Produit_finis.idE = Etat.idE", cn);
                dataAdapter.Fill(DataSet1, "Produit_finis");

                DataSet1.Tables["HistoriqueV"].Clear();
                dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueV");
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("quelque chose a mal tourné");
                DataSet1.Tables["Produit_finis"].Clear();
                dataAdapter = new SqlDataAdapter("select nomPF + ' ' +CONVERT(varchar(50),codePF) [Code Prodiuit],'AG' +CONVERT(varchar(50),Emballage.idEM )[Emballage],qteEM [Quantite],Etat.NomE [Etat],cordoEM [Cordonnees],descEM [Descrption],idpf from Produit_finis inner join Emballage on Emballage.idEM = Produit_finis.idEM inner join Etat on Produit_finis.idE = Etat.idE", cn);
                dataAdapter.Fill(DataSet1, "Produit_finis");
                return;
            }

            
        }

        private void gunaGradientButton6_Click_1(object sender, EventArgs e)
        {
            actualiser();
        }

        private void gunaGradientButton7_Click(object sender, EventArgs e)
        {
            actualiser();
        }

        private void gunaGradientButton8_Click(object sender, EventArgs e)
        {
            actualiser();
        }

        private void gunaGradientButton10_Click(object sender, EventArgs e)
        {
            actualiser();
        }

        private void gunaGradientButton11_Click(object sender, EventArgs e)
        {
            actualiser();
        }
        bool fermerFourni = true, fermerClient = true, fermerEmballage = true, fermerMatiereP = true, fermerHistoA = true, fermerHistoV = true, fermerProdF = true;

        private void datagridAffProduitF_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            this.lblCountProdF.Text = "Nombre des Produits : " + datagridAffProduitF.Rows.Count;
        }

        private void dataGrideHistoriqueV_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            this.lblCountHistorV.Text = "Nombre des Historique : " + dataGrideHistoriqueV.Rows.Count;
        }

        private void dataGrideHistoriqueA_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            this.lblCountHistoA.Text = "Nombre des Historique : " + dataGrideHistoriqueA.Rows.Count;
        }

        private void dataGridMatiereP_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            this.lblCountMP.Text = "Nombre des Matieres Premieres : " + dataGridMatiereP.Rows.Count;
        }

        private void dataGridEmballage_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            this.lblCountEmballage.Text = "Nombre des Emballages : " + dataGridEmballage.Rows.Count;
        }

        private void menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DataSet1.Tables["Client"].Rows.Count != 0)
            {
                for (int i = 0; i < DataSet1.Tables["Client"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["Client"].Rows[i].RowState == DataRowState.Modified || DataSet1.Tables["Client"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        fermerClient = false;
                        break;
                    }
                }
            }

            if (DataSet1.Tables["fournisseur"].Rows.Count != 0)
            {
                for (int i = 0; i < DataSet1.Tables["fournisseur"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["fournisseur"].Rows[i].RowState == DataRowState.Modified || DataSet1.Tables["fournisseur"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        fermerFourni = false;
                        break;
                    }
                }
            }

            if (DataSet1.Tables["Emballage"].Rows.Count != 0)
            {
                for (int i = 0; i < DataSet1.Tables["Emballage"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["Emballage"].Rows[i].RowState == DataRowState.Modified || DataSet1.Tables["Emballage"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        fermerEmballage = false;
                        break;
                    }
                }
            }

            if (DataSet1.Tables["MatiereP"].Rows.Count != 0)
            {
                for (int i = 0; i < DataSet1.Tables["MatiereP"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["MatiereP"].Rows[i].RowState == DataRowState.Modified || DataSet1.Tables["MatiereP"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        fermerMatiereP = false;
                        break;
                    }
                }
                
            }

            if (DataSet1.Tables["HistoriqueA"].Rows.Count != 0)
            {
                for (int i = 0; i < DataSet1.Tables["HistoriqueA"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["HistoriqueA"].Rows[i].RowState == DataRowState.Modified || DataSet1.Tables["HistoriqueA"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        fermerHistoA = false;
                        break;
                    }
                }
                
            }


            if (DataSet1.Tables["HistoriqueV"].Rows.Count != 0)
            {
                for (int i = 0; i < DataSet1.Tables["HistoriqueV"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["HistoriqueV"].Rows[i].RowState == DataRowState.Modified || DataSet1.Tables["HistoriqueV"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        fermerHistoV = false;
                        break;
                    }
                }
                
            }

            if (DataSet1.Tables["Produit_finis"].Rows.Count != 0)
            {
                for (int i = 0; i < DataSet1.Tables["Produit_finis"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["Produit_finis"].Rows[i].RowState == DataRowState.Modified || DataSet1.Tables["Produit_finis"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        fermerProdF = false;
                        break;
                    }
                }
                
            }
            if (!fermerClient || !fermerFourni || !fermerEmballage || !fermerHistoA || !fermerHistoV || !fermerMatiereP || !fermerProdF)
            {
                DialogResult dialogResult = MessageBox.Show("voulez-Vous Enregistrer les Modificaions?", "attention", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    save();
                    MessageBox.Show("Modification effectues avec succes");
                    Program.form1.Close();
                }
                else if (dialogResult == DialogResult.Cancel)
                    e.Cancel = true;
                else if(dialogResult == DialogResult.No)
                    Program.form1.Close();
            }
            if (fermerClient && fermerFourni && fermerEmballage && fermerHistoA && fermerHistoV && fermerMatiereP && fermerProdF)
            {
                Program.form1.Close();
            }
        }

        private void cmbnomproduitaddhistoriquedachat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnAddHistoAMatierePrem.Checked)
            {
                for (int i = 0; i < DataSet1.Tables["MatiereP"].Rows.Count; i++)
                {
                    if (cmbnomproduitaddhistoriquedachat.Text == DataSet1.Tables["MatiereP"].Rows[i][1].ToString())
                    {
                        gunaTextBox3.Text = DataSet1.Tables["MatiereP"].Rows[i][2].ToString();
                    }

                }
            }
            else 
            {
                for (int i = 0; i < DataSet1.Tables["Emballage"].Rows.Count; i++)
                {
                    if (cmbnomproduitaddhistoriquedachat.Text == DataSet1.Tables["Emballage"].Rows[i][0].ToString())
                    {
                        gunaTextBox3.Text = DataSet1.Tables["Emballage"].Rows[i][1].ToString();
                    }

                }
            }
        }

        void actualiser()
        {
            try
            {
                DataSet1.Tables["Client"].Clear();
                DataSet1.Tables["Fournisseur"].Clear();
                DataSet1.Tables["Emballage"].Clear();
                DataSet1.Tables["MatiereP"].Clear();
                DataSet1.Tables["HistoriqueA"].Clear();
                DataSet1.Tables["HistoriqueV"].Clear();
                DataSet1.Tables["Produit_finis"].Clear();
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
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

                dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueV");

                dataAdapter = new SqlDataAdapter("select nomPF + ' ' +CONVERT(varchar(50),codePF) [Code Prodiuit],'AG' +CONVERT(varchar(50),Emballage.idEM )[Emballage],qteEM [Quantite],Etat.NomE [Etat],cordoEM [Cordonnees],descPF [Descrption], idPF from Produit_finis inner join Emballage on Emballage.idEM = Produit_finis.idEM inner join Etat on Produit_finis.idE = Etat.idE", cn);
                dataAdapter.Fill(DataSet1, "Produit_finis");

                dataAdapter = new SqlDataAdapter("Select * from Historique_Achat", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueAchatPure");

                dataAdapter = new SqlDataAdapter("Select * from Etat", cn);
                dataAdapter.Fill(DataSet1, "Etat");

                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], RCF [RCF] , idE [idE] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée], idMP from Matiere_premiere ", cn);
                dataAdapter.Fill(DataSet1, "MPPure");

                dataAdapter = new SqlDataAdapter("Select 'AG'+convert(varchar(50),idEM) [CODEEM],idEM from Emballage ", cn);
                dataAdapter.Fill(DataSet1, "EMCMBOX");
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        void save()
        {
            try
            {
                dataAdapter = new SqlDataAdapter("select nomClt as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1, "Client");

                DataSet1.Tables["HistoriqueV"].Clear();
                dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueV");

                cn.Close();


                //fourni
                dataAdapter = new SqlDataAdapter("select nomFournisseur [Nom Fourni] ,numTel [Num Tel] , adresse [Adresse] , email [Mail] , RCF [RC] from Fournisseur", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                dataAdapter.Update(DataSet1, "Fournisseur");

                DataSet1.Tables["Emballage"].Clear();
                dataAdapter = new SqlDataAdapter("select  'AG'+convert(varchar(50),idEM) [Code Emballage],nomFournisseur [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
                dataAdapter.Fill(DataSet1, "Emballage");

                DataSet1.Tables["MatiereP"].Clear();
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
                dataAdapter.Fill(DataSet1, "MatiereP");

                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");

                cn.Close();

                //emba

                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();

                cmd = new SqlCommand("select RCF ,idE from Fournisseur,Etat where nomFournisseur = @a and UniteE = @b'", cn);


                for (int i = 0; i < dataGridEmballage.Rows.Count; i++)
                {
                    cmd = new SqlCommand("update Emballage set RCF = (select RCF from Fournisseur where nomFournisseur = @b),qteEM = @c, idE = (select idE from Etat where UniteE = @d) , supportEM = @e,descEM = @f,cordoEM = @j where idEM = @a", cn);
                    // SqlCommand cmd2 = new SqlCommand("delete from emballage where idEM = @a", cn);
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

                        if (dataGridEmballage.Rows[i].Cells[1].Value.ToString() == DataSet1.Tables["Fournisseur"].Rows[j][0].ToString() && (dataGridEmballage.Rows[i].Cells[3].Value.ToString().ToLower() == "g" || dataGridEmballage.Rows[i].Cells[3].Value.ToString().ToLower() == "ml"))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            msgbx = false;
                        }
                    }
                    else
                        MessageBox.Show("la valeur " + dataGridEmballage.Rows[i].Cells[1].Value.ToString() + " ou " + dataGridEmballage.Rows[i].Cells[3].Value.ToString() + " de la lign " + (i + 1).ToString() + " est invalide");

                }

                for (int i = 0; i < DataSet1.Tables["Emballage"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["Emballage"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from emballage where idEM =" + DataSet1.Tables["Emballage"].Rows[i][0, DataRowVersion.Original].ToString().Remove(0, 2), cn);
                        cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["Emballage"].Clear();
                dataAdapter = new SqlDataAdapter("select ISNULL('AG'+convert(varchar(50),idEM) ,'')[Code Emballage],ISNULL(nomFournisseur,'notfound') [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
                dataAdapter.Fill(DataSet1, "Emballage");

                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");

                DataSet1.Tables["Produit_finis"].Clear();
                dataAdapter = new SqlDataAdapter("select nomPF + ' ' +CONVERT(varchar(50),codePF) [Code Prodiuit],'AG' +CONVERT(varchar(50),Emballage.idEM )[Emballage],qteEM [Quantite],Etat.NomE [Etat],cordoEM [Cordonnees],descEM [Descrption],idpf from Produit_finis inner join Emballage on Emballage.idEM = Produit_finis.idEM inner join Etat on Produit_finis.idE = Etat.idE", cn);
                dataAdapter.Fill(DataSet1, "Produit_finis");
                cn.Close();

                //matP

                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
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

                        if (dataGridMatiereP.Rows[i].Cells[2].Value.ToString() == DataSet1.Tables["Fournisseur"].Rows[j][0].ToString() && (dataGridMatiereP.Rows[i].Cells[3].Value.ToString().ToLower() == "g" || dataGridMatiereP.Rows[i].Cells[3].Value.ToString().ToLower() == "ml"))
                        {
                            b = true;
                            break;
                        }
                    }
                    if (b)
                    {
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            msgbx = false;
                        }
                    }
                    else
                        MessageBox.Show("la valeur " + dataGridMatiereP.Rows[i].Cells[2].Value.ToString() + " ou " + dataGridMatiereP.Rows[i].Cells[3].Value.ToString() + " de la lign " + (i + 1).ToString() + "  est invalide");



                }
                for (int i = 0; i < DataSet1.Tables["MatiereP"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["MatiereP"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from Matiere_premiere where codeMP =" + DataSet1.Tables["MatiereP"].Rows[i][0, DataRowVersion.Original].ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["MatiereP"].Clear();
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], nomFournisseur [Fournisseur] , UniteE [Unité de mesure] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF inner join Etat on Etat.idE = Matiere_premiere.idE", cn);
                dataAdapter.Fill(DataSet1, "MatiereP");

                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");
                cn.Close();

                //histoA
                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                for (int i = 0; i < dataGrideHistoriqueA.Rows.Count; i++)
                {
                    b = false;
                    b2 = false;
                    nom = false;

                    if (dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().Contains("AG"))
                    {
                        cmd = new SqlCommand("update historique_achat set idMP = NULL, idEM = @b , RCF = (select RCF from Fournisseur where nomFournisseur = @c),qteA = @d ,prix = @e , dateA = @f where numAchat = @a", cn);
                        cmd.Parameters.AddWithValue("@b", dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().Remove(0, 2));
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
                        cmd = new SqlCommand("update historique_achat set idEM = NULL, idMP = (select idmp from Matiere_premiere where codeMP = @b) , RCF = (select RCF from Fournisseur where nomFournisseur = @c),qteA = @d ,prix = @e , dateA = @f where numAchat = @a", cn);
                        cmd.Parameters.AddWithValue("@b", dataGrideHistoriqueA.Rows[i].Cells[1].Value);
                        for (int k = 0; k < DataSet1.Tables["MPPure"].Rows.Count; k++)
                        {
                            if (dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString() == DataSet1.Tables["MPPure"].Rows[k][0].ToString())
                            {
                                b2 = true;
                                break;
                            }
                        }
                        for (int k = 0; k < DataSet1.Tables["MPPure"].Rows.Count; k++)
                        {
                            if (dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString().ToLower() == DataSet1.Tables["MPPure"].Rows[k][1].ToString().ToLower())
                            {
                                nom = true;
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
                    if (!b2 && nom)
                        //hna ghi mal9it manktb 3liha drt nom = true , ghi bach maidozch l dok les ifs lokhrin
                        nom = true;
                    else if (b && b2)
                    {
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            msgbx = false;
                        }
                    }
                    else if (!b && !b2)
                        MessageBox.Show("la valeur '" + dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString() + "' et '" + dataGrideHistoriqueA.Rows[i].Cells[2].Value.ToString() + "' d'achat numero : " + dataGrideHistoriqueA.Rows[i].Cells[0].Value.ToString() + " est invalide");
                    else if (!b)
                        MessageBox.Show("la valeur '" + dataGrideHistoriqueA.Rows[i].Cells[2].Value.ToString() + "' d'achat numero : " + dataGrideHistoriqueA.Rows[i].Cells[0].Value.ToString() + " est invalide");
                    else if (!b2)
                        MessageBox.Show("la valeur '" + dataGrideHistoriqueA.Rows[i].Cells[1].Value.ToString() + "' d'achat numero : " + dataGrideHistoriqueA.Rows[i].Cells[0].Value.ToString() + " est invalide");
                }

                for (int i = 0; i < DataSet1.Tables["HistoriqueA"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["HistoriqueA"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from historique_achat where numAchat = " + DataSet1.Tables["HistoriqueA"].Rows[i][0, DataRowVersion.Original].ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["HistoriqueA"].Clear();
                dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(Matiere_premiere.nomMP,'') + ISNULL('AG' +CONVERT(varchar(50),Emballage.idEM) ,'') [Nom de Produit], Fournisseur.nomFournisseur [Nom Fournisseur], qteA [Quantite] , prix [Prix] ,dateA [Date Achat] from historique_achat left join Matiere_premiere on  historique_achat.idMP = Matiere_premiere.idmp  left join Emballage on   historique_achat.idEM = Emballage.idEM inner join Fournisseur on Fournisseur.RCF = Matiere_premiere.RCF or Fournisseur.RCF = Emballage.RCF", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueA");
                cn.Close();
                //histoV

                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                for (int i = 0; i < dataGrideHistoriqueV.Rows.Count; i++)
                {
                    b2 = false;
                    b = false;
                    String[] str = dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString().Split(' ');
                    if (str.Length != 2)
                    {
                        MessageBox.Show("le Nom de produit de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString() + " est incorrect , veuillez respecter la syntaxe d'insertion 'Nom'+ espace + 'Code'");
                        break;
                    }
                    cmd = new SqlCommand("update historique_vente set  idPF = (select idPF from Produit_finis where nomPF = @b and codePF = @c) , RC_CIN = (select RC_CIN from Client where nomClt = @d) , qteV = @e , prix = @f , dateV  = @j where numVente = @a", cn);
                    cmd.Parameters.AddWithValue("@a", dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                    cmd.Parameters.AddWithValue("@b", str[0]);
                    cmd.Parameters.AddWithValue("@c", str[1]);
                    cmd.Parameters.AddWithValue("@d", dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString());
                    cmd.Parameters.AddWithValue("@e", dataGrideHistoriqueV.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@f", dataGrideHistoriqueV.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@j", dataGrideHistoriqueV.Rows[i].Cells[5].Value);

                    for (int k = 0; k < DataSet1.Tables["Produit_finis"].Rows.Count; k++)
                    {
                        if (dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString().ToLower() == DataSet1.Tables["Produit_finis"].Rows[k][0].ToString().ToLower())
                        {
                            b2 = true;
                            break;
                        }
                    }

                    for (int k = 0; k < DataSet1.Tables["Client"].Rows.Count; k++)
                    {
                        if (dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString().ToLower() == DataSet1.Tables["Client"].Rows[k][0].ToString().ToLower())
                        {
                            b = true;
                            break;
                        }
                    }


                    if (b & b2)
                    {
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            msgbx = false;
                        }
                    }
                    else if (!b && !b2)
                        MessageBox.Show("le Produit '" + dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString() + "' et le Client '" + dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString() + "' est introuvable de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                    else if (!b)
                        MessageBox.Show("le Client '" + dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString() + "' est introuvable de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                    else if (!b2)
                        MessageBox.Show("le Produit '" + dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString() + "' est introuvable de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                }

                for (int i = 0; i < DataSet1.Tables["HistoriqueV"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["HistoriqueV"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from historique_vente where numVente = " + DataSet1.Tables["HistoriqueV"].Rows[i][0, DataRowVersion.Original].ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["HistoriqueV"].Clear();
                dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueV");
                cn.Close();

                //ProdF
                msgbx = true;
                if (ConnectionState.Open == cn.State)
                {
                    cn.Close();
                }
                cn.Open();
                for (int i = 0; i < dataGrideHistoriqueV.Rows.Count; i++)
                {
                    b2 = false;
                    b = false;
                    String[] str = dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString().Split(' ');
                    if (str.Length != 2)
                    {
                        MessageBox.Show("le Nom de produit de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString() + " est incorrect , veuillez respecter la syntaxe d'insertion 'Nom'+ espace + 'Code'");
                        break;
                    }
                    cmd = new SqlCommand("update historique_vente set  idPF = (select idPF from Produit_finis where nomPF = @b and codePF = @c) , RC_CIN = (select RC_CIN from Client where nomClt = @d) , qteV = @e , prix = @f , dateV  = @j where numVente = @a", cn);
                    cmd.Parameters.AddWithValue("@a", dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                    cmd.Parameters.AddWithValue("@b", str[0]);
                    cmd.Parameters.AddWithValue("@c", str[1]);
                    cmd.Parameters.AddWithValue("@d", dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString());
                    cmd.Parameters.AddWithValue("@e", dataGrideHistoriqueV.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@f", dataGrideHistoriqueV.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@j", dataGrideHistoriqueV.Rows[i].Cells[5].Value);

                    for (int k = 0; k < DataSet1.Tables["Produit_finis"].Rows.Count; k++)
                    {
                        if (dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString().ToLower() == DataSet1.Tables["Produit_finis"].Rows[k][0].ToString().ToLower())
                        {
                            b2 = true;
                            break;
                        }
                    }

                    for (int k = 0; k < DataSet1.Tables["Client"].Rows.Count; k++)
                    {
                        if (dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString().ToLower() == DataSet1.Tables["Client"].Rows[k][0].ToString().ToLower())
                        {
                            b = true;
                            break;
                        }
                    }


                    if (b & b2)
                    {
                        cmd.ExecuteNonQuery();
                        if (msgbx)
                        {
                            msgbx = false;
                        }
                    }
                    else if (!b && !b2)
                        MessageBox.Show("le Produit '" + dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString() + "' et le Client '" + dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString() + "' est introuvable de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                    else if (!b)
                        MessageBox.Show("le Client '" + dataGrideHistoriqueV.Rows[i].Cells[2].Value.ToString() + "' est introuvable de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                    else if (!b2)
                        MessageBox.Show("le Produit '" + dataGrideHistoriqueV.Rows[i].Cells[1].Value.ToString() + "' est introuvable de Vente num : " + dataGrideHistoriqueV.Rows[i].Cells[0].Value.ToString());
                }

                for (int i = 0; i < DataSet1.Tables["HistoriqueV"].Rows.Count; i++)
                {
                    if (DataSet1.Tables["HistoriqueV"].Rows[i].RowState == DataRowState.Deleted)
                    {
                        cmd = new SqlCommand("delete from historique_vente where numVente = " + DataSet1.Tables["HistoriqueV"].Rows[i][0, DataRowVersion.Original].ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                }

                DataSet1.Tables["HistoriqueV"].Clear();
                dataAdapter = new SqlDataAdapter("select numVente [Num Vente],CONVERT(varchar(50),Produit_finis.nomPF)+' ' +CONVERT(varchar(50), codePF )[Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
                dataAdapter.Fill(DataSet1, "HistoriqueV");
                cn.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("quelque chose a mal tourné");
                return;
            }
        }

    }
}
