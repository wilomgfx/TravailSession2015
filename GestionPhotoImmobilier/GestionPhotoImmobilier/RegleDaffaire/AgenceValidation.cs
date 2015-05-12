using GestionPhotoImmobilier.RegleDaffaire;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.Models
{
    public partial class Agence : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return phoneValidation.Validate(this);
        }
    }
}