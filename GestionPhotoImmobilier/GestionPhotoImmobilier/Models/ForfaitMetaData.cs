using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    [MetadataType(typeof(ForfaitMetaData))]

    public partial class Forfait { }
    public partial class ForfaitMetaData
    {
        [Required]
        public int ForfaitId { get; set; }
         [Required]
        public string Nom { get; set; }
        public string DescriptionForfait { get; set; }
         [Required]
        public string Prix { get; set; }
    }
}