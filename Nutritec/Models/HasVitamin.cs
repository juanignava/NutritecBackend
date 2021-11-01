using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class HasVitamin
    {
        public int ProductBarcode { get; set; }
        public string VitaminCode { get; set; }

        public virtual Product ProductBarcodeNavigation { get; set; }
        public virtual Vitamin VitaminCodeNavigation { get; set; }
    }
}
