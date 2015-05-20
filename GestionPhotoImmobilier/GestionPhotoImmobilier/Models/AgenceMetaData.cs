using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    [MetadataType(typeof(AgenceMetaData))]

    public partial class Agence { }
    public partial class AgenceMetaData
    {
        [Required]
        public int AgenceId { get; set; }
         [Required]
        public string Nom { get; set; }
         [Required]
         [MinLength(8)]
        public string Adresse { get; set; }
         [Required]
        public string NumTel { get; set; }
    }
}