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

            return Get(filter: s => s.SeanceId == idVrai, includeProperties: "Rdvs").First();
        }

        public Seance ObtenirSeanceParID(int? id)
        {
            return GetByID(id);
        }

        public void InsertSeance(Seance Seance) { Insert(Seance); }
        public void DeleteSeance(Seance Seance) { Delete(Seance); }
        public void UpdateSeance(Seance Seance) { Update(Seance); }


    }
}