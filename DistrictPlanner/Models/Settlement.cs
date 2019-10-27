using System;
using System.Collections.Generic;

namespace DistrictPlanner.Models
{
    public partial class Settlement
    {
        public Settlement()
        {
            RoadsEndSettlement = new HashSet<Road>();
            RoadsStartSettlement = new HashSet<Road>();
        }

        public int SettlementId { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }

        public virtual ICollection<Road> RoadsEndSettlement { get; set; }
        public virtual ICollection<Road> RoadsStartSettlement { get; set; }
    }
}
