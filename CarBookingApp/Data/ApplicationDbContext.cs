using CarBookingApp.Authentication;
using CarBookingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options){ }

        public DbSet<Review> Reviews { get; set; }
        
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<UserManagement> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
