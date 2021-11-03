using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nutritec.Models
{
    public class NutritionistReport
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public int CreditCardNumber { get; set; }
        public int Payment { get; set; }
        public float Discount { get; set; }
        public float Amount { get; set; }
    }
}
