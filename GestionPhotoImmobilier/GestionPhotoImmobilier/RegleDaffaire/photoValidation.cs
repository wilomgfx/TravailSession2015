﻿using System;
using System.Collections.Generic;
using GestionPhotoImmobilier.RegleDaffaire;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GestionPhotoImmobilier.Models
{
    public partial class Photo : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return FormatPhoto.Validate(this);
        }
    }
}