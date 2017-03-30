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
        public DbSet<User> users { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<VisitorLimit> VisitorLimit { get; set; }
        public DbSet<VisitorReg> VisitorReg { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Schedule> Schedules { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Schedule>().HasRequired(s => s.Doctor).WithMany(s => s.Schedules).HasForeignKey(r => r.Doctor_id);
        }
    }
}