using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OSMH.Models
{
    public class OSMHDbContext : DbContext
    {
        public DbSet<Blog> blogs { get; set; }
        public DbSet<UserAccount> userAccounts { get; set; }
        public DbSet<Doctor> doctors { get; set; }
    }
}