﻿using GestionPhotoImmobilier.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    public class RdvRepository : GenericRepository<Rdv>
    {
        public RdvRepository(GestionPhotoImmobilierEntities context) : base(context) { }

        public IEnumerable<Rdv> ObtenirRdv()
        {
            return Get();
        }
        public Rdv ObtenirRdvParID(int? id)
        {
            return GetByID(id);
        }

        public void InsertRdv(Rdv Rdv) { Insert(Rdv); }
        public void DeleteRdv(Rdv Rdv) { Delete(Rdv); }
        public void UpdateRdv(Rdv Rdv) { Update(Rdv); }
    
    }
}