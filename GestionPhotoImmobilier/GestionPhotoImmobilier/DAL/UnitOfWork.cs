﻿using GestionPhotoImmobilier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPhotoImmobilier.DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        private H15_PROJET_E03Entities context = new H15_PROJET_E03Entities();

        private AgentRepository agentRepository;
        private SeanceRepository seancerepository;
        private RdvRepository rdvrepository;
        private PhotoRepository photoRepository;
        private ProprieteRepository proprieteRepository;


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