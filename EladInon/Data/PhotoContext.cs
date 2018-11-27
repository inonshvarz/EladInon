using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EladInon.Models;
using Microsoft.EntityFrameworkCore;

namespace EladInon.Data
{
    public class PhotoContext: DbContext
    {
        public PhotoContext(DbContextOptions<PhotoContext> options)
            :base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionLocations> SessionLocations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().ToTable(nameof(Location));
            modelBuilder.Entity<Picture>().ToTable(nameof(Picture));
            modelBuilder.Entity<Session>().ToTable(nameof(Session));
            modelBuilder.Entity<SessionLocations>().HasKey(sl => new { sl.SessionID, sl.LocationID });

        }
    }
}
