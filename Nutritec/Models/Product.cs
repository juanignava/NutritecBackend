using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class Product
    {
        public Product()
        {
            ConsumesProducts = new HashSet<ConsumesProduct>();
            HasVitamins = new HashSet<HasVitamin>();
            PlanHas = new HashSet<PlanHa>();
            RecipeHas = new HashSet<RecipeHa>();
        }

        public int Barcode { get; set; }
        public int Aprroved { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Sodium { get; set; }
        public float Carbohydrates { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Iron { get; set; }
        public float Calcium { get; set; }
        public float Calories { get; set; }

        public virtual ICollection<ConsumesProduct> ConsumesProducts { get; set; }
        public virtual ICollection<HasVitamin> HasVitamins { get; set; }
        public virtual ICollection<PlanHa> PlanHas { get; set; }
        public virtual ICollection<RecipeHa> RecipeHas { get; set; }
    }
}
