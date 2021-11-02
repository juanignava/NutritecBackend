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

        public PatientController(NutritecContext context)
        {
            _context = context;
        }

        // Get patient by email or username
        [HttpGet("login/{credential}")]
        public async Task<ActionResult<Patient>> GetPatientByCredential(string credential)
        {
            // Use SQL query to search by email or username
            return await _context.Patients.FromSqlRaw($@"SELECT *
                                                    FROM PATIENT
                                                    WHERE Email = '{credential}' OR Username = '{credential}'").FirstOrDefaultAsync();
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
