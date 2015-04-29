using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionPhotoImmobilier.Models;

namespace GestionPhotoImmobilier.DAL
{
    public class SeanceRepository : GenericRepository<Seance>
    {
        public SeanceRepository(GestionPhotoImmobilierEntities context) : base(context) { }

        public IEnumerable<Seance> ObtenirSeance()
        {
            return Get();
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