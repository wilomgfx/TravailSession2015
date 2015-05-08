using GestionPhotoImmobilier.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.RegleDaffaire
{
    public class ValidationTypeFormatPhoto
    {
        public static ValidationResult Validate(GestionPhotoImmobilier.Models.Photo photo)
        {
            UnitOfWork unitofWork = new UnitOfWork();

            // Doit vérifier lors de la création d'une photo que seul les formats .jpg, .jpeg et .png sont accepté

            if (photo.TypeFichier == ".jpg" || photo.TypeFichier == ".jpeg" || photo.TypeFichier == ".png")
                return ValidationResult.Success;


            return new ValidationResult("Le format de la photo entré n'est pas supporté, veillez choisir un des format suivant :  .jpg, .jpeg, .png ");
        } 
    }
}