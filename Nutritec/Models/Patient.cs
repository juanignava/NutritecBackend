using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class Patient
    {
        public Patient()
        {
            ConsumesProducts = new HashSet<ConsumesProduct>();
            ConsumesRecipes = new HashSet<ConsumesRecipe>();
            Follows = new HashSet<Follow>();
            Measurements = new HashSet<Measurement>();
            Recipes = new HashSet<Recipe>();
        }

        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Passowrd { get; set; }
        public string NutritionistEmail { get; set; }

        public virtual Nutritionist NutritionistEmailNavigation { get; set; }
        public virtual ICollection<ConsumesProduct> ConsumesProducts { get; set; }
        public virtual ICollection<ConsumesRecipe> ConsumesRecipes { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Measurement> Measurements { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
