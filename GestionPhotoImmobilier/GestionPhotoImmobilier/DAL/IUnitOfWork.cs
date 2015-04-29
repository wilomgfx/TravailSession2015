using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace GestionPhotoImmobilier.DAL
{
    interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}