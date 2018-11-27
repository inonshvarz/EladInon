using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class SessionLocations
    {
        public SessionLocations(int locationID, Location location, int sesionID, Session session)
        {
            LocationID = locationID;
            Location = location;
            SessionID = sesionID;
            Session = session;
        }
        public SessionLocations()
        {

        }

        public int LocationID { get; set; }
        public Location Location { get; set; }
        public int SessionID { get; set; }
        public Session Session { get; set; }
    }
}
