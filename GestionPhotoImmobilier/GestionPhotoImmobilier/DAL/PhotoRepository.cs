using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.DAL
{
    public class PhotoRepository:GenericRepository<Photo>
    {
        public PhotoRepository(H15_PROJET_E03Entities context) : base(context) { }
        //public PhotoRepository(GestionPhotoImmobilierEntities1 context) : base(context) { }


        public IEnumerable<Photo> ObtenirPhoto()
        {
            return Get();
        }
        public Photo ObtenirPhotoParID(int? id)
        {
            return GetByID(id);
        }

        public IEnumerable<Photo> ObtenirPhotosComplets()
        {
            return Get(includeProperties: "Propriete");
        }


        public void InsertPhoto(Photo Photo) { Insert(Photo); }
        public void DeletePhoto(Photo Photo) { Delete(Photo); }
        public void UpdatePhoto(Photo Photo) { Update(Photo); }
    }
}