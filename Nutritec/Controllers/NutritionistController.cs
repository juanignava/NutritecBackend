using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nutritec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nutritec.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionistController : ControllerBase
    {
        private readonly NutritecContext _context;

        public NutritionistController()
        {
            _context = new NutritecContext();
        }


        /*

        // Get nutritionist password by email or username
        [HttpGet("login/{credential}")]

        public async Task<ActionResult<PasswordClass>> GetNutritionistPassword(string credential)
        {
            // Use SQL query to search by email
            return await _context.Passwords.FromSqlRaw($"SELECT Password FROM NUTRITIONIST WHERE Email = \'{credential}\'").FirstOrDefaultAsync();

            //return await _context.Nutritionists.SqlQuery($"SELECT FROM NUTRITIONIST WHERE Email={credential}").ToListAsync();
        }

        */

        // Get nutritionist report by charge type (NU.3)
        [HttpGet("report/{chargeType}")]
        public async Task<IEnumerable<NutritionistReport>> GetNutritionistReport(string chargeType)
        {
            // Use sql query that uses the store procedure
            return await _context.NutritionistReports.FromSqlRaw($"EXECUTE uspNutritionistReport {chargeType}").ToListAsync();
        }

        // Get nutritionist password by email or username (NU.1)
        [HttpGet("login/{credential}")]
        public async Task<ActionResult<Nutritionist>> GetNutritionistByCredential(string credential)
        {
            // Use SQL query to search by email or username
            return await _context.Nutritionists.FromSqlRaw(@$"SELECT *
                                                            FROM NUTRITIONIST 
                                                            WHERE Email = '{credential}' OR Username = '{credential}'").FirstOrDefaultAsync();
        }

        // Get all nutritionists
        [HttpGet]
        public async Task<IEnumerable<Nutritionist>> GetNutritionist()
        {
            return await _context.Nutritionists.FromSqlRaw("SELECT * FROM NUTRITIONIST").ToListAsync();
        }

        // Post a nutritionist (NU.1)
        [HttpPost]
        public async Task<ActionResult> Add(Nutritionist nutritionist)
        {


            // First check there if the nutritionist doesn't exist
            var itemToAdd = await _context.Nutritionists.FromSqlRaw(@$"SELECT * 
                                                                FROM NUTRITIONIST
                                                                WHERE Email = '{nutritionist.Email}'").FirstOrDefaultAsync();
            
            
            if (itemToAdd != null) return Conflict();

            // Then add the nutritionist to the database

            // save the values
            string email = nutritionist.Email;
            string username = nutritionist.Username;
            int nutritionistCode = nutritionist.NutritionistCode;
            int id = nutritionist.Id;
            int active = 1;
            string firstName = nutritionist.FirstName;
            string lastName1 = nutritionist.LastName1;
            string lastname2 = nutritionist.LastName2;
            var birthDate = nutritionist.BirthDate;

            // Convert password into md5
            string password = nutritionist.Password;
            using (MD5 md5hash = MD5.Create())
            {
                password = GetMd5Hash(md5hash, password);
            }
            string chargeType = nutritionist.ChargeType;
            float? weight = nutritionist.Weight;
            float? height = nutritionist.Height;
            int? creditCardNumber = nutritionist.CreditCardNumber;
            string country = nutritionist.Country;
            string province = nutritionist.Province;
            string canton = nutritionist.Canton;
            string pictureUrl = nutritionist.PrictureUrl;

            // sql insertion
            
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"INSERT INTO NUTRITIONIST 
                    (Email, Username, NutritionistCode, Id, Active, FirstName, LastName1, LastName2, BirthDate, Password, ChargeType, Weight, Height, CreditCardNumber, Country, Province, Canton, PrictureUrl)
            VALUES ({email}, {username}, {nutritionistCode}, {id}, {active}, {firstName}, {lastName1}, {lastname2}, {birthDate}, {password}, {chargeType}, {weight}, {height}, {creditCardNumber},
                    {country}, {province}, {canton}, {pictureUrl})");
            

            // save the changes
            await _context.SaveChangesAsync();

            return Ok();
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}