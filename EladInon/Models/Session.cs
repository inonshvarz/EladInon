using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.Models
{
    public class Session
    {
        public Session()
        {
            SessionLocations = new List<SessionLocations>();
        }
        public Session(int id, DateTime time, SessionType sessionType):this()
        {
            Time = time;
            SessionType = sessionType;
            ID = id;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public SessionType SessionType { get; set; }
        public virtual ICollection<SessionLocations> SessionLocations { get; set; }
    }
}
