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

        // Get recipes by the patient that made it
        [HttpGet("getbypatient/{patientEmail}")]
        public async Task<IEnumerable<Recipe>> GetRecipesFromCreator(string patientEmail)
        {
            // Use sql query that gets the recipes by creator
            return await _context.Recipes.FromSqlRaw($@"SELECT * 
                                                        FROM RECIPE
                                                        WHERE  PatientEmail = '{patientEmail}'").ToListAsync();
        }

        // Get recipe by number 
        [HttpGet("specific/{number}")]
        public async Task<ActionResult<Recipe>> GetRecipesByNumber(int number)
        {
            // Use sql query that gets a recipe by its number
            return await _context.Recipes.FromSqlRaw($@"SELECT *
                                                        FROM RECIPE
                                                        WHERE Number = {number}").FirstOrDefaultAsync();
        }

        // Get the products that are in the recipe
        [HttpGet("getproductsin/{number}")]
        public async Task<IEnumerable<Product>> GetProductsInRecipe(int number)
        {
            // Use sql query that gets a recipe by its number
            return await _context.Products.FromSqlRaw($@"SELECT DISTINCT 
                P.Barcode, P.Approved, P.Name, P.Description, P.Sodium, P.Carbohydrates, P.Protein, P.Fat, P.Iron, P.Calcium, P.Calories
                FROM RECIPE_HAS AS RH JOIN PRODUCT AS P ON RH.ProductBarcode = P.Barcode
                WHERE RH.RecipeNumber = {number}").ToListAsync();
        }

        // Get the products that are NOT in the recipe
        [HttpGet("getproductsnotin/{number}")]
        public async Task<IEnumerable<Product>> GetProductsNotInRecipe(int number)
        {
            // Use sql query that gets a recipe by its number
            return await _context.Products.FromSqlRaw($@"SELECT * FROM PRODUCT
                EXCEPT 
                SELECT DISTINCT P.Barcode, P.Approved, P.Name, P.Description, P.Sodium, P.Carbohydrates, P.Protein, P.Fat, P.Iron, P.Calcium, P.Calories
                FROM RECIPE_HAS AS RH JOIN PRODUCT AS P ON RH.ProductBarcode = P.Barcode
                WHERE RH.RecipeNumber = {number}").ToListAsync();
        }

        // Add product to recipe
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
