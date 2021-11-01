using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class DailyPlan
    {
        public DailyPlan()
        {
            Follows = new HashSet<Follow>();
            PlanHas = new HashSet<PlanHa>();
        }

        public int Number { get; set; }
        public string Name { get; set; }
        public string NutritionistEmail { get; set; }

        public virtual Nutritionist NutritionistEmailNavigation { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<PlanHa> PlanHas { get; set; }
    }
}
