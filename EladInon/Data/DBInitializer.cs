﻿using EladInon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static EladInon.Models.LocationType;
using System.Threading.Tasks;

namespace EladInon.Data
{
    public static class DBInitializer
    {
        public static void Initialize(PhotoContext context)
        {
            context.Database.EnsureCreated();
            if (context.Locations.Any()) return;
            var packages = new List<Packge>()
            {
                new Packge("BatMitzvaBasic","Hour,50 pictures, 10 edited",AlbumType.BatMitzva,700),
                new Packge("BatMitzvaExtended","2Hours,80 pictures, 20 edited",AlbumType.BatMitzva,1200),
                new Packge("Family","Hours,50 pictures, 15 edited",AlbumType.Family,850),
                new Packge("FamilyExtended","2Hours,50 pictures + 10 for each person, 10 + 2 for each person edited",AlbumType.Family,1400),
                new Packge("Purim","30 min,20 pictures edited",AlbumType.Another,300),
            };
            foreach (var packge in packages)
                context.Packges.Add(packge);
            context.SaveChanges();
            var user = new User()
            {
                UserName = "Admin",
                Password = "12345678",
                FirstName = "Admin",
                LastName = "Example",
                EMail = "Admin@Example.com",
                IsAdmin = true
            };
            context.Users.Add(user);
            context.SaveChanges();
            var locations = new Location[]
            {
                new Location(){Address="AmudeyAmram",LocationType = Dessert, Location_X = 29.652802m, Location_Y = 34.932271m },
                new Location(){Address="YamSuf",LocationType= SeaBeach, Location_X = 29.546229m, Location_Y = 34.969302m },
                new Location(){Address="male levona",LocationType= SeaBeach, Location_X = 32.054462m, Location_Y = 35.239503m }
            };

            var Albums = new Album[]
            {
                new Album(new DateTime(2018,11,27),AlbumType.Another){Name = "MishMash",LocationID = locations.First().ID,AlbumLocation = locations.First() },
                new Album(new DateTime(2018,11,26),AlbumType.Family){Name = "Family" ,LocationID = locations.First().ID,AlbumLocation = locations.First() }
            };
            var pictures = new Picture[]
            {
                new Picture(Albums.First(),@"Pictures\1.jpeg"),
                new Picture(Albums.First(), @"Pictures\2.jpeg")
            };

          
            foreach (var location in locations)
                context.Locations.Add(location);
            context.SaveChanges();

            foreach (var Album in Albums)
                context.Albums.Add(Album);
            context.SaveChanges();

            foreach (var picture in pictures)
                context.Pictures.Add(picture);
            context.SaveChanges();
            context.Albums.First().Pictures = pictures;
            context.SaveChanges();
            context.Locations.First().Albums = Albums;
            context.SaveChanges();
            
        }
    }
}
