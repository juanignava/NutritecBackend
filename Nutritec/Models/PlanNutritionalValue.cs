using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nutritec.Models
{
    public class PlanNutritionalValue
    {
        public int Number { get; set; }
        public double TotalSodium { get; set; }
        public double TotalCarbohydrates { get; set; }
        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalIron { get; set; }
        public double TotalCalcium { get; set; }
        public double TotalCalories { get; set; }
    }
}
