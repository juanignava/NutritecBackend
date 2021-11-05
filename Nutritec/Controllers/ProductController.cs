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
    public class ProductController : ControllerBase
    {
        private readonly NutritecContext _context;

        public ProductController()
        {
            _context = new NutritecContext();
        }

        // Get specific product by Barcode (PR.2)
        [HttpGet("{barcode}")]
        public async Task<ActionResult<Product>> GetPrdoctByBarcode(int barcode)
        {
            // Use SQL query to search by barcode
            return await _context.Products.FromSqlRaw(@$"SELECT *
                                                         FROM PRODUCT
                                                         WHERE Barcode = {barcode}").FirstOrDefaultAsync();
        }

        // Get all products related to a plan number by mealtime (PR.5)
        [HttpGet("byplan/{planNumber}/{mealtime}")]
        public async Task<IEnumerable<PlanProductView>> GetProductByPlan(int planNumber, string mealtime)
        {
            // Use sql query to search in view
            return await _context.PlanProductViews.FromSqlRaw($@"SELECT *
                                                                FROM PLAN_PRODUCT_VIEW
                                                                WHERE Number = {planNumber} AND Mealtime = '{mealtime}'").ToListAsync();

        }

        // Get all products that are NOT related to a plan number by mealtime (PR.6)
        [HttpGet("notinplan/{planNumber}/{mealtime}")]
        public async Task<IEnumerable<Product>> GetProductNotInPlan(int planNumber, string mealtime)
        {
            // Use sql query to search in view
            return await _context.Products.FromSqlRaw($@"SELECT *
                                                         FROM PRODUCT
                                                         WHERE Barcode Not in
                                                            (SELECT Barcode
                                                            FROM PLAN_PRODUCT_VIEW
                                                            WHERE Number = {planNumber} AND Mealtime = '{mealtime}')").ToListAsync();

        }

        // Get all products bases on the state (Pending, Approved or Declined) (PR.1)
        [HttpGet("state/{state}")]
        public async Task<IEnumerable<Product>> GetPendingProducts(string state)
        {
            // Use SQL query to select pending products
            return await _context.Products.FromSqlRaw(@$"SELECT *
                                                         FROM PRODUCT 
                                                         WHERE Approved = '{state}'").ToListAsync();
        }

        // Get consumed products and recipes consumed by patient email, consuptoin day and meal
        [HttpGet("consumption/{patientEmail}/{day}/{meal}")]
        public async Task<IEnumerable<ConsumptionModel>> GetConsumptionDetails(string patientEmail, string day, string meal)
        {
            // use sql query to get the consumption details
            return await _context.ConsumptionModels.FromSqlRaw($@"SELECT * 
                                                                  FROM PATIENT_RECIPES
                                                                  WHERE Email = '{patientEmail}' AND Day = '{day}' AND Meal = '{meal}'
                                                                  
                                                                  UNION
                
                                                                  SELECT * 
                                                                  FROM PATIENT_PRODUCTS
                                                                  WHERE Email = '{patientEmail}' AND Day = '{day}' AND Meal = '{meal}'").ToListAsync();
        }

        // Add product in the list of consumption
        [HttpPost("consumption/addproduct/{barcode}/{patientEmail}/{day}/{meal}/{servings}")]
        public async Task<ActionResult> AddProductToConsumption(int barcode, string patientEmail, string day, string meal, int servings)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO CONSUMES_PRODUCT
                                            (ProductBarcode, PatientEmail, Day, Meal, Servings)
                                VALUES      ({barcode}, {patientEmail}, {day}, {meal}, {servings})");

            // save the changes
            await _context.SaveChangesAsync();

            return Ok();
        }


        // Update product Approved state (PR.3)
        [HttpPut("Approved/{barcode}/{state}")]
        public async Task<ActionResult> UpdateProductApprovedState(int barcode, string state)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"UPDATE PRODUCT
                                                                   SET Approved = {state}
                                                                   WHERE Barcode = {barcode}");

            await _context.SaveChangesAsync();

            return Ok();
        }

        // Create relation between a product and a plan
        [HttpPost("newproductinplan/{planNumber}/{productBarcode}/{mealtime}/{servings}")]
        public async Task<ActionResult> AddProductInPlan(int planNumber, int productBarcode, string mealtime, int servings)
        {
            // sql insertion
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO PLAN_HAS
                        (PlanNumber, ProductBarcode, Mealtime, Servings)
                VALUES  ({planNumber}, {productBarcode}, {mealtime}, {servings})");

            // save the changes
            await _context.SaveChangesAsync();

            return Ok();
        }


        // Post a product (PR.4)
        [HttpPost]
        public async Task<ActionResult> Add(Product product)
        {

            // First check if the patient doesn't exist
            var itemToAdd = await _context.Products.FromSqlRaw(@$"SELECT * 
                                                                FROM Product
                                                                WHERE Barcode = '{product.Barcode}'").FirstOrDefaultAsync();

            if (itemToAdd != null) return Conflict();

            // define barcode automatically
            var products = await _context.Products.FromSqlRaw("SELECT * FROM PRODUCT").ToListAsync();

            int max = 0;
            foreach (var prod in products)
            {
                if (prod.Barcode > max) max = prod.Barcode;
            }

            // save the values
            int barcode = max+1;
            string approved = "Pending";
            string name = product.Name;
            string description = product.Description;
            float sodium = product.Sodium;
            float carbohydrates = product.Carbohydrates;
            float protein = product.Protein;
            float fat = product.Fat;
            float iron = product.Iron;
            float calcium = product.Calcium;
            float calories = product.Calories;

            // sql insertion
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO PRODUCT
                (Barcode, Approved, Name, Description, Sodium, Carbohydrates, Protein, Fat, Iron, Calcium, Calories)
            VALUES ({barcode}, {approved}, {name}, {description}, {sodium}, {carbohydrates}, {protein}, {fat}, {iron},
                    {calcium}, {calories})");

            // save the changes
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
