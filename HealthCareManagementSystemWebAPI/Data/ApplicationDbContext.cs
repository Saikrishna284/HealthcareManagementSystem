using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystemWebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Interaction> Interactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships
            builder.Entity<Interaction>()
                .HasOne(i => i.Patient)
                .WithMany(p => p.Interactions)
                .HasForeignKey(i => i.PatientId);

            builder.Entity<Interaction>()
                .HasOne(i => i.User)
                .WithMany(p => p.Interactions)
                .HasForeignKey(i => i.UserId);
        }
    }
}