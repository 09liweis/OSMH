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
        public DbSet<EmailPost> EmailPost { get; set; }
        public DbSet<EmailSub> EmailSub { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Alert> Alerts { get; set; }
        public DbSet<Message> Messages { get; set; }
		public DbSet<Suggestion> Suggestions { get; set; }
		public DbSet<SuggestionComment> SuggestionComments { get; set; }
		public DbSet<SuggestionUpvote> SuggestionUpvotes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
		public DbSet<StaticPage> StaticPages { get; set; }
        public DbSet<Donations> Donations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<FAQ> FAQs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Schedule>().HasRequired(s => s.Doctor).WithMany(s => s.Schedules).HasForeignKey(r => r.Doctor_id);
            modelBuilder.Entity<Appointment>().HasRequired(a => a.schedule).WithMany(a => a.Appointments).HasForeignKey(a => a.Schedule_Id);
            modelBuilder.Entity<Appointment>().HasRequired(a => a.patient).WithMany(a => a.appointments).HasForeignKey(a => a.Patient_Id);
            modelBuilder.Entity<Doctor>().HasRequired(d => d.User).WithMany(d => d.Doctors).HasForeignKey(d => d.User_id);
            modelBuilder.Entity<Patient>().HasRequired(p => p.User).WithMany(p => p.Patients).HasForeignKey(p => p.User_id);
            //modelBuilder.Entity<User>().HasOptional(u => u.Doctor).WithRequired(d => d.User);
			modelBuilder.Entity<User>().HasMany(s => s.Suggestions);
			modelBuilder.Entity<User>().HasMany(s => s.SuggestionComments);
			modelBuilder.Entity<Suggestion>().HasRequired(u => u.User).WithMany(s => s.Suggestions).HasForeignKey(u => u.UserId);
			modelBuilder.Entity<Suggestion>().HasMany(s => s.SuggestionUpvotes);
			modelBuilder.Entity<Suggestion>().HasMany(s => s.SuggestionComments);
			modelBuilder.Entity<SuggestionComment>().HasRequired(u => u.User);
			modelBuilder.Entity<SuggestionComment>().HasRequired(s => s.Suggestion).WithMany(s => s.SuggestionComments).HasForeignKey(s => s.SuggestionId);
			modelBuilder.Entity<SuggestionUpvote>().HasRequired(s => s.Suggestion).WithMany(s => s.SuggestionUpvotes).HasForeignKey(s => s.SuggestionId);
			modelBuilder.Entity<StaticPage>().HasRequired(u => u.User);
        }

        public System.Data.Entity.DbSet<OSMH.Models.AcceptedPayment> AcceptedPayments { get; set; }
    }
}