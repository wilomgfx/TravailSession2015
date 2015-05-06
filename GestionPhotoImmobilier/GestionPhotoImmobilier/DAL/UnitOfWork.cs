using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        private H15_PROJET_E03Entities context = new H15_PROJET_E03Entities();

        private SeanceRepository seancerepository;
        private RdvRepository rdvrepository;

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




        public void Save()
        {
            context.SaveChanges();
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