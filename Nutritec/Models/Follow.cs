using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class Follow
    {
        public string PatientEmail { get; set; }
        public int PlanNumber { get; set; }
        public string Month { get; set; }

        public virtual Patient PatientEmailNavigation { get; set; }
        public virtual DailyPlan PlanNumberNavigation { get; set; }
    }
}
