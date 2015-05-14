using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.ViewModels
{
    public class SommairePhoto
    {
        public ICollection<Photo> Photos { get; set; }
        public int SeanceId { get; set; }
    }
}