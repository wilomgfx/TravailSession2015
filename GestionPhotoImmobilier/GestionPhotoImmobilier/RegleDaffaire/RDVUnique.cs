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

            IEnumerable<Rdv> rdvsSeance = uow.RdvRepository.ObtenirRdvDeLaSeance(seance.SeanceId);

            if (rdvsSeance == null || rdvsSeance.Count() == 0)
                return ValidationResult.Success;

            Rdv rendezVousSeance = rdvsSeance.OrderByDescending(s => s.RdvId).First();

            IEnumerable<Seance> lstSeances = uow.SeanceRepository.ObtenirSeance();

            foreach (var item in lstSeances)
            {
                if (item.SeanceId == seance.SeanceId)
                    continue;

                Seance sea = uow.SeanceRepository.ObtenirSeanceComplete(item.SeanceId);
                //Rdv rendezvous = sea.Rdvs.OrderByDescending(s => s.RdvId).First();
                Rdv rendezvous = uow.RdvRepository.ObtenirRdvDeLaSeance(sea.SeanceId).OrderByDescending(b => b.RdvId).First();

                if(item.Photographe.Equals(seance.Photographe))
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