using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class Measurement
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string PatientEmail { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public int? Hips { get; set; }
        public int? Waist { get; set; }
        public int? Neck { get; set; }
        public int? FatPercentage { get; set; }
        public int? MusclePercentage { get; set; }

        public virtual Patient PatientEmailNavigation { get; set; }
    }
}
