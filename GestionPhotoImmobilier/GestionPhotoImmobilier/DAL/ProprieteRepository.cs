using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public void DeleteProprieteEtPhoto(Propriete Propriete)
        {
                var id = Propriete.ProprieteId;
           
                var proprieteID = new SqlParameter("@proprieteID", id);
                var proprieteID2 = new SqlParameter("@proprieteID", id);
               // Propriete propriete = context.Database.SqlQuery<Propriete>("Select * from Proprietes.Propriete Where ProprieteID =@proprieteID", idParam).FirstOrDefault<Propriete>();

                var deletePhotosPropriete = "DELETE FROM Proprietes.Photo WHERE ProprieteId = @proprieteID";
                context.Database.ExecuteSqlCommand(deletePhotosPropriete, proprieteID);

                var deletePropriete = "DELETE FROM Proprietes.Propriete WHERE ProprieteId = @proprieteID";
                context.Database.ExecuteSqlCommand(deletePropriete, proprieteID2);
            

                
        }

        public void InsertPropriete(Propriete Propriete) { Insert(Propriete); }
        public void DeletePropriete(Propriete Propriete) { Delete(Propriete); }
        public void UpdatePropriete(Propriete Propriete) { Update(Propriete); }


    }
 }
