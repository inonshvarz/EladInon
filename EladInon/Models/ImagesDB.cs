using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class ImagesDB : DbContext
    {
        public DbSet<Image> Items { get; set; }
    }
}
