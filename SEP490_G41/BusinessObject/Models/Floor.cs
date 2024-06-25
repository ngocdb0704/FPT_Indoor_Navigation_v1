using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Floor
    {
        public Floor()
        {
            Mappoints = new HashSet<Mappoint>();
            Maps = new HashSet<Map>();
            Sections = new HashSet<Section>();
        }

        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public string Greeting { get; set; }
        public string Status { get; set; }
        public int BuildingId { get; set; }

        public virtual Building Building { get; set; } = null!;
        public virtual ICollection<Mappoint> Mappoints { get; set; }
        public virtual ICollection<Map> Maps { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
