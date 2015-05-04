using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    public partial class RdvMetaData
    {
        public int RdvId { get; set; }
        [DisplayName("Est confirmé")]
        public Nullable<bool> Confirmer { get; set; }
        public string Client { get; set; }
        public string Photographe { get; set; }
    }
}