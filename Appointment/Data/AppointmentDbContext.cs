using Appointment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data
{
    public class AppointmentDbContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppointmentDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }

        public DbSet<AppointmentModel> Appointment { get; set; }
    }
}