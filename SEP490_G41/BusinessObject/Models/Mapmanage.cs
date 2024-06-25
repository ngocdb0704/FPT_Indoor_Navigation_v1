using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Mapmanage
    {
        public int MapId { get; set; }
        public int MemberId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Map Map { get; set; }
        public virtual Member Member { get; set; }
    }
}
