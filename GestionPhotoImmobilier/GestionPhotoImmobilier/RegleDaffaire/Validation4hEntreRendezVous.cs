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

            foreach(var sea in unitofWork.SeanceRepository.ObtenirSeance())
            {
                // Si la séance n'a pas de date. Ne devrait jamais arrivé.
                if (sea.DateSeance == null)
                    return ValidationResult.Success;

                // Si la séance n'a aucun rendez vous de lié.
                //if (unitofWork.RdvRepository.ObtenirRdvDeLaSeance(sea.SeanceId).Count() == 0)
                //    return ValidationResult.Success;

                // Si la séance prise ici est la même que celle qu'on essaye de valider...
                if (sea.SeanceId == seance.SeanceId)
                    continue;

                // vérifie que la date de la seance est plus tard que l'heure actuel
                if (sea.DateSeance.Value.Year == seance.DateSeance.Value.Year && 
                    sea.DateSeance.Value.Month == seance.DateSeance.Value.Month &&
                    sea.DateSeance.Value.Day == seance.DateSeance.Value.Day &&
                    sea.Photographe.Equals(seance.Photographe))
                {
                    TimeSpan diff = seance.DateSeance.Value - sea.DateSeance.Value;

                    // vérifie que l'heure de la séance est plus tard d'au moins 4h avec le dernier rendez-vous deja séduler sur l'horaire pour un meme photographe 
                    if (Math.Abs(diff.Hours) < 4)
                        return new ValidationResult("L'heure de la séance doit être ultérieur d'au moins 4h au dernier rendez-vous de la journée.");
                }
            }

            return ValidationResult.Success;
        } 
    }
}