//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionPhotoImmobilier.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Agent
    {
        public Agent()
        {
            this.Seances = new HashSet<Seance>();
        }
    
        public int AgentId { get; set; }
        public string Nom { get; set; }
        public Nullable<int> AgenceId { get; set; }
    
        public virtual Agence Agence { get; set; }
        public virtual ICollection<Seance> Seances { get; set; }
    }
}
