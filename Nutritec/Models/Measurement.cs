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
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public float? Hips { get; set; }
        public float? Waist { get; set; }
        public float? Neck { get; set; }
        public float? FatPercentage { get; set; }
        public float? MusclePercentage { get; set; }

        public virtual Patient PatientEmailNavigation { get; set; }
    }
}
