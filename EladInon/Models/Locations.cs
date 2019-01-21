using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class Location
    {
        public Location()
        {
            SessionLocationss = new List<SessionLocations>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Adress { get; set; }
        public LocationType LocationType { get; set; }
        public decimal Location_X { get; set; }
        public decimal Location_Y { get; set; }
        public virtual ICollection<SessionLocations> SessionLocationss { get; set; }
    }
}
