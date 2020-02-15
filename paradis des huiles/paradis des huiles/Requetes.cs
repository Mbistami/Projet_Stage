using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paradis_des_huiles
{
    class Requetes
    {
        public static string rqtClient = "Select * from Client";
        public static string rqtFournisseur = "Select * from Fournisseur";
        public static string rqtEmballage = "Select * from Emballage";
        public static string rqtMatiereP = "Select * from matiere_premiere";
        public static string rqtProduitF = "Select * from produit_finis";
        public static string rqtHistoriqueA = "Select * from historique_achat";
        public static string rqtHistoriqueV = "Select * from historique_vente";
        public static string rqtEtat = "Select * from Etat";
    }
}
