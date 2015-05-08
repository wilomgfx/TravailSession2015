using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionPhotoImmobilier.Models;

namespace GestionPhotoImmobilier.ViewModels
{
    public class SommaireSeance
    {
        public SeanceRdv SeanceRdv { get; set; }
        public Agent Agent { get; set; }
        public Propriete Propriete { get; set; }
    }
}