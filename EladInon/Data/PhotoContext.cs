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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().ToTable(nameof(Location));

        }
    }
}
