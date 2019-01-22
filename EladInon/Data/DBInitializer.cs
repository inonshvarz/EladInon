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
                new Location(){ID= 1,Adress="AmudeyAmram",LocationType = Dessert, Location_X = 32.0926000m, Location_Y = 34.8312178m }, 
                new Location(){ID = 2, Adress="YamSuf",LocationType= SeaBeach, Location_X = 32.0926099m, Location_Y = 34.8312999m },
                new Location(){ID = 3, Adress="male levona",LocationType= SeaBeach, Location_X = 32.0926099m, Location_Y = 34.8312999m }
            };

            var Albums = new Album[]
            {
                new Album(1, new DateTime(2018,11,27),AlbumType.Another)
            };

            var pictures = new Picture[]
            {
                new Picture(locations[1],Albums.First(),@"Pictures\1.jpeg"),
                new Picture(locations[0],Albums.First(), @"Pictures\2.jpeg")
            };

            var AlbumLocations = new AlbumLocations[]
            {
                new AlbumLocations(locations.First().ID,locations.First(),Albums.First().ID,Albums.First()),
                new AlbumLocations(locations.Last().ID, locations.Last(), Albums.First().ID, Albums.First())
            };

            foreach (var location in locations)
            {
                context.Locations.Add(location);
            }
            context.SaveChanges();

            foreach (var Album in Albums)
            {
                Album.Pictures = pictures;
                context.Albums.Add(Album);
            }
            context.SaveChanges();
            foreach (var picture in pictures)
                context.Pictures.Add(picture);
            context.SaveChanges();
            foreach (var item in AlbumLocations)
            {
                context.AlbumLocations.Add(item);
            }
            context.SaveChanges();
        }
    }
}
