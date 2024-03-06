using IdentityDataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace IdentityDataAccess
{
    public class UserContext : DbContext
    {
        public UserContext( DbContextOptions<UserContext> options)
            : base(options)
        {
          
        }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email);
                

            base.OnModelCreating(modelBuilder);
        }
    }
}
