using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class Packge
    {
        public Packge()
        {

        }

        public Packge(string name, string description, AlbumType albumType, double price)
        {
            Name = name;
            Description = description;
            AlbumType = albumType;
            Price = price;
        }

        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AlbumType AlbumType { get; set; }
        public double Price { get; set; }
    }
}
