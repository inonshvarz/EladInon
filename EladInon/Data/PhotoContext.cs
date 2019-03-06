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
        public DbSet<User> Users { get; set; }
        public DbSet<Packge> Packges { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().HasMany(l=>l.Albums).WithOne(a=>a.AlbumLocation).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Picture>().ToTable(nameof(Picture));
            modelBuilder.Entity<Album>().HasMany(a => a.Pictures).WithOne(p => p.ContainingAlbum).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<Packge>().ToTable(nameof(Packge));

        }
    }
}
