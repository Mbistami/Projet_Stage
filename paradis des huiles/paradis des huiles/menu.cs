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

            dataAdapter = new SqlDataAdapter("select numAchat [Num Achat], ISNULL(nomMP, '') + ISNULL(historique_achat.idEM + Emballage.idE, '') [Nom Produit], ISNULL(nomFournisseur, '') + ISNULL(nomFournisseur, '') [Fournisseur], qtea [Quantite], prix  [Prix], dateA [Date Achat]from historique_achat inner join Matiere_premiere on historique_achat.idMP=Matiere_premiere.idmp/* or */ inner join Emballage on Emballage.idEM = historique_achat.idEM inner join Fournisseur on Fournisseur.RCF = Emballage.RCF  or  Fournisseur.RCF=Matiere_premiere.RCF", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueA");

            dataAdapter = new SqlDataAdapter("select numVente [Num Vente], Produit_finis.idPF + codePF [Nom Produit], nomClt  [Nom Client], qteV [Quantite], prix [Prix] , dateV [Date Vente] from Historique_vente inner join Produit_finis on Produit_finis.idPF = Historique_vente.idPF inner join Client on Historique_vente.RC_CIN = Client.RC_CIN", cn);
            dataAdapter.Fill(DataSet1, "HistoriqueV");

            dataAdapter = new SqlDataAdapter("Select * from Etat", cn);
            dataAdapter.Fill(DataSet1, "Etat");

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

            
            //fournisseures names in MP_ADDING
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
            this.Labelcount.Text = "Nombre de client : " + dataGridClient.Rows.Count;
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
            DataView dataView = new DataView(DataSet1.Tables["HistoriqueV"]);
            dataView.RowFilter = "[" + CmbAfficherHistVfind.SelectedItem.ToString() + "]like '%" + TxtAffHistVfind.Text + "%'";
            dataGridEmballage.DataSource = dataView;
        }

        int fdl; //fidele val
        private void BtnAddClt_Click(object sender, EventArgs e)
        {
            try
            {
                //Ajouter mode Déco using dataset
                int f;
                if (cmbtypecltadd.SelectedItem.ToString() == "fidéle") { f = 1; } else { f = 0; }
                DataSet1.Tables["Client"].Rows.Add(TxtNomaddclt.Text , txtaddnumclt.Text, txtaddadressclt.Text, txtaddmailclt.Text,f , txtaddrcclt.Text);
                dataAdapter = new SqlDataAdapter("select nomClt as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);
                cn.Open();
                dataAdapter.Update(DataSet1.Tables["Client"]);
                cn.Close();
                /*cn.Open();
                SqlCommand cmd = new SqlCommand("insert Client values (@aclient, @bclient, @dclient, @eclient, @fclient, @gclient)", cn);
                SqlParameter p1 = new SqlParameter("@aclient", txtaddrcclt.Text);
                SqlParameter p2 = new SqlParameter("@bclient", TxtNomaddclt.Text);
                SqlParameter p4 = new SqlParameter("@dclient", txtaddnumclt.Text);
                SqlParameter p5 = new SqlParameter("@eclient", txtaddmailclt.Text);
                SqlParameter p6 = new SqlParameter("@fclient", txtaddadressclt.Text);
                if (cmbtypecltadd.SelectedItem.ToString() == "fidele")
                {
                    fdl = 1;
                }
                else
                {
                    fdl = 0;
                }
                SqlParameter p7 = new SqlParameter("@gclient", fdl);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.ExecuteNonQuery();
                cn.Close();

                //clear txtbox
                txtaddrcclt.Text = "";
                TxtNomaddclt.Text = "";
                txtaddnumclt.Text = "";
                txtaddmailclt.Text = "";
                txtaddmailclt.Text = "";
                txtaddadressclt.Text = "";
                TxtNomaddclt.Focus();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TxtModifClientfind_TextChanged(object sender, EventArgs e)
        {
            
        }

        int indexdatamodif;

        private void DatagridModifClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        //val fidelite modif
        int mfid;
        private void BtnModifierclt_Click(object sender, EventArgs e)
        {
            try
            {
                //dataAdapter = new SqlDataAdapter("select nomClt as NomClient, numTel [Num Tel] , adresse Adresse , email Mail, fidelite as Type , RC_CIN [RC / CIN] from Client ", cn);
                //commandBuilder = new SqlCommandBuilder(dataAdapter);
                MessageBox.Show(DataSet1.Tables["Client"].Rows[dataGridClient.SelectedRows[0].Index][dataGridClient.SelectedColumns[0].Index].ToString());
                /*cn.Open();
                SqlCommand cmd = new SqlCommand("update client set RC_CIN = @Maclient, nomClt = @Mbclient , numTel = @Mdclient, email = @Meclient, adresse = @Mfclient, fidelite = @Mg where RC_CIN = @Rc", cn);
                SqlParameter p1 = new SqlParameter("@Maclient", txtrccltmodif.Text);
                SqlParameter p2 = new SqlParameter("@Mbclient", txtnomcltmodif.Text);
                SqlParameter p3 = new SqlParameter("@Mdclient", txtnumcltmodif.Text);
                SqlParameter p4 = new SqlParameter("@Meclient", txtmailcltmodif.Text);
                SqlParameter p5 = new SqlParameter("@Mfclient", txtadressecltmodif.Text);
                if (cmbtypecltmodife.SelectedItem.ToString() == "fidele")
                {
                    mfid = 1;
                }
                else
                    mfid = 0;
                SqlParameter p6 = new SqlParameter("@Mg", mfid);
                SqlParameter pselect = new SqlParameter("@Rc", DatagridModifClient.Rows[indexdatamodif].Cells[5].Value.ToString());
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(pselect);
                cmd.ExecuteNonQuery();
                cn.Close();

                txtrccltmodif.Text = "";
                txtnomcltmodif.Text = "";
                txtnumcltmodif.Text = "";
                txtmailcltmodif.Text = "";
                txtadressecltmodif.Text = "";
                txtnomcltmodif.Focus();*/
            }
            catch (Exception ex)
            {
            }
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
            if(open.ShowDialog() == DialogResult.OK)
            {
                pictureBoxEM.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxEM.Image = new Bitmap(open.FileName);
                FilePathEM.Text = open.FileName;
            }
        }

        private void btnAddEmballage_Click(object sender, EventArgs e)
        {
            /*try
            {
                dataAdapter = new SqlDataAdapter("select codeEM [Code Emballage], RCF [RCsF] , qteEM [Quantite] , idE [idE] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage ", cn);
                cn.Open();
                dataAdapter.Fill(DataSet1, "EmballagePure");
                cn.Close();
                //DataSet1.Tables["EmballagePure"].Rows.Add(txtAddCodeEmballage.Text, cmbAddNomFournisseurEmballage.SelectedValue.ToString(), txtAddquantiteEmballage.Text, cmbAddTypeEmballge.SelectedValue.ToString(), txtAddSupportEmballage.Text, txtAddDescriptionEmballage.Text, "E2S2");
                //SqlCommand cmd = new SqlCommand("update emballage set img=(SELECT BulkColumn FROM Openrowset( Bulk '"+this.FilePathEM.Text+"', Single_Blob) as img) where codeEM='" + txtAddCodeEmballage.Text + "'", cn);
                commandBuilder = new SqlCommandBuilder(dataAdapter);

                cn.Open();
                dataAdapter.Update(DataSet1.Tables["EmballagePure"]);
                cmd.ExecuteNonQuery();
                dataAdapter = new SqlDataAdapter("select codeEM [Code Emballage], nomFournisseur [Nom Fournisseur] , qteEM [Quantite] , UniteE [Unité de mesure] , supportEM[Support] , descEM [Description] , cordoEM [Coordonnée] from Emballage inner join Fournisseur on Emballage.RCF = Fournisseur.RCF inner join Etat on Etat.idE = Emballage.idE", cn);
                DataSet1.Tables["Emballage"].Clear();
                dataAdapter.Fill(DataSet1, "Emballage");
                cn.Close();
                MessageBox.Show("Ajouter avec succes", "Succes!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }*/
        }

        private void batnAddMatiereP_Click(object sender, EventArgs e)
        {
            try
            {
                //Etage Salle non ajouter /!\
                dataAdapter = new SqlDataAdapter("select codeMP [Code], nomMP [Nom], RCF [RCF] , idE [idE] , qteMP [Quantite] , descMP [Description], cordoMP [Cordonnée] from Matiere_premiere ", cn);
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
            dataAdapter = new SqlDataAdapter("select RCF [RCF], qteA [Quantité], prix [prix],dateA [Date d'achat],idEM ,idMP from historique_achat", cn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            cn.Open();
            dataAdapter.Fill(DataSet1, "Historique_AchatPure");
            cn.Close();
            //DataSet1.Tables["Historique_AchatPure"].Rows.Add(cmbAddHabituelHistorA.SelectedValue.ToString(),txtquantiteaddhistoriquedachat.Text ,prixaddhistoriquedachat.Text ,dateventeaddhistoriquedachat.Value.ToString())
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
            DialogResult result = MessageBox.Show("Voulez-Vous supprimer le client de la base de donnée","Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGridClient.Rows.RemoveAt(dataGridClient.SelectedRows[0].Index);
                MessageBox.Show("Suppression effectuer! Veuillez Sauvgardez les modifications.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void dataGridClient_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowindex = dataGridClient.HitTest(e.X, e.Y).RowIndex;
                if(rowindex >= 0)
                {
                    ClickDroitMouse.Show(dataGridClient, new Point(e.X, e.Y));
                    dataGridClient.ClearSelection();
                    dataGridClient.Rows[rowindex].Selected = true;
                }
            }
        }

        private void dataGridClient_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            this.Labelcount.Text = "Nombre de client : " + dataGridClient.Rows.Count;
        }

        private void ClickDroirMouseVA_Click(object sender, EventArgs e)
        {
            TxtAffHistVfind.Text = dataGridClient.SelectedRows[0].Cells[0].Value.ToString();
            tabControl1.SelectedIndex = 5;
        }

        private void ClickDroitMouseModifier_Click(object sender, EventArgs e)
        {
            if (ClickDroitMouseModifier.Checked)            
                dataGridClient.ReadOnly = false;
            else
                dataGridClient.ReadOnly = true;
        }
    }
}
