using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class Nutritionist
    {
        public Nutritionist()
        {
            DailyPlans = new HashSet<DailyPlan>();
            Patients = new HashSet<Patient>();
        }

        public string Email { get; set; }
        public string Username { get; set; }
        public int NutritionistCode { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Password { get; set; }
        public string ChargeType { get; set; }
        public int? Weight { get; set; }
        public float? Height { get; set; }
        public int? CreditCardNumber { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Canton { get; set; }
        public string PictureUrl { get; set; }

        public virtual ICollection<DailyPlan> DailyPlans { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
