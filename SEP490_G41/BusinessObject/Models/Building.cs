using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Building
    {
        public Building()
        {
            Floors = new HashSet<Floor>();
            Mappoints = new HashSet<Mappoint>();
        }

        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }
        public virtual ICollection<Floor> Floors { get; set; }
        public virtual ICollection<Mappoint> Mappoints { get; set; }
    }
}
