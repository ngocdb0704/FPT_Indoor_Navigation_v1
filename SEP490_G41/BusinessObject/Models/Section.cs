using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace BusinessObject.Models
{
    public partial class Section
    {
        public int SectionId { get; set; }
        public int FloorId { get; set; }
        public string SectionName { get; set; } = null!;
        public int Long { get; set; }
        public int Width { get; set; }
        public Point UpCorner { get; set; } = null!;
        public Point DownCorner { get; set; } = null!;

        public virtual Floor Floor { get; set; } = null!;
    }
}
