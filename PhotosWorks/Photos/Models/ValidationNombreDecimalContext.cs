using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Photos.Models
{
    public class ValidationNombreDecimalContext : DbContext
    {
        public DbSet<ValidationNbrDecimal> ValidationNbrDecimals { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}