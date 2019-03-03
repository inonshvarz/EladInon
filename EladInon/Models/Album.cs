using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class Album
    {
        public Album()
        {
        }
        public Album(DateTime time, AlbumType albumType):this()
        {
            Time = time;
            AlbumType = albumType;
        }
        public int? ID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
        public AlbumType AlbumType { get; set; }
        public int? LocationID { get; set; }
        public Location AlbumLocation { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }

        [NotMapped]
        public virtual ICollection<IFormFile> Images { get; set; }

    }
}
