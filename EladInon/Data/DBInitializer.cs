using EladInon.Models;
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
            var locations = new Location[]
            {
                new Location(){Address="AmudeyAmram",LocationType = Dessert, Location_X = 32.0926000m, Location_Y = 34.8312178m },
                new Location(){Address="YamSuf",LocationType= SeaBeach, Location_X = 32.0926099m, Location_Y = 34.8312999m },
                new Location(){Address="male levona",LocationType= SeaBeach, Location_X = 32.0926099m, Location_Y = 34.8312999m }
            };

            var Albums = new Album[]
            {
                new Album(new DateTime(2018,11,27),AlbumType.Another){Name = "MishMash",LocationID = locations.First().ID,AlbumLocation = locations.First() },
                new Album(new DateTime(2018,11,26),AlbumType.Family){Name = "Family" ,LocationID = locations.First().ID,AlbumLocation = locations.First() }
            };
            locations.First().Albums = Albums;
            var pictures = new Picture[]
            {
                new Picture(Albums.First(),@"Pictures\1.jpeg"),
                new Picture(Albums.First(), @"Pictures\2.jpeg")
            };
            Albums.First().Pictures = pictures;

          
            foreach (var location in locations)
            {
                context.Locations.Add(location);
            }
            context.SaveChanges();

            foreach (var Album in Albums)
                context.Albums.Add(Album);
            context.SaveChanges();

            foreach (var picture in pictures)
            {
                context.Pictures.Add(picture);
            }
            context.SaveChanges();
        }
    }
}
