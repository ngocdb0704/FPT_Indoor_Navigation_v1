using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Map
    {
        public Map()
        {
            Mapmanages = new HashSet<Mapmanage>();
            Mappoints = new HashSet<Mappoint>();
        }

        public int MapId { get; set; }
        public int FloorId { get; set; }
        public string MapName { get; set; } = null!;
        public string MapImage2D { get; set; } = null!;
        public string? MapImage3D { get; set; }

        public virtual Floor Floor { get; set; }
        public virtual ICollection<Mapmanage> Mapmanages { get; set; }
        public virtual ICollection<Mappoint> Mappoints { get; set; }
    }
}
