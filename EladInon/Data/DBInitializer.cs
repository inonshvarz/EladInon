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
            var locations = new List<Location>
            {
                new Location(){Address="AmudeyAmram",LocationType = Dessert, Location_X = 29.652802m, Location_Y = 34.932271m },
                new Location(){Address="YamSuf",LocationType= SeaBeach, Location_X = 29.546229m, Location_Y = 34.969302m },
                new Location(){Address="male levona",LocationType=Park, Location_X = 32.054462m, Location_Y = 35.239503m },
                new Location(){Address="Ramat Hashron",LocationType=City, Location_X = 32.142401m, Location_Y = 34.845927m }
            };

            foreach (var location in locations)
                context.Locations.Add(location);
            context.SaveChanges();

            var Albums = new List<Album>
            {
                new Album(new DateTime(2018,11,27),AlbumType.Another){Name = "Eilath",LocationID = locations.First().ID,AlbumLocation = locations.First() },
                new Album(new DateTime(2018,11,27),AlbumType.Another){Name = "EilathSea",LocationID = locations[1].ID,AlbumLocation = locations[1] },
                new Album(new DateTime(2018,11,26),AlbumType.Family){Name = "Some" ,LocationID = locations[3].ID,AlbumLocation = locations[3] },
                new Album(new DateTime(2018,11,26),AlbumType.Family){Name = "Family" ,LocationID = locations[3].ID,AlbumLocation = locations[3] }
            };
            var pictures = new List<Picture>
            {
                new Picture(Albums[1],@"Pictures\1.jpeg"),
                new Picture(Albums[0], @"Pictures\2.jpeg"),
                new Picture(Albums[2], @"Pictures\3.jpg"),
                new Picture(Albums[3], @"Pictures\4.jpg"),
                new Picture(Albums[3], @"Pictures\5.jpg"),
                new Picture(Albums[0], @"Pictures\6.jpg"),
                new Picture(Albums[1], @"Pictures\7.jpg"),
                new Picture(Albums[2], @"Pictures\8.jpg"),
            };




            foreach (var Album in Albums)
                context.Albums.Add(Album);
            context.SaveChanges();

            foreach (var picture in pictures)
                context.Pictures.Add(picture);
            context.SaveChanges();
            var i = 0;
            foreach (var album in context.Albums)
            {
                if (i == 0)
                    album.Pictures = new List<Picture>() { pictures[1], pictures[5] };
                if (i == 1)
                    album.Pictures = new List<Picture>() { pictures[0], pictures[6] };
                if (i == 2)
                    album.Pictures = new List<Picture>() { pictures[2], pictures[7] };
                if (i == 3)
                    album.Pictures = new List<Picture>() { pictures[31], pictures[4] };
                i++;
            }

            context.SaveChanges();
            i = 0;
            foreach (var location in context.Locations)
            {
                if (i == 0)
                    location.Albums = new List<Album>() { Albums[0] };
                if (i == 1)
                    location.Albums = new List<Album>() { Albums[1] };
                if (i == 3)
                    location.Albums = new List<Album>() { Albums[2], Albums[3] };
                i++;
            }
            context.SaveChanges();
        }
    }
}
