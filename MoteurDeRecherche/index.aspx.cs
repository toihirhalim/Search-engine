using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoteurDeRecherche
{
    public partial class index : System.Web.UI.Page
    {
        DCDataContext db = new DCDataContext();
        protected static string nbrResultat = "Essayer avec : <strong>internet, etudiant</strong> ... ou autre", prev = "", next = "";
        protected static int begin = 0;
        protected static string[,] results = new string[,] { { "", "" }, { "", "" }, { "", "" }, { "", "" }, { "", "" }, { "", "" }, };
        protected static Recherche[] recherches ;
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void search_Click(object sender, EventArgs e)
        {
            clear();
            if (txtBox.Text != "" && Search(txtBox.Text))
            {
                optimiser();
                afficher();
            }
            else if (txtBox.Text != "")
            {
                nbrResultat += " !! essayer avec : <strong>internet, etudiant, collège, aliment, thé, arbre, sommet ou foret</strong>";
                nbrResultat += "<br>ou voir sur <a href = \"https://www.google.com/search?q=" + txtBox.Text + "\" target = \"blanck\" >Google</a>";
                nbrResultat += " ou sur <a href = \"https://www.youtube.com/results?search_query=" + txtBox.Text + "\" target = \"blanck\" >Youtube</a>.";
            }
        }
         
        //recherche d un mot
        protected Boolean Search(string mot)
        {
            Boolean trouvé = false; 
            int max = db.Recherche.Count(x => x.Mot.Texte == mot);
            recherches = new Recherche[max];
            int i = 0;
            
            foreach (Recherche r in db.Recherche)
            {
                if (r.Mot.Texte == mot)
                {
                    recherches[i] = r;
                    i++;
                    trouvé = true;
                }
            }
            nbrResultat = i + " resultat pour " + mot;
            return trouvé;
        }

        //optimiser les resultats du recherche 
        protected void optimiser()
        {
            for(int i = 0; i < recherches.Length; i++)
            {
                for (int j = 0; j < recherches.Length; j++)
                {
                    int nbrI = db.Historique.Count(x => x.Recherche == recherches[i]);
                    int nbrJ = db.Historique.Count(x => x.Recherche == recherches[j]);

                    // comparaison pas nombre de fois cliqué
                    if (nbrJ < nbrI)
                    {
                        Recherche tmp = recherches[i];
                        recherches[i] = recherches[j];
                        recherches[j] = tmp;
                    }
                    //comparaison par le plus recent
                    else if (nbrI == nbrJ && nbrI != 0)
                    {
                        //dernier historique
                        Historique histI = new Historique(), histJ = new Historique();
                        foreach (Historique hist in db.Historique)
                        {
                            if (hist.Recherche == recherches[i]) histI = hist;
                            if (hist.Recherche == recherches[j]) histJ = hist;
                        }
                        DateTime dt1 = getDate(histI);
                        DateTime dt2 = getDate(histJ);
                        if (DateTime.Compare(dt1, dt2) > 0)
                        {
                            Recherche tmp = recherches[i];
                            recherches[i] = recherches[j];
                            recherches[j] = tmp;
                        }
                    }
                }
            }
        }

        //affiche des resultat
        protected void afficher()
        {
            int i = 0;
            results = new string[,] { { "", "" }, { "", "" }, { "", "" }, { "", "" }, { "", "" }, { "", "" }, };

            for (int j = begin; j< recherches.Length && j < begin + 6; j++)
            {
                if(recherches[j] != null)
                {
                    results[i, 0] = recherches[j].Lien.UrlText;
                    results[i, 1] = recherches[j].Lien.UrlTitle;
                    i++;
                }
            }
            if (recherches.Length > (begin + 6)) next = "next";
            if (recherches.Length <= (begin + 6)) next = "";
            if (begin > 0) prev = "prev";
            if (begin == 0) prev = "";
        }

        //ajout d un recherche a l historique
        protected void addhist(int indexSearch)
        {
            indexSearch += begin;
            Recherche existRecherche = db.Recherche.SingleOrDefault(x => x == recherches[indexSearch]);
            if (existRecherche != null)
            {
                Historique historique = new Historique();
                historique.Recherche = existRecherche;
                historique.DateHist = DateTime.Now;
                db.Historique.InsertOnSubmit(historique);
                db.SubmitChanges();
            }
        }

        //click des liens
        protected void link0Clicked(object sender, EventArgs e)
        {
            var Url = results[0, 0];
            System.Diagnostics.Process.Start(Url);
            addhist(0);
        }
        protected void link1Clicked(object sender, EventArgs e)
        {
            var Url = results[1, 0];
            System.Diagnostics.Process.Start(Url);
            addhist(1);
        }
        protected void link2Clicked(object sender, EventArgs e)
        {
            var Url = results[2, 0];
            System.Diagnostics.Process.Start(Url);
            addhist(2);
        }
        protected void link3Clicked(object sender, EventArgs e)
        {
            var Url = results[3, 0];
            System.Diagnostics.Process.Start(Url);
            addhist(3);
        }
        protected void link4Clicked(object sender, EventArgs e)
        {
            var Url = results[4, 0];
            System.Diagnostics.Process.Start(Url);
            addhist(4);
        }
        protected void link5Clicked(object sender, EventArgs e)
        {
            var Url = results[5, 0];
            System.Diagnostics.Process.Start(Url);
            addhist(5);
        }
        protected void Prev(object sender, EventArgs e)
        {
            begin -= 6;
            afficher();
        }
        protected void Next(object sender, EventArgs e)
        {
            begin += 6;
            afficher();
        }

        //date d un historique
        protected DateTime getDate(Historique hist)
        {
            int year = hist.DateHist.Value.Year;
            int mois = hist.DateHist.Value.Month;
            int day = hist.DateHist.Value.Day;
            int heure = hist.DateHist.Value.Hour;
            int min = hist.DateHist.Value.Minute;
            int sec = hist.DateHist.Value.Second;
            DateTime dt = new DateTime(year, mois, day, heure, min, sec);
            return dt;
        }

        //clear everything
        protected void clear()
        {
            recherches = new Recherche[6];
            results = new string[,] { { "", "" }, { "", "" }, { "", "" }, { "", "" }, { "", "" }, { "", "" }, }; 
            nbrResultat = "";
            begin = 0;
        }
    }
}