using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Mappointex
    {
        public int MapPointExId { get; set; }
        public int MapPointId { get; set; }
        public string Url { get; set; } = null!;

        public virtual Mappoint MapPoint { get; set; } = null!;
    }
}
