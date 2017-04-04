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
        public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Alert> Alerts { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Schedule>().HasRequired(s => s.Doctor).WithMany(s => s.Schedules).HasForeignKey(r => r.Doctor_id);
            modelBuilder.Entity<Appointment>().HasRequired(a => a.schedule).WithMany(a => a.Appointments).HasForeignKey(a => a.Schedule_Id);
            modelBuilder.Entity<Appointment>().HasRequired(a => a.patient).WithMany(a => a.appointments).HasForeignKey(a => a.Patient_Id);
            modelBuilder.Entity<User>().HasOptional(u => u.Doctor).WithRequired(d => d.User);
        }
    }
}