using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class Album
    {
        public Album()
        {
            AlbumLocations = new List<AlbumLocations>();
        }
        public Album(int id, DateTime time, AlbumType albumType):this()
        {
            Time = time;
            AlbumType = albumType;
            ID = id;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public AlbumType AlbumType { get; set; }
        public virtual ICollection<AlbumLocations> AlbumLocations { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }

        [NotMapped]
        public virtual ICollection<IFormFile> Images { get; set; }
    }
}
