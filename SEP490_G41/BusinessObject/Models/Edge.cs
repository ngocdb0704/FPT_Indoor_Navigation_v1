using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Edge
    {
        public int EdgeId { get; set; }
        public int MapPointA { get; set; }
        public int MapPointB { get; set; }
        public int Direction { get; set; }
        public double Distance { get; set; }

        public virtual Mappoint MapPointANavigation { get; set; } = null!;
        public virtual Mappoint MapPointBNavigation { get; set; } = null!;

    }
}
