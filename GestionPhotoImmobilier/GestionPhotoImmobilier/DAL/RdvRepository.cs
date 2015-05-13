using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.DAL
{
    public class RdvRepository : GenericRepository<Rdv>
    {
        //public RdvRepository(H15_PROJET_E03Entities context) : base(context) { }
        public RdvRepository(GestionPhotoImmobilierEntities1 context) : base(context) { }
        public IEnumerable<Rdv> ObtenirRdv()
        {
            return Get();
        }
        public Rdv ObtenirRdvParID(int? id)
        {
            return GetByID(id);
        }

        public IEnumerable<Rdv> ObtenirRdvsComplets()
        {
            return Get(includeProperties: "Seance");
        }

        public IEnumerable<Rdv> ObtenirRdvDeLaSeance(int? id)
        {
            return Get(filter: r => r.SeanceId == id);
        }

        public void InsertRdv(Rdv Rdv) { Insert(Rdv); }
        public void DeleteRdv(Rdv Rdv) { Delete(Rdv); }
        public void UpdateRdv(Rdv Rdv) { Update(Rdv); }


    }
}