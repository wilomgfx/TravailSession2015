using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GestionPhotoImmobilier.DAL;
using GestionPhotoImmobilier.Models;
using GestionPhotoImmobilier.RegleDaffaire;

namespace GestionPhotoImmobilier.Models
{
    public partial class Seance : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return DateEntreUnEt15JoursPlusTard.Validate(this);
            yield return RDVUnique.Validate(this);

            yield return Validation4hEntreRendezVous.Validate(this);
        }
    }
}