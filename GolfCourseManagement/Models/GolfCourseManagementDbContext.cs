using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GolfCourseManagement.Models
{
    public class GolfCourseManagementDbContext : DbContext
    {

        public GolfCourseManagementDbContext(DbContextOptions<GolfCourseManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeeTime>()
                .HasOne(tt => tt.GolfCourse)
                .WithMany(gc => gc.TeeTimes)
                .HasForeignKey(tt => tt.GolfCourseID);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.TeeTime)
                .WithMany(tt => tt.Reservations)
                .HasForeignKey(r => r.TeeTimeID);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerID);
        }

        public DbSet<GolfCourse> GolfCourses { get; set; }
        public DbSet<TeeTime> TeeTimes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
