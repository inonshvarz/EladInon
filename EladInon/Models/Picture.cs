using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class Picture
    {
        public Picture()
        {

        }
        public Picture(Location location, Album album, string path)
        {
            Location = location;
            Path = path;
            Album = album;
        }

        public int ID { get; set; }
        public Location Location { get; set; }
        public Album Album { get; set; }
        public string Path { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
