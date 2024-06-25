using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Route
    {
        public Route()
        {
            Mappointroutes = new HashSet<Mappointroute>();
        }

        public int RouteId { get; set; }
        public string RouteName { get; set; } = null!;
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public double Distance { get; set; }

        public virtual ICollection<Mappointroute> Mappointroutes { get; set; }
    }
}
