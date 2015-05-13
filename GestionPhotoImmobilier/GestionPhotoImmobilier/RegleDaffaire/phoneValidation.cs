using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.RegleDaffaire
{
    public class phoneValidation
    {
         public static ValidationResult Validate(GestionPhotoImmobilier.Models.Agence Agence)
       {
           var phoneRegex = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
             string errorMessagePhone = "Veuillez entrer une numéro de téléphone valide sous le format : (XXX)-XXX-XXXX ";

             if(Agence.NumTel == null)
                return ValidationResult.Success;

             if(Regex.IsMatch(Agence.NumTel,phoneRegex))
              return ValidationResult.Success;


                 return new ValidationResult(errorMessagePhone);
            

        } 
    }
}