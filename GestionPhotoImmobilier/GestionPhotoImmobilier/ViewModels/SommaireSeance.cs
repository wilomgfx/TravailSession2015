using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionPhotoImmobilier.Models;

namespace GestionPhotoImmobilier.ViewModels
{
    public class SommaireSeance
    {
        SeanceRdv SeanceRdv { get; set; }
        Agent Agent { get; set; }
        Propriete Propriete { get; set; }
    }
}