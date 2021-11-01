using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            ConsumesRecipes = new HashSet<ConsumesRecipe>();
            RecipeHas = new HashSet<RecipeHa>();
        }

        public int Number { get; set; }
        public string Name { get; set; }
        public string PatientEmail { get; set; }

        public virtual Patient PatientEmailNavigation { get; set; }
        public virtual ICollection<ConsumesRecipe> ConsumesRecipes { get; set; }
        public virtual ICollection<RecipeHa> RecipeHas { get; set; }
    }
}
