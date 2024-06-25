using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Mappointroute
    {
        public int MapPointId { get; set; }
        public int RouteId { get; set; }
        public int MpOrder { get; set; }

        public virtual Mappoint MapPoint { get; set; }
        public virtual Route Route { get; set; }
    }
}
