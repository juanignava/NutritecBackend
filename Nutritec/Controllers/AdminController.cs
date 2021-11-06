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
    public class AdminController : Controller
    {
        private readonly NutritecContext _context;

        public AdminController()
        {
            _context = new NutritecContext();
        }

        // Get admin by email or username (PA.1)
        [HttpGet("login/{credential}")]
        public async Task<ActionResult<Admin>> GetAdminByCredential(string credential)
        {
            // Use SQL query to search by email or username
            return await _context.Admins.FromSqlRaw(@$"SELECT * FROM ADMIN WHERE Email = '{credential}' OR Username = '{credential}'").FirstOrDefaultAsync();
        }
    }
}
