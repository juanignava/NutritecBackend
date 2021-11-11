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
    public class RecipeController : ControllerBase
    {
        private readonly NutritecContext _context;

        public RecipeController()
        {
            _context = new NutritecContext();
        }

        // Get recipes by the patient that made it (RE.1)
        [HttpGet("getbypatient/{patientEmail}")]
        public async Task<IEnumerable<Recipe>> GetRecipesFromCreator(string patientEmail)
        {
            // Use sql query that gets the recipes by creator
            return await _context.Recipes.FromSqlRaw($@"SELECT * 
                                                        FROM RECIPE
                                                        WHERE  PatientEmail = '{patientEmail}'").ToListAsync();
        }

        // Add recipe in the list of consumption (RE.2)
        [HttpPost("consumption/addrecipe/{number}/{patientEmail}/{day}/{meal}")]
        public async Task<ActionResult> AddRecipeToConsumption(int number, string patientEmail, string day, string meal)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO CONSUMES_RECIPE
                                            (RecipeNumber, PatientEmail, Day, Meal)
                                VALUES      ({number}, {patientEmail}, {day}, {meal})");

            // save the changes
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Get recipe by number (RE.3)
        [HttpGet("specific/{number}")]
        public async Task<ActionResult<Recipe>> GetRecipesByNumber(int number)
        {
            // Use sql query that gets a recipe by its number
            return await _context.Recipes.FromSqlRaw($@"SELECT *
                                                        FROM RECIPE
                                                        WHERE Number = {number}").FirstOrDefaultAsync();
        }

        // Get the products that are in the recipe (RE.4)
        [HttpGet("getproductsin/{number}")]
        public async Task<IEnumerable<Product>> GetProductsInRecipe(int number)
        {
            // Use sql query that gets a recipe by its number
            return await _context.Products.FromSqlRaw($@"SELECT DISTINCT P.*
                    FROM RECIPE_HAS AS RH JOIN PRODUCT AS P ON RH.ProductBarcode = P.Barcode
                    WHERE RH.RecipeNumber = {number}").ToListAsync();
        }

        // Get the products that are NOT in the recipe (RE.5)
        [HttpGet("getproductsnotin/{number}")]
        public async Task<IEnumerable<Product>> GetProductsNotInRecipe(int number)
        {
            // Use sql query that gets a recipe by its number
            return await _context.Products.FromSqlRaw($@"SELECT * FROM PRODUCT
                EXCEPT 
                SELECT DISTINCT P.*
                FROM RECIPE_HAS AS RH JOIN PRODUCT AS P ON RH.ProductBarcode = P.Barcode
                WHERE RH.RecipeNumber = {number}").ToListAsync();
        }

        // Get recipes that have not been consumed at a specific time (RE.7)
        [HttpGet("noconsumption/{patientEmail}/{day}/{meal}")]
        public async Task<IEnumerable<NoConsumptionRecipe>> GetNoConsumptionRecipes(string patientEmail, string day, string meal)
        {
            // use sql query to get the no consumption products
            return await _context.NoConsumptionRecipes.FromSqlRaw($@"EXECUTE uspRecipesNotConsumed
	                                                                @email = '{patientEmail}',
	                                                                @day = '{day}',
	                                                                @meal = '{meal}'").ToListAsync();
        }

        // Add product to recipe  (RE.6)
        [HttpPost("newproductinrecipe/{number}/{barcode}/{servings}")]
        public async Task<ActionResult> AddProductInRecipe(int number, int barcode,  int servings)
        {
            // sql insertion
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO RECIPE_HAS
                        (RecipeNumber, ProductBarcode, Servings)
                VALUES  ({number}, {barcode}, {servings})");

            // save the changes
            await _context.SaveChangesAsync();

            return Ok();
        }


        // Post a recipe (RE.3)
        [HttpPost("postrecipe/{name}/{patientEmail}")]
        public async Task<ActionResult> AddRecipe(string name, string patientEmail)
        {
            // deifne the number of the recipe
            var recipes = await _context.Recipes.FromSqlRaw($@"SELECT * FROM RECIPE").ToListAsync();

            int max = 0;
            foreach (var r in recipes)
            {
                if (r.Number > max) max = r.Number;
            }

            // save the values
            int number = max + 1;

            // sql insertion
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO RECIPE
                    (Number, Name, PatientEmail)
            VALUES  ({number}, {name}, {patientEmail})");

            // save the changes
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
