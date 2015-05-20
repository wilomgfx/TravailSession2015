using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    [MetadataType(typeof(RdvMetaData))]
    public partial class Rdv { }

    public partial class RdvMetaData
    {
        [Required]
        public int RdvId { get; set; }
        [DisplayName("Est confirmé")]
        [Required]
        public bool Confirmer { get; set; }
        [Required]
        public string Client { get; set; }
        [Required]
        public string Photographe { get; set; }
    }
}