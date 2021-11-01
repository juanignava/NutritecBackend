using System;
using System.Collections.Generic;

#nullable disable

namespace Nutritec.Models
{
    public partial class Vitamin
    {
        public Vitamin()
        {
            HasVitamins = new HashSet<HasVitamin>();
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HasVitamin> HasVitamins { get; set; }
    }
}
