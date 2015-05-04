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
        public int RdvId { get; set; }
        [DisplayName("Est confirmé")]
        public bool Confirmer { get; set; }
        public string Client { get; set; }
        public string Photographe { get; set; }
    }
}