using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.DAL
{
    public class ProprieteRepository:GenericRepository<Propriete>
    {
        public ProprieteRepository(H15_PROJET_E03Entities context) : base(context) { }
        //public ProprieteRepository(GestionPhotoImmobilierEntities1 context) : base(context) { }

        public IEnumerable<Propriete> ObtenirPropriete()
        {
            return Get();
        }
        public Propriete ObtenirProprieteParID(int? id)
        {
            return GetByID(id);
        }

        public void InsertPropriete(Propriete Propriete) { Insert(Propriete); }
        public void DeletePropriete(Propriete Propriete) { Delete(Propriete); }
        public void UpdatePropriete(Propriete Propriete) { Update(Propriete); }


    }
 }
