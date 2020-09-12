using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Authentication;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public ServiceController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("service-names")]
        public async Task<ActionResult<IEnumerable<string>>> GetServiceName()
        {
            List<RentACar> services = new List<RentACar>();
            services = await  _context.RentACars.ToListAsync();
            List<string> names = new List<string>();

            foreach (RentACar service in services)
            {
                names.Add(service.Name);
            }

            return names;
        }

        [HttpGet]
        [Route("all-services")]
        public async Task<ActionResult<IEnumerable<RentACar>>> AllRentACarServices()
        {
            return await _context.RentACars.ToListAsync();
        }

        [HttpPut]
        [Route("add-service/{email}")]
        public async Task<IActionResult> AddService(string email, RentACar service)
        {
            List<Admin> admins = new List<Admin>();
            admins = await _context.Admins.ToListAsync();
            List<RentACar> rentacarlist = new List<RentACar>();
            foreach (RentACar rentACar in rentacarlist)
            {
                if (rentACar.Name.Equals(service.Name))
                {
                    return NotFound();
                }
            }

            foreach (Admin admin in admins)
            {
                if (admin.Email.Equals(email))
                {
                    if (admin.RentACarID == null)
                    {
                        _context.RentACars.Add(service);
                        await _context.SaveChangesAsync();

                        admin.RentACarID = service.ID;
                        admin.RentACar = service;

                        _context.Entry(admin).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        return Ok();
                    }
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Server faild!" });
        }

        [HttpGet]
        [Route("myservice")]
        public async Task<ActionResult<RentACar>> MyService(string email)
        {
            List<Admin> admins = new List<Admin>();
            admins = await _context.Admins.ToListAsync();
            foreach (Admin admin in admins)
            {
                if (admin.Email.Equals(email))
                {
                    var service = await _context.RentACars.FindAsync(admin.RentACarID);
                    if (service == null)
                    {
                        return null;
                    }
                    return Ok(service);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("service-info/{name}")]
        public async Task<ActionResult<RentACar>> GetServiceInfo(string name)
        {
            List<RentACar> services = new List<RentACar>();
            services = await _context.RentACars.ToListAsync();
            List<string> names = new List<string>();

            foreach (RentACar service in services)
            {
                if (service.Name.Equals(name))
                {
                    return service;
                }
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("delete-service/{serviceId}")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            List<Admin> admins = new List<Admin>();
            admins = await _context.Admins.ToListAsync();
            foreach(Admin admin in admins)
            {
                if(admin.RentACarID == serviceId)
                {
                    admin.RentACar = null;
                    admin.RentACarID = null;
                    _context.Entry(admin).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }

            List<Branch> branches = new List<Branch>();
            branches = await _context.Branches.ToListAsync();
            foreach (Branch branch in branches)
            {
                if (branch.RentACarID == serviceId)
                {
                    List<Vehicle> vehicles = new List<Vehicle>();
                    vehicles = await _context.Vehicles.ToListAsync();
                    foreach (Vehicle vehicle in vehicles)
                    {
                        if (vehicle.BranchID == branch.ID)
                        {
                            _context.Vehicles.Remove(vehicle);
                            await _context.SaveChangesAsync();
                        }
                    }
                    _context.Branches.Remove(branch);
                    await _context.SaveChangesAsync();
                }
            }

            RentACar rentACar = await _context.RentACars.FindAsync(serviceId);
            _context.RentACars.Remove(rentACar);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("edit-service/{serviceId}")]
        public async Task<ActionResult<Vehicle>> EditService(int serviceId, RentACar service)
        {
            RentACar oldService = await _context.RentACars.FindAsync(serviceId);
            if(!oldService.Name.Equals(service.Name))
            {
                List<RentACar> services = new List<RentACar>();
                foreach (RentACar s in services)
                {
                    if (s.Name.Equals(service.Name))
                    {
                        return BadRequest();
                    }
                }
            }
            oldService.Name = service.Name;
            oldService.PromoDescription = service.PromoDescription;
            oldService.Address = service.Address;

            _context.Entry(oldService).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("branches")]
        public async Task<IEnumerable<Branch>> Branches(int serviceId)
        {
            List<Branch> branches = new List<Branch>();
            List<Branch> branchList = new List<Branch>();
            branches = await _context.Branches.ToListAsync();
            foreach (Branch branch in branches)
            {
                if (branch.RentACarID == serviceId)
                {
                    branchList.Add(branch);
                }
            }

            return branchList;
        }

        [HttpPut]
        [Route("add-branch/{serviceId}")]
        public async Task<IActionResult> AddBranchOffice(int serviceId, Branch branch)
        {
            List<Branch> branchlist = new List<Branch>();
            foreach (Branch b in branchlist)
            {
                if (b.Name.Equals(branch.Name))
                {
                    return NotFound();
                }
            }

            RentACar service = await _context.RentACars.FindAsync(serviceId);
            branch.RentACar = service;
            branch.RentACarID = serviceId;
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("branch-vehicle/{branchId}")]
        public async Task<IEnumerable<Vehicle>> BranchVehicle(int branchId)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = await _context.Vehicles.ToListAsync();
            List<Vehicle> vehicleList = new List<Vehicle>();
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.BranchID == branchId)
                {
                    vehicleList.Add(vehicle);
                }
            }

            return vehicleList;
        }

        [HttpGet]
        [Route("service-vehicle/{serviceId}")]
        public async Task<IEnumerable<Vehicle>> ServiceVehicle(int serviceId)
        {
            List<Branch> branches = new List<Branch>();
            List<Branch> serviceBranches = new List<Branch>();
            branches = await _context.Branches.ToListAsync();
            foreach (Branch branch in branches)
            {
                if (branch.RentACarID == serviceId)
                {
                    serviceBranches.Add(branch);
                }
            }

            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = await _context.Vehicles.ToListAsync();
            List<Vehicle> vehicleList = new List<Vehicle>();
            foreach (Vehicle vehicle in vehicles)
            {
                foreach (Branch serviceBranch in serviceBranches)
                {
                    if (vehicle.BranchID == serviceBranch.ID)
                    {
                        vehicleList.Add(vehicle);
                    }
                }
            }

            return vehicleList;
        }


        [HttpGet]
        [Route("branch/{branchId}")]
        public async Task<ActionResult<Branch>> GetBranchInfo(int branchId)
        {
            var branch = await _context.Branches.FindAsync(branchId);

            return branch;
        }

        [HttpPut]
        [Route("edit-branch/{branchId}")]
        public async Task<ActionResult<Branch>> EditBranch(int branchId, Branch branch)
        {
            Branch oldBranch = new Branch();
            oldBranch = await _context.Branches.FindAsync(branchId);

            if (!branch.Name.Equals(oldBranch.Name))
            {
                List<Branch> branches = new List<Branch>();
                foreach (Branch b in branches)
                {
                    if (b.Name.Equals(branch.Name))
                    {
                        return BadRequest();
                    }
                }
            }
            oldBranch.Name = branch.Name;
            oldBranch.Address = branch.Address;
            oldBranch.NumberOfVehicle = branch.NumberOfVehicle;
            oldBranch.City = branch.City;
            oldBranch.State = branch.State;

            _context.Entry(oldBranch).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = await _context.Vehicles.ToListAsync();
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.BranchID == branchId)
                {
                    vehicle.Branch = branch;

                    _context.Entry(vehicle).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }

            return Ok();
        }

        [HttpDelete]
        [Route("delete-branch/{branchId}")]
        public async Task<IActionResult> DeleteBranch(int branchId)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = await _context.Vehicles.ToListAsync();
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.BranchID == branchId)
                {
                    _context.Vehicles.Remove(vehicle);
                }
            }

            Branch branch = await _context.Branches.FindAsync(branchId);
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("vehicle")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> AllVehicle()
        {
            return await _context.Vehicles.ToListAsync();
        }

        [HttpPut]
        [Route("add-vehicle/{branchId}")]
        public async Task<IActionResult> AddVehicle(int branchId, Vehicle vehicle)
        {
            Branch branch = await _context.Branches.FindAsync(branchId);
            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = await _context.Vehicles.ToListAsync();
            int count = 0;
            foreach (Vehicle vehicle1 in vehicles)
            {
                if (vehicle1.BranchID == branchId)
                {
                    count++;
                }
            }

            if (count > branch.NumberOfVehicle)
            {
                return BadRequest();
            }

            vehicle.Branch = branch;
            vehicle.BranchID = branchId;
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("vehicle/{vehicleId}")]
        public async Task<ActionResult<Vehicle>> GetVehicleInfo(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);

            return vehicle;
        }

        [HttpPut]
        [Route("edit-vehicle/{vehicleId}")]
        public async Task<ActionResult<Vehicle>> EditVehicle(int vehicleId, Vehicle vehicle)
        {
            Vehicle oldVehicle = await _context.Vehicles.FindAsync(vehicleId);
            oldVehicle.Brand = vehicle.Brand;
            oldVehicle.NumberOfSeats = vehicle.NumberOfSeats;
            oldVehicle.Image = vehicle.Image;
            oldVehicle.Class = vehicle.Class;
            oldVehicle.Price = vehicle.Price;


            _context.Entry(oldVehicle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("delete-vehicle/{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            List<CarReservation> reservations = new List<CarReservation>();
            reservations = await _context.CarReservations.ToListAsync();
            foreach (CarReservation carReservation in reservations)
            {
                if (carReservation.VehicleID == vehicleId)
                {
                    return BadRequest();
                }
            }
            Vehicle vehicle = await _context.Vehicles.FindAsync(vehicleId);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("search-service")]
        public async Task<ActionResult<IEnumerable<RentACar>>> SearchService(string name, string city, string state)
        {
            List<RentACar> services = new List<RentACar>();
            List<RentACar> rentacarlist = new List<RentACar>();
            RentACar rentacar = new RentACar();
            rentacar = null;
            services = await _context.RentACars.ToListAsync();

            if(name == null && city == null && state == null)
            {
                return Ok(services);
            }

            if(name != null)
            {
                foreach(RentACar service in services)
                {
                    if (service.Name.Equals(name) || service.Name.ToLower() == name.ToLower())
                    {
                        rentacar = service;
                        rentacarlist.Add(rentacar);
                        break;
                    }
                }
            }

            if(city != null)
            {
                if(rentacar != null)
                {
                    if(rentacar.City.Equals(city) || rentacar.City.ToLower() == city.ToLower())
                    {
                        if(state != null)
                        {
                            if(rentacar.State.Equals(state) || rentacar.State.ToLower() == state.ToLower())
                            {
                                return Ok(rentacarlist);
                            }
                            return NotFound();
                        }
                        return Ok(rentacarlist);
                    }
                    return NotFound();
                }
                else
                {
                    foreach (RentACar service in services)
                    {
                        if (service.City.Equals(city) || service.City.ToLower() == city.ToLower())
                        {
                            if(state != null)
                            {
                                if(service.State.Equals(state) || service.State.ToLower() == state.ToLower())
                                {
                                    rentacarlist.Add(service);
                                    return Ok(rentacarlist);
                                }
                            }
                            rentacarlist.Add(service);
                            return Ok(rentacarlist);
                        }
                    }
                    return NotFound();
                }
            }

            if(state != null)
            {
                if(rentacar != null)
                {
                    if(rentacar.State.Equals(state) || rentacar.State.ToLower() == state.ToLower())
                    {
                        return Ok(rentacarlist);
                    }
                    return NotFound();
                }
                else
                {
                    foreach(RentACar service in services)
                    {
                        if (service.State.Equals(state) || service.State.ToLower() == state.ToLower())
                        {
                            rentacarlist.Add(service);
                            return Ok(rentacarlist);
                        }
                    }
                    return NotFound();
                }
            }

            return Ok(rentacarlist);
        }

        [HttpGet]
        [Route("search-vehicle")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> SearchVehicles(string brand, string vehicleClass, string numberOfSeats, string vehiclePrice)
        {
            double price = 0;
            int nos = 0;
            if (numberOfSeats != null)
            {
                nos = Int32.Parse(numberOfSeats);
            }

            if(vehiclePrice != null)
            {
                price = double.Parse(vehiclePrice);
            }

            var vehicles = from vehicle in _context.Vehicles
                         select vehicle;

            if (brand == null && vehicleClass == null && numberOfSeats == null && vehiclePrice == null)
            {
                return Ok(vehicles);
            }

            if(brand != null && vehicleClass != null && numberOfSeats != null && vehiclePrice != null)
            {
                vehicles = vehicles.Where(v => v.Brand.Contains(brand) && v.Class.Equals(vehicleClass) &&
                                               v.NumberOfSeats == nos && v.Price == price);
            }
            else if (brand != null && vehicleClass != null && nos != 0)
            {
                vehicles = vehicles.Where(v => v.Brand.Contains(brand) && v.Class.Equals(vehicleClass) &&
                                               v.NumberOfSeats == nos);
            }
            else if (brand != null && vehicleClass != null && price != 0)
            {
                vehicles = vehicles.Where(v => v.Brand.Contains(brand) && v.Class.Equals(vehicleClass) && v.Price == price);
            }
            else if (brand != null && nos != 0 && price != 0)
            {
                vehicles = vehicles.Where(v => v.Brand.Contains(brand) &&
                                               v.NumberOfSeats == nos && v.Price == price);
            }
            else if (vehicleClass != null && nos != 0 && price != 0)
            {
                vehicles = vehicles.Where(v => v.Class.Equals(vehicleClass) &&
                                               v.NumberOfSeats == nos && v.Price == price);
            }
            else if (brand != null && vehicleClass != null)
            {
                vehicles = vehicles.Where(v => v.Brand.Contains(brand) && v.Class.Equals(vehicleClass));
            }
            else if (brand != null && nos != 0)
            {
                vehicles = vehicles.Where(v => v.Brand.Contains(brand) && v.NumberOfSeats == nos);
            }
            else if (brand != null && price != 0)
            {
                vehicles = vehicles.Where(v => v.Brand.Contains(brand) && v.Price == price);
            }
            else if (vehicleClass != null && nos != 0)
            {
                vehicles = vehicles.Where(v => v.Class.Equals(vehicleClass) &&
                                               v.NumberOfSeats == nos);
            }
            else if (vehicleClass != null && price != 0)
            {
                vehicles = vehicles.Where(v => v.Class.Equals(vehicleClass) && v.Price == price);
            }
            else if (nos != 0 && price != 0)
            {
                vehicles = vehicles.Where(v => v.NumberOfSeats == nos && v.Price == price);
            }
            else if (brand != null)
            {
                vehicles = vehicles.Where(v => v.Brand.Contains(brand));
            }
            else if (vehicleClass != null)
            {
                vehicles = vehicles.Where(v => v.Class.Equals(vehicleClass));
            }
            else if (nos != 0)
            {
                vehicles = vehicles.Where(v => v.NumberOfSeats == nos);
            }
            else if(price != 0)
            {
                vehicles = vehicles.Where(v => v.Price == price);
            }

            return Ok(vehicles);
        }
    }
}
