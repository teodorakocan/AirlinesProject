using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Route("add-flight")]
        public async Task<IActionResult> NewFlight()
        {
            Destination destination1 = await _context.Destinations.FindAsync(1);
            Destination destination2 = await _context.Destinations.FindAsync(2);
            Destination destination3 = await _context.Destinations.FindAsync(3);
            Destination destination4 = await _context.Destinations.FindAsync(4);
            Destination destination5 = await _context.Destinations.FindAsync(5);
            Destination destination6 = await _context.Destinations.FindAsync(6);

            Flight flight1 = new Flight()
            {
                Start_DateTime = new DateTime(2020, 9, 29, 20, 00, 00),
                End_DateTime = new DateTime(2020, 9, 30, 19, 00, 00),
                Price = 390,
                Price2 = 390 * 2,
                Flight_Number = "109A",
                Number_Transfer = 1,
                Destination_Transfer = "Beograd-Berlin-Moskva",
                Travel_Length = new TimeSpan(23, 00, 00),
                Destination_ID = 1
            };

            Flight flight2 = new Flight()
            {
                Start_DateTime = new DateTime(2020, 10, 1, 14, 00, 00),
                End_DateTime = new DateTime(2020, 10, 2, 17, 00, 00),
                Price = 890,
                Price2 = 890 * 2,
                Flight_Number = "310B",
                Number_Transfer = 0,
                Destination_Transfer = "Beograd-London",
                Travel_Length = new TimeSpan(3, 00, 00),
                Destination_ID = 2
            };

            Flight flight3 = new Flight()
            {
                Start_DateTime = new DateTime(2020, 12, 28, 12, 00, 00),
                End_DateTime = new DateTime(2020, 12, 28, 18, 00, 00),
                Price = 550,
                Price2 = 550 * 2,
                Flight_Number = "145C",
                Number_Transfer = 0,
                Destination_Transfer = "Beograd-Barcelon",
                Travel_Length = new TimeSpan(6, 00, 00),
                Destination_ID = 3
            };

            Flight flight4 = new Flight()
            {
                Start_DateTime = new DateTime(2020, 10, 16, 15, 00, 00),
                End_DateTime = new DateTime(2020, 10, 16, 17, 00, 00),
                Price = 200,
                Price2 = 200 * 2,
                Flight_Number = "560D",
                Number_Transfer = 0,
                Destination_Transfer = "Budimpesta-Beograd",
                Travel_Length = new TimeSpan(2, 00, 00),
                Destination_ID = 5
            };

            Flight flight5 = new Flight()
            {
                Start_DateTime = new DateTime(2020, 8, 29, 16, 00, 00),
                End_DateTime = new DateTime(2020, 9, 1, 12, 00, 00),
                Price = 1200,
                Price2 = 1200 * 2,
                Flight_Number = "52D",
                Number_Transfer = 2,
                Destination_Transfer = "New York-Roma-Berlin-Barcelona",
                Travel_Length = new TimeSpan(12, 23, 62),
                Destination_ID = 3
            };

            Flight flight6 = new Flight()
            {
                Start_DateTime = new DateTime(2020, 9, 30, 17, 00, 00),
                End_DateTime = new DateTime(2020, 9, 30, 21, 00, 00),
                Price = 280,
                Price2 = 280 * 2,
                Flight_Number = "450C",
                Number_Transfer = 0,
                Destination_Transfer = "Beograd-Paris",
                Travel_Length = new TimeSpan(4, 00, 00),
                Destination_ID = 4
            };

            Flight flight7 = new Flight()
            {
                Start_DateTime = new DateTime(2020, 10, 12, 18, 00, 00),
                End_DateTime = new DateTime(2020, 10, 12, 22, 00, 00),
                Price = 150,
                Price2 = 150 * 2,
                Flight_Number = "160A",
                Number_Transfer = 0,
                Destination_Transfer = "Keln-Beograd",
                Travel_Length = new TimeSpan(4, 00, 00),
                Destination_ID = 5
            };

            Flight flight8 = new Flight()
            {
                Start_DateTime = new DateTime(2020, 10, 21, 08, 00, 00),
                End_DateTime = new DateTime(2020, 10, 21, 14, 00, 00),
                Price = 200,
                Price2 = 200 * 2,
                Flight_Number = "100E",
                Number_Transfer = 0,
                Destination_Transfer = "Paris-Lisbon",
                Travel_Length = new TimeSpan(6, 00, 00),
                Destination_ID = 6
            };

            _context.Flights.Add(flight1);
            _context.Flights.Add(flight2);
            _context.Flights.Add(flight3);
            _context.Flights.Add(flight4);
            _context.Flights.Add(flight5);
            _context.Flights.Add(flight6);
            _context.Flights.Add(flight7);
            _context.Flights.Add(flight8);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("airlines-name")]
        public async Task<ActionResult<IEnumerable<string>>> GetAirlinesName()
        {
            List<Airline> airlines = new List<Airline>();
            airlines = _context.Airlines.ToList();
            List<string> names = new List<string>();

            foreach (Airline airline in airlines)
            {
                names.Add(airline.Name);
            }

            return names;
        }

        [HttpGet]
        [Route("airiline_info/{name}")]
        public async Task<ActionResult<Airline>> AirlineInfo(string name)
        {
            List<Airline> airlines = new List<Airline>();
            airlines = _context.Airlines.ToList();
            foreach(Airline airline in airlines)
            {
                if(airline.Name.Equals(name))
                {
                    return airline;
                }
            }

            return NotFound();
        }
    }
}
