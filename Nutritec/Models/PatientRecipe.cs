using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class PatientRecipe
    {
        public string Email { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Day { get; set; }
        public string Meal { get; set; }
    }
}
