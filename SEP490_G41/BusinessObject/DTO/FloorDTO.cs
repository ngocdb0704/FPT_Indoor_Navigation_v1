using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class FloorDTO
    {
        public int FloorId { get; set; }
        public string FloorName { get; set; } = null!;
        public string Greeting { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int BuildingId { get; set; }
    }

    public class FloorAddDTO
    {
        public string FloorName { get; set; } = null!;
        public string Greeting { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int BuildingId { get; set; }
    }

}
