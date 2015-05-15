using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.RegleDaffaire
{
    public class photoValidation:ValidationAttribute
    {
        //Un attribut personnalisé permettra de valider que le format du fichier est l’un de cela 
        //jpg, jpeg et png
        public photoValidation():base()
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string ErrorMessage = "Le format de la photo est érroné, veuillez en choisir un parmi les suivants : jpg, jpeg ou png ";
            if(value != null && value is string )
            {
                string path = (string)value;
                if(path.Contains(".jpg")|| path.Contains(".jpeg")|| path.Contains(".png"))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}