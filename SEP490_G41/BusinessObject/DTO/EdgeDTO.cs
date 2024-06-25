using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class EdgeDTO
    {
        public int EdgeId { get; set; }
        public int MapPointA { get; set; }
        public string MapPointAName { get; set; } = null!;
        public int MapPointB { get; set; }
        public string MapPointBName { get; set; } = null!;
        public int Direction { get; set; }
        public double Distance { get; set; }
    }

    public class EdgeAddDTO
    {
        public int MapPointA { get; set; }
        public int MapPointB { get; set; }
        public int Direction { get; set; }
        public double Distance { get; set; }
    }

    public class EdgeUpdateDTO
    {
        public int EdgeId { get; set; }
        public int MapPointA { get; set; }
        public int MapPointB { get; set; }
        public int Direction { get; set; }
        public double Distance { get; set; }
    }
}
