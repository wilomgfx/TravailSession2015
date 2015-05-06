﻿using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.DAL
{
    public class AgentRepository : GenericRepository<Agent>
    {

        public AgentRepository(H15_PROJET_E03Entities context) : base(context) { }

        public IEnumerable<Agent> ObtenirRdv()
        {
            return Get();
        }
        public Agent ObtenirAgentParID(int? id)
        {
            return GetByID(id);
        }

        //public IEnumerable<Agent> ObtenirRdvsComplets()
        //{
        //    return Get(includeProperties: "Seance");
        //}

        public IEnumerable<Agent> ObtenirSeanceParAgentId(int? id)
        {
            return Get(filter: r => r.AgentId == id);
        }

        public void InsertRdv(Agent Agent) { Insert(Agent); }
        public void DeleteRdv(Agent Agent) { Delete(Agent); }
        public void UpdateRdv(Agent Agent) { Update(Agent); }
    }

}