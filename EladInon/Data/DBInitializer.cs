using EladInon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new Location(){ID= 1,Adress="MaaleLEvona"},
                new Location(){ID = 2, Adress="Yzhar"}
            };
            foreach (var location in locations)
                context.Locations.Add(location);
            context.SaveChanges();
        }
    }
}
