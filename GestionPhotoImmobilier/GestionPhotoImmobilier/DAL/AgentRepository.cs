using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.DAL
{
    public class AgentRepository : GenericRepository<Agent>
    {

        public AgentRepository(H15_PROJET_E03Entities context) : base(context) { }
        public Agent ObtenirAgentParID(int? id)
        {
            return GetByID(id);
        }
        public IEnumerable<Agent> ObtenirAgent()
        {
            return Get();
        }
        //public IEnumerable<Agent> ObtenirRdvsComplets()
        //{
        //    return Get(includeProperties: "Seance");
        //}

        public IEnumerable<Agent> ObtenirSeanceParAgentId(int? id)
        {
            return Get(filter: r => r.AgentId == id);
        }

        public IEnumerable<Agent> ObtenirAgentParAgence(int? id)
        {
            return Get(filter: a => a.AgenceId == id);
        }

        public void InsertAgent(Agent Agent) { Insert(Agent); }
        public void DeleteAgent(Agent Agent) { Delete(Agent); }
        public void UpdateAgent(Agent Agent) { Update(Agent); }
    }

}