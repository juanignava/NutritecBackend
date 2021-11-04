using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class PatientController : ControllerBase
    {
        private readonly NutritecContext _context;

        public PatientController()
        {
            _context = new NutritecContext();
        }

        // Get patient by email or username (PA.1)
        [HttpGet("login/{credential}")]
        public async Task<ActionResult<Patient>> GetPatientByCredential(string credential)
        {
            // Use SQL query to search by email or username
            return await _context.Patients.FromSqlRaw(@$"SELECT * FROM PATIENT WHERE Email = '{credential}' OR Username = '{credential}'").FirstOrDefaultAsync();
        }

        // Get not associate patient by email portion
        [HttpGet("unassociated/{emailtext}")]
        public async Task<IEnumerable<Patient>> GetUnassociatedByEmail(string emailtext)
        {
            // Use SQL query to get the result
            return await _context.Patients.FromSqlRaw(@$"SELECT *
                                                        FROM PATIENT
                                    WHERE NutritionistEmail IS NULL AND Email Like '%{emailtext}%'").ToListAsync();

        }

        // Get associate patient to a nutritionist by email portion
        [HttpGet("associated/{nutritionistEmail}/{emailtext}")]
        public async Task<IEnumerable<Patient>> GetAssociatedByEmail(string nutritionistEmail, string emailtext)
        {
            // Use SQL query to get the result
            return await _context.Patients.FromSqlRaw(@$"SELECT *
                                                        FROM PATIENT
                                    WHERE NutritionistEmail = '{nutritionistEmail}' AND Email Like '%{emailtext}%'").ToListAsync();

        }

        // Get associated patients to a nutritionist 
        [HttpGet("associated/{nutritionistemail}")]
        public async Task<IEnumerable<Patient>> GetAssociatedByNutritionist(string nutritionistemail)
        {
            // Use SQL query to get the result
            return await _context.Patients.FromSqlRaw(@$"SELECT *
                                                        FROM PATIENT
                                    WHERE NutritionistEmail = '{nutritionistemail}'").ToListAsync();

        }

        // Update the nutritionist associated to the patient
        [HttpPut("associate/{patientEmail}/{nutritionistEmail}")]
        public async Task<ActionResult> UpdatePatientNutritionist(string patientEmail, string nutritionistEmail)
        {
            // Use sql query to update the value
            await _context.Database.ExecuteSqlInterpolatedAsync(@$"UPDATE PATIENT
                                                                   SET NutritionistEmail = {nutritionistEmail}
                                                                   WHERE Email = {patientEmail}");

            await _context.SaveChangesAsync();

            return Ok();
        }

        // Post a patient
        [HttpPost]
        public async Task<ActionResult> Add(Patient patient)
        {


            // First check there if the nutritionist doesn't exist
            var itemToAdd = await _context.Patients.FromSqlRaw(@$"SELECT * 
                                                                FROM PATIENT
                                                                WHERE Email = '{patient.Email}'").FirstOrDefaultAsync();


            if (itemToAdd != null) return Conflict();

            // Then add the nutritionist to the database

            // save the values
            string email = patient.Email;
            string username = patient.Username;
            string firstName = patient.FirstName;
            string lastName1 = patient.LastName1;
            string lastname2 = patient.LastName2;
            var birthDate = patient.BirthDate;

            // Convert password into md5
            string password = patient.Passowrd;
            using (MD5 md5hash = MD5.Create())
            {
                password = GetMd5Hash(md5hash, password);
            }

            // sql insertion

            await _context.Database.ExecuteSqlInterpolatedAsync(@$"INSERT INTO PATIENT 
                    (Email, Username, FirstName, LastName1, LastName2, BirthDate, Passowrd)
            VALUES ({email}, {username}, {firstName}, {lastName1}, {lastname2}, {birthDate}, {password})");


            // save the changes
            await _context.SaveChangesAsync();

            return Ok();
        }

        // convert password into md5
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
