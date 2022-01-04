using Microsoft.EntityFrameworkCore;
using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScanningApp.Infrastructure.Data
{
    public class ScanningAppContext : DbContext

    {
        public ScanningAppContext(DbContextOptions<ScanningAppContext> opt)
            : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Scan>()
               .HasOne(s => s.Concert)
               .WithMany(c => c.Scans)
               .OnDelete(DeleteBehavior.SetNull*/
        }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Scan> Scans { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
