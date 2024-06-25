using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Facility
    {
        public Facility()
        {
            Buildings = new HashSet<Building>();
        }

        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }
    }
}
