 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class Picture
    {
        public Picture()
        {

        }
        public Picture(Location location, Session session, string path)
        {
            Location = location;
            Path = path;
            Session = session;
        }

        public int ID { get; set; }
        public Location Location { get; set; }
        public Session Session { get; set; }
        public string Path { get; set; }
    }
}
