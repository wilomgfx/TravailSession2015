using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GestionPhotoImmobilier.Models;
using GestionPhotoImmobilier.DAL;

namespace GestionPhotoImmobilier.RegleDaffaire
{
    public class Validation4hEntreRendezVous
    {
        public static ValidationResult Validate(GestionPhotoImmobilier.Models.Seance seance)
        {
            UnitOfWork unitofWork = new UnitOfWork();

            // Il doit y avoir un minimum de 4h entre deux rendez-vous  
            // pour un même phtographe la même journée

            if(seance == null)
            return ValidationResult.Success;

            foreach(var rdv in seance.Rdvs)
            {
                // vérifie que la date n'a pas déjà eu lieu
                int d =  DateTime.Compare(seance.DateSeance.Value,DateTime.Now);
                if(d == -1)
                    return new ValidationResult("La Date de la séance doit être ultérieur à aujourd'hui");

                int  heureDiff = (seance.DateSeance.Value.Hour - rdv.Seance.DateSeance.Value.Hour) ;

                // vérifie que la date de la seance est plus tard que l'heure actuel
                if(rdv.Seance.DateSeance == seance.DateSeance && seance.DateSeance.Value.Hour < DateTime.Now.Hour)
                    return new ValidationResult("L'heure de la séance doit être ultérieur à celle d'aujourd'hui");

                // vérifie que l'heure de la séance est plus tard d'au moins 4h avec le dernier rendez-vous deja séduler sur l'horaire pour un meme photographe 
                if (rdv.Seance.DateSeance == seance.DateSeance && seance.DateSeance.Value.Hour < rdv.Seance.DateSeance.Value.AddHours(4).Hour && seance.Photographe == rdv.Seance.Photographe)
                    return new ValidationResult("L'heure de la séance doit être ultérieur d'au moins 4h au dernier rendez-vous de la journée");
                
            }
            return ValidationResult.Success;
        } 
    }
}