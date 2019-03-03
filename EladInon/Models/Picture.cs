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
        public Picture(Album album, string path)
        {
            Path = path;
            ContainingAlbum = album;
        }

        public int? ID { get; set; }
        public int? AlbumID { get; set; }
        public Album ContainingAlbum { get; set; }
        public string Path { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
