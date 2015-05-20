using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        private H15_PROJET_E03Entities context = new H15_PROJET_E03Entities();
        //private GestionPhotoImmobilierEntities1 context = new GestionPhotoImmobilierEntities1();


        private AgenceRepository agenceRepository;
        private AgentRepository agentRepository;
        private SeanceRepository seancerepository;
        private RdvRepository rdvrepository;
        private PhotoRepository photoRepository;
        private ProprieteRepository proprieteRepository;
        private ForfaitRepository forfaitRepository;

        public ForfaitRepository ForfaitRepository
        {
            get
            {

                if (this.forfaitRepository == null)
                {
                    this.forfaitRepository = new ForfaitRepository(context);
                }
                return forfaitRepository;
            }
        }

        public AgenceRepository AgenceRepository
        {
            get
            {

                if (this.agenceRepository == null)
                {
                    this.agenceRepository = new AgenceRepository(context);
                }
                return agenceRepository;
            }
        }


        public AgentRepository AgentRepository
        {
            get
            {
                if (this.agentRepository == null)
                {
                    this.agentRepository = new AgentRepository(context);
                }
                return agentRepository;
            }
        }

        public SeanceRepository SeanceRepository
        {
            get
            {
                if (this.seancerepository == null)
                {
                    this.seancerepository = new SeanceRepository(context);
                }
                return seancerepository;
            }
        }

        public RdvRepository RdvRepository
        {
            get
            {
                if (this.rdvrepository == null)
                {
                    this.rdvrepository = new RdvRepository(context);
                }
                return rdvrepository;
            }
        }

        public PhotoRepository PhotoRepository
        {
            get
            {
                if (this.photoRepository == null)
                {
                    this.photoRepository = new PhotoRepository(context);
                }
                return photoRepository;
            }
        }

        public ProprieteRepository ProprieteRepository 
        {
            get
            {
                if (this.proprieteRepository == null)
                {
                    this.proprieteRepository = new ProprieteRepository(context);
                }
                return proprieteRepository;
            }
        }

        
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors

                        .SelectMany(x => x.ValidationErrors)

                        .Select(x => x.ErrorMessage);
                // Join the list to a single string.

                var fullErrorMessage = string.Join("; ", errorMessages);
                // Combine the original exception message with the new one.

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}