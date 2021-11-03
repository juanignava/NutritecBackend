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

        // Get specific product by Barcode
        [HttpGet("{barcode}")]
        public async Task<ActionResult<Product>> GetPrdoctByBarcode(int barcode)
        {
            // Use SQL query to search by barcode
            return await _context.Products.FromSqlRaw(@$"SELECT *
                                                         FROM PRODUCT
                                                         WHERE Barcode = {barcode}").FirstOrDefaultAsync();
        }

        // Get all products bases on the state (Pending, Approved or Declined)
        [HttpGet("state/{state}")]
        public async Task<IEnumerable<Product>> GetPendingProducts(string state)
        {
            // Use SQL query to select pending products
            return await _context.Products.FromSqlRaw(@$"SELECT *
                                                         FROM PRODUCT 
                                                         WHERE Approved = '{state}'").ToListAsync();
        }


        // Update product Approved state
        [HttpPut("Approved/{barcode}/{state}")]
        public async Task<ActionResult> UpdateProductApprovedState(int barcode, string state)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"UPDATE PRODUCT
                                                                   SET Approved = {state}
                                                                   WHERE Barcode = {barcode}");

            await _context.SaveChangesAsync();

            return Ok();
        }

        // Post a product
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
