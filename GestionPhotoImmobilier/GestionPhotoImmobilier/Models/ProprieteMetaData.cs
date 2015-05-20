using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    [MetadataType(typeof(ProprieteMetaData))]
    public partial class Propriete { }
    public class ProprieteMetaData
    {
        [Required]
        public int ProprieteId { get; set; }
        [Required]
        public string Client { get; set; }
        [Required]
        [MinLength(8)]
        public string Adresse { get; set; }
        [Required]
        [MinLength(8)]
        public string Ville { get; set; }
    }
}