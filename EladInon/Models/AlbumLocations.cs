﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class AlbumLocations
    {
        public AlbumLocations(int locationID, Location location, int sesionID, Album album)
        {
            LocationID = locationID;
            Location = location;
            AlbumID = sesionID;
            Album = album;
        }
        public AlbumLocations()
        {

        }

        public int LocationID { get; set; }
        public Location Location { get; set; }
        public int AlbumID { get; set; }
        public Album Album { get; set; }
    }
}
