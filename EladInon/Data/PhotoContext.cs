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
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumLocations> AlbumLocations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().ToTable(nameof(Location));
            modelBuilder.Entity<Picture>().ToTable(nameof(Picture));
            modelBuilder.Entity<Album>().ToTable(nameof(Album));
            modelBuilder.Entity<AlbumLocations>().HasKey(sl => new { sl.AlbumID, sl.LocationID });

        }
    }
}
