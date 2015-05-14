using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.DAL
{
    public class AgenceRepository : GenericRepository<Agence>
    {
        public AgenceRepository(H15_PROJET_E03Entities context) : base(context) { }
        //public AgenceRepository(GestionPhotoImmobilierEntities1 context) : base(context) { }
        
        public Agence ObtenirAgenceParID(int? id)
            {
                return GetByID(id);
            }
        public IEnumerable<Agence> ObtenirAgence()
            {
                return Get();
            }

        public IEnumerable<Agence> ObtenirAgenceComplets()
            {
                return Get(includeProperties: "Agent");
            }


        public void InsertAgence(Agence Agence) { Insert(Agence); }
        public void DeleteAgence(Agence Agence) { Delete(Agence); }
        public void UpdateAgence(Agence Agence) { Update(Agence); }
    }
}