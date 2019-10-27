using System;
using System.Collections.Generic;

namespace DistrictPlanner.Models
{
    public partial class Road
    {
        public int RoadId { get; set; }
        public int StartSettlementId { get; set; }
        public int EndSettlementId { get; set; }
        public decimal Distance { get; set; }

        public virtual Settlement EndSettlement { get; set; }
        public virtual Settlement StartSettlement { get; set; }
    }
}
