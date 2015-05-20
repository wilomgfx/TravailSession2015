using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.RegleDaffaire
{
    public class FormatPhoto:ValidationAttribute
    {
        //Un attribut personnalisé permettra de valider que le format du fichier est l’un de cela 
        //jpg, jpeg et png
        public static ValidationResult Validate(Photo photo)
        {
            //return ValidationResult.Success;

            string ErrorMessage = "Le format de la photo est érroné, veuillez en choisir un parmi les suivants : jpg, jpeg ou png ";
            if (photo.Chemin != null)
            {
                string path = photo.Chemin;
                if (path.Contains(".jpg") || path.Contains(".jpeg") || path.Contains(".png"))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
            //else
            //{
            //    return new ValidationResult("Le chemin de l'image n'est pas valide.");
            //}       
        }
    }
}