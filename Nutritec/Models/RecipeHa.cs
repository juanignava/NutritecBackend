using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class RecipeHa
    {
        public int RecipeNumber { get; set; }
        public int ProductBarcode { get; set; }
        public int Servings { get; set; }

        public virtual Product ProductBarcodeNavigation { get; set; }
        public virtual Recipe RecipeNumberNavigation { get; set; }
    }
}
