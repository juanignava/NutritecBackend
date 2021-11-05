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
    public class MeasurementController : ControllerBase
    {
        private readonly NutritecContext _context;

        public MeasurementController()
        {
            _context = new NutritecContext();
        }

        // Get measurement by email address, filtering between dates (ME.1)
        [HttpGet("filterdates/{patientEmail}/{initialDate}/{endingDate}")]
        public async Task<IEnumerable<Measurement>> GetMeasurementsFilteredByDate(string patientEmail, string initialDate, string endingDate)
        {
            // Use sql query to filter the measurements
            return await _context.Measurements.FromSqlRaw($@"SELECT *
                                                            FROM MEASUREMENT
                                                            WHERE
                                                                PatientEmail = '{patientEmail}' AND
                                                                Date >= '{initialDate}' AND
                                                                Date <= '{endingDate}'
                                                            ORDER BY Date").ToListAsync();
        }

        // Get specific measurement based on the key values (ME.2)
        [HttpGet("{patientEmail}/{number}")]
        public async Task<ActionResult<Measurement>> GetSpecificMeasurement(string patientEmail, int number)
        {
            // Use sql query to get one measurement
            return await _context.Measurements.FromSqlRaw($@"SELECT *
                                                             FROM MEASUREMENT
                                                             WHERE
                                                                PatientEmail = '{patientEmail}' AND
                                                                Number = {number}").FirstOrDefaultAsync();
        }

        // Post measurement (ME.3)
        [HttpPost]
        public async Task<ActionResult> Add(Measurement measurement)
        {
            // Get the number of the measurement automatically
            var measurements = await _context.Measurements.FromSqlRaw($@"SELECT *
                                                                        FROM MEASUREMENT
                                                                        WHERE PatientEmail = '{measurement.PatientEmail}'").ToListAsync();
            int max = 0;
            if (measurements == null) max = 1;

            else
            {
                foreach (var measure in measurements)
                {
                    if (measure.Number > max) max = measure.Number;
                }
            }

            // Save the rest of the values
            int number = max + 1;
            var date = measurement.Date;
            string email = measurement.PatientEmail;
            float? height = measurement.Height;
            float? weight = measurement.Weight;
            float? hips = measurement.Hips;
            float? waist = measurement.Waist;
            float? neck = measurement.Neck;
            float? fatPercentage = measurement.FatPercentage;
            float? musclePercentage = measurement.MusclePercentage;

            // sql insertion
            await _context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO MEASUREMENT
                    (Number, Date, PatientEmail, Height, Weight, Hips, Waist, Neck, FatPercentage, MusclePercentage)
            VALUES  ({number}, {date}, {email}, {height}, {weight}, {hips}, {waist}, {neck}, {fatPercentage}, {musclePercentage})");

            // save the changes
            await _context.SaveChangesAsync();

            return Ok();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        }
    }
}
