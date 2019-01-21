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
                new Location(){ID= 1,Adress="AmudeyAmram",LocationType = Dessert, Location_X = 32, Location_Y = 23 }, 
                new Location(){ID = 2, Adress="YamSuf",LocationType= SeaBeach, Location_X = 35, Location_Y = 23 }
            };

            var sessions = new Session[]
            {
                new Session(1, new DateTime(2018,11,27),SessionType.Another)
            };

            var pictures = new Picture[]
            {
                new Picture(locations[1],sessions.First(),@"Pictures\1.jpeg"),
                new Picture(locations[0],sessions.First(), @"Pictures\2.jpeg")
            };

            var sessionLocations = new SessionLocations[]
            {
                new SessionLocations(locations.First().ID,locations.First(),sessions.First().ID,sessions.First()),
                new SessionLocations(locations.Last().ID, locations.Last(), sessions.First().ID, sessions.First())
            };

            foreach (var location in locations)
            {
                context.Locations.Add(location);
            }
            context.SaveChanges();

            foreach (var session in sessions)
            {
                context.Sessions.Add(session);
            }
            context.SaveChanges();
            foreach (var picture in pictures)
                context.Pictures.Add(picture);
            context.SaveChanges();
            foreach (var item in sessionLocations)
            {
                context.SessionLocations.Add(item);
            }
            context.SaveChanges();
        }
    }
}
