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
               DateTime dateMaintenant = DateTime.Now;
               dateMaintenant = dateMaintenant.AddHours(-(dateMaintenant.Hour));
               dateMaintenant = dateMaintenant.AddMinutes(-(dateMaintenant.Minute));
               dateMaintenant = dateMaintenant.AddSeconds(-(dateMaintenant.Second));
               dateMaintenant = dateMaintenant.AddMilliseconds(-(dateMaintenant.Millisecond));

               DateTime dateMaintenantPlusUnJour = dateMaintenant.AddDays(1);
               DateTime dateMaintenantPlus15Jours = dateMaintenant.AddDays(15);

               if (date.Value.Day == dateMaintenantPlusUnJour.Day && date.Value.Month == dateMaintenantPlusUnJour.Month && date.Value.Year == dateMaintenantPlusUnJour.Year)
               {
                   return ValidationResult.Success;
               }
               if(date < dateMaintenantPlusUnJour)
               {
                   return new ValidationResult("La date doit être supérieur à une journée suivant la date du jour");
               }
               if (date >= dateMaintenantPlus15Jours)
               {
                   return new ValidationResult("La date doit être inférieure ou égale à 15 journée suivant la date du jour");
               }
           }
            return ValidationResult.Success;
        } 

       
       
       
       
       
       }
}