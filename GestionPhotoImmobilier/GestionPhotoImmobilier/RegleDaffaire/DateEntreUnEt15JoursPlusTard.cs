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

           if(seance.DateSeance != null)
           {
               DateTime? date = seance.DateSeance;
               if(date < DateTime.Now.AddDays(1))
               {
                   return new ValidationResult("La date doit être supérieur à une journée suivant la date du jour");
               }
               if (date > DateTime.Now.AddDays(15))
               {
                   return new ValidationResult("La date doit être inférieure ou égale à 15 journée suivant la date du jour");
               }
           }
            return ValidationResult.Success;
        } 

       
       
       
       
       
       }
}