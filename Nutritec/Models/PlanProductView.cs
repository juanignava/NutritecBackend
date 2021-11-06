using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class PlanProductView
    {
        public int Number { get; set; }
        public int Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Servings { get; set; }
        public string Mealtime { get; set; }
    }
}
