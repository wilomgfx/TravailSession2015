﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class H15_PROJET_E03Entities : DbContext
    {
        public H15_PROJET_E03Entities()
            : base("name=H15_PROJET_E03Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agence> Agences { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Propriete> Proprietes { get; set; }
        public virtual DbSet<Rdv> Rdvs { get; set; }
        public virtual DbSet<Forfait> Forfaits { get; set; }
        public virtual DbSet<Seance> Seances { get; set; }
    }
}
