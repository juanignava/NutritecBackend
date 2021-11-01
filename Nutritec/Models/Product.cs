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
        public bool Aprroved { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sodium { get; set; }
        public int Carbohydrates { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Iron { get; set; }
        public int Calcium { get; set; }
        public int Calories { get; set; }

        public virtual ICollection<ConsumesProduct> ConsumesProducts { get; set; }
        public virtual ICollection<HasVitamin> HasVitamins { get; set; }
        public virtual ICollection<PlanHa> PlanHas { get; set; }
        public virtual ICollection<RecipeHa> RecipeHas { get; set; }
    }
}
