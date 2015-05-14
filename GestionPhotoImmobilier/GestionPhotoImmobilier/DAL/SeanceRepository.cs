using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionPhotoImmobilier.Models;

namespace GestionPhotoImmobilier.DAL
{
    public class SeanceRepository : GenericRepository<Seance>
    {
        public SeanceRepository(H15_PROJET_E03Entities context) : base(context) { }
        //public SeanceRepository(GestionPhotoImmobilierEntities1 context) : base(context) { }

        public IEnumerable<Seance> ObtenirSeance()
        {
            return Get();
        }

        public Seance ObtenirSeanceComplete(int? id)
        {
            int idVrai = 0;
            if (id == null)
                return null;
            else
            {
                string parsed = id.ToString();
                idVrai = int.Parse(parsed);
            }

            return Get(filter: s => s.SeanceId == idVrai, includeProperties: "Rdvs,Agent,Propriete").First();
        }

        public Seance ObtenirSeanceParID(int? id)
        {
            return GetByID(id);
        }

        public IEnumerable<Seance> ObtenirSeanceFutures(DateTime date)
        {
            return Get(filter: s => s.DateSeance > date, includeProperties: "Rdvs");
        }


        public IEnumerable<Seance> ObtenirSeanceParForfait(int? id)
        {
            return Get(filter: s => s.ForfaitId == id);
        }

        public IEnumerable<Seance> ObtenirSeanceParAgent(int? id)
        {
            return Get(filter: s => s.AgentId == id);
        }


        public double? ObtenirPrixSeance(int? id)
        {
            if (id != null)
            {


                double result = 0;

                H15_PROJET_E03Entities dataContext = new H15_PROJET_E03Entities();

                var query = from Seances in dataContext.Seances
                            where Seances.SeanceId == id
                            select Seances.Forfait.Prix;
                string prixSeance = "";
                foreach (var asfa in query)
                {
                    prixSeance = asfa.ToString();
                }

                if (!double.TryParse(prixSeance, out result))
                {
                    return null;
                }

                return result;
            }
            return null;

        }
        public double? ObtenirPrixCoutTps(int? id)
        {

            if (id != null)
            {
                double? result = ObtenirPrixSeance(id);

                if (ObtenirCoutExtra(id) != null)
                {
                    result += ObtenirCoutExtra(id);
                }

                decimal Tps = 0.05m;

                if (result == null)
                {
                    return null;
                }
                decimal resultDec = (decimal)result;
                decimal resultArrondi = Math.Round((resultDec * Tps), 2);
                result = (double?)resultArrondi;
                return result;
            }


            return null;

        }
        public double? ObtenirPrixCoutTvq(int? id)
        {

            if (id != null)
            {
                double? result = (double)ObtenirPrixSeance(id);

                if (ObtenirCoutExtra(id) != null)
                {
                    result += ObtenirCoutExtra(id);
                }

                decimal TVQ = 0.09975m;

                if (result == null)
                {
                    return null;
                }
                decimal resultDec = (decimal)result;
                decimal resultArrondi = Math.Round((resultDec * TVQ), 2);
                result = (double?)resultArrondi;
                return result;
            }


            return null;

        }
        public double? ObtenirCoutTotal(int? id)
        {

            if (id != null)
            {
                double? result = ObtenirPrixSeance(id);
                if (result != null)
                {



                    double coutExtra = 0.0;
                    if (ObtenirExtra(id) != null)
                    {
                        if (ObtenirExtra(id).Length > 0)
                        {
                            for (int index = 0; index < ObtenirExtra(id).Length; index++)
                            {
                                coutExtra += double.Parse(ObtenirExtra(id)[index].Substring(ObtenirExtra(id)[index].IndexOf('$')+1));
                            }
                        }
                    }

                    result = ObtenirPrixSeance(id) + ObtenirPrixCoutTvq(id) + ObtenirPrixCoutTps(id) + coutExtra;
                    return result;
                }
                else
                {
                    return null;
                }
            }
            return null;

        }

        public double? ObtenirCoutExtra(int? id)
        {

            double coutExtra = 0.0;
            if (ObtenirExtra(id) != null)
            {
                if (ObtenirExtra(id).Length > 0)
                {
                    for (int index = 0; index < ObtenirExtra(id).Length; index++)
                    {
                        string[] tabExtra = ObtenirExtra(id);
                        int indexDollar = (ObtenirExtra(id)[index].IndexOf('$'));
                        string asd = ObtenirExtra(id)[index].Substring(indexDollar+1);
                        coutExtra += double.Parse(ObtenirExtra(id)[index].Substring(indexDollar+1));
                    }
                    return coutExtra;
                }
            }


            return null;
        }


        public string[] ObtenirExtra(int? id)
        {

            H15_PROJET_E03Entities dataContext = new H15_PROJET_E03Entities();

            var query = from Seances in dataContext.Seances
                        where Seances.SeanceId == id
                        select Seances.Extras;
            string[] tabExtra;

            if (query != null)
            {


                foreach (var extra in query)
                {
                    if (extra != null)
                    {
                        tabExtra = extra.Split('|');
                        return tabExtra;
                    }
                    
                }
            }
            return null;

        }
        /*public IEnumerable<Seance> obtenirSeancsRechercheSearch(string recherche)
        {
            IEnumerable<Seance> seances;
            seances = (!String.IsNullOrEmpty(recherche)) ? Get(filter: s => (s.Photographe.Contains(recherche.ToUpper())) || (s.FirstName.ToUpper().Contains(recherche.ToUpper()))) : Get();)
        }*/

        public void InsertSeance(Seance Seance) { Insert(Seance); }
        public void DeleteSeance(Seance Seance) { Delete(Seance); }
        public void UpdateSeance(Seance Seance) { Update(Seance); }


    }
}