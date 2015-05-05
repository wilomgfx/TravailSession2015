using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GestionPhotoImmobilier.DAL;
using GestionPhotoImmobilier.Models;


namespace GestionPhotoImmobilier.RegleDaffaire
{
    public class DateEntreUnEt15JoursPlusTard
    {
       public static ValidationResult Validate(GestionPhotoImmobilier.Models.Seance seance)
       {
            return ValidationResult.Success;
        } 

       
       
       
       
       
       }
}