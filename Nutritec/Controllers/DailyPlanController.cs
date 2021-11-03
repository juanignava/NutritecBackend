using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutritec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nutritec.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyPlanController : ControllerBase
    {
        private readonly NutritecContext _context;

        public DailyPlanController()
        {
            _context = new NutritecContext();
        }

        // Get all plans related to a nutritionist
        [HttpGet("bynutritionist/{nutritionist}")]
        public async Task<IEnumerable<DailyPlan>> GetPlansRelatedNutritionist(string nutritionist)
        {
            // Use SQL query that gets a plan by nutritionist
            return await _context.DailyPlans.FromSqlRaw(@$"SELECT *
                                                           FROM DAILY_PLAN
                                                           WHERE NutritionistEmail = '{nutritionist}'").ToListAsync();
        }

        // Get nutritional values of the plan
        [HttpGet("nutritionalvalue/{planNumber}")]
        public async Task<ActionResult<PlanNutritionalValue>> GetNutritionalValues(int planNumber)
        {
            // Use SQL query to get the nutritional values
            return await _context.PlanNutritionalValues.FromSqlRaw($"EXECUTE uspPlanDetails {planNumber}").FirstOrDefaultAsync();
        }

        // Post of a plan with just the name
        [HttpPost("{name}/{nutritionistEmail}")]
        public async Task<ActionResult> Add(string name, string nutritionistEmail)
        {
            // get a number for the plan
            var plans = await _context.DailyPlans.FromSqlRaw("SELECT * FROM DAILY_PLAN").ToListAsync();

            int max = 0;
            foreach(var p in plans)
            {
                if (p.Number > max) max = p.Number;
            }

            // save the values
            int number = max + 1;

            // sql of insertion
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO DAILY_PLAN 
                    (Number, Name, NutritionistEmail)
                    VALUES ({number}, {name}, {nutritionistEmail})");

            // save the changes
            await _context.SaveChangesAsync();

            return Ok();
            
        }
        
    }
}
