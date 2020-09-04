using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Authentication;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AirlineController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("airlines-name")]
        public async Task<ActionResult<IEnumerable<string>>> GetAirlinesName()
        {
            List<Airline> airlines = new List<Airline>();
            airlines = await _context.Airlines.ToListAsync();
            List<string> names = new List<string>();

            foreach (Airline airline in airlines)
            {
                names.Add(airline.Name);
            }

            return names;
        }

        [HttpGet]
        [Route("all-airlines")]
        public async Task<ActionResult<IEnumerable<Airline>>> AllAirlines()
        {
            return await _context.Airlines.ToListAsync();
        }
    }
}
