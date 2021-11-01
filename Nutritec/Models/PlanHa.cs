using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class PlanHa
    {
        public int PlanNumber { get; set; }
        public int ProductBarcode { get; set; }
        public string Mealtime { get; set; }
        public int Servings { get; set; }

        public virtual DailyPlan PlanNumberNavigation { get; set; }
        public virtual Product ProductBarcodeNavigation { get; set; }
    }
}
