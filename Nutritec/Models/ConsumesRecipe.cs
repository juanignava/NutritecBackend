using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class ConsumesRecipe
    {
        public int RecipeNumber { get; set; }
        public string PatientEmail { get; set; }
        public string Day { get; set; }
        public string Meal { get; set; }

        public virtual Patient PatientEmailNavigation { get; set; }
        public virtual Recipe RecipeNumberNavigation { get; set; }
    }
}
