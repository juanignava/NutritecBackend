using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nutritec.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nutritec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NutritionistController : ControllerBase
    {
        private readonly NutritecContext _context;

        public NutritionistController(NutritecContext context)
        {
            _context = context;
        }


        // Get all nutritionists
        [HttpGet]
        public async Task<IEnumerable<Nutritionist>> GetNutritionist()
        {
            return await _context.Nutritionists.FromSqlRaw("SELECT * FROM NUTRITIONIST").ToListAsync();
        }
    }
}