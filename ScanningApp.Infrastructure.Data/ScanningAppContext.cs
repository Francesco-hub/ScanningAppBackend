using Microsoft.EntityFrameworkCore;
using ScanningApp.Core.Entity;

namespace ScanningApp.Infrastructure.Data
{
    public class ScanningAppContext : DbContext

    {
        public ScanningAppContext(DbContextOptions<ScanningAppContext> opt)
            : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Scan> Scans { get; set; }

        public DbSet<User> Users { get; set; }

        //Real users are initialized here
        public void InitializeUsers()
        {
            var user1_pia = Users.Add(new User()
            {
                Code = 1111,
                FirstName = "Pia",
                LastName = "Jørs"
            }).Entity;

            var user2_gabriella = Users.Add(new User()
            {
                Code = 2222,
                FirstName = "Gabriella",
                LastName = "Bergman"
            }).Entity;

            SaveChanges();
        }
    }
}
