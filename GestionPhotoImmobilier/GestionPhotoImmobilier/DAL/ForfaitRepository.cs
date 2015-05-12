using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionPhotoImmobilier.Models;



namespace GestionPhotoImmobilier.DAL
{
    public class ForfaitRepository : GenericRepository<Forfait>
    {
        public ForfaitRepository(H15_PROJET_E03Entities context) : base(context) { }
        public Forfait ObtenirForfaitParID(int? id)
        {
            return GetByID(id);
        }
        public IEnumerable<Forfait> ObtenirForfait()
        {
            return Get();
        }
        public void InsertAgent(Forfait Forfait) { Insert(Forfait); }
        public void DeleteAgent(Forfait Forfait) { Delete(Forfait); }
        public void UpdateAgent(Forfait Forfait) { Update(Forfait); }




    }
}