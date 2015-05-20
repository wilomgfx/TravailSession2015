using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    [MetadataType(typeof(AgentMetaData))]

    public partial class Agent { }
    public partial class AgentMetaData
    {
        public int AgentId { get; set; }
        [Required]
        public string Nom { get; set; }
    }
}