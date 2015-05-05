using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionPhotoImmobilier.Models;
using System.ComponentModel.DataAnnotations;
using GestionPhotoImmobilier.DAL;

namespace GestionPhotoImmobilier.RegleDaffaire
{
    public class RDVUnique
    {
        public static ValidationResult Validate(Seance seance)
        {
            UnitOfWork uow = new UnitOfWork();

            if(seance.Rdvs == null || seance.Rdvs.Count == 0)
                return ValidationResult.Success;

            Rdv rendezVousSeance = seance.Rdvs.OrderByDescending(s => s.RdvId).First();

            IEnumerable<Seance> lstSeances = uow.SeanceRepository.ObtenirSeance();

            foreach (var item in lstSeances)
            {
                Seance sea = uow.SeanceRepository.ObtenirSeanceComplete(item.SeanceId);
                Rdv rendezvous = sea.Rdvs.OrderByDescending(s => s.RdvId).First();

                if(rendezvous.Photographe.Equals(rendezVousSeance.Photographe))
                {
                    if ((item.DateSeance != null) && (item.DateSeance.Value.Year == seance.DateSeance.Value.Year) &&
                        (item.DateSeance.Value.Month == seance.DateSeance.Value.Month) &&
                        (item.DateSeance.Value.Day == seance.DateSeance.Value.Day) &&
                        (item.DateSeance.Value.Hour == seance.DateSeance.Value.Hour))
                        return new ValidationResult("Le photographe a déjà une séance à la date et heure indiquée.");
                }
            }

            return ValidationResult.Success;
        }
    }
}