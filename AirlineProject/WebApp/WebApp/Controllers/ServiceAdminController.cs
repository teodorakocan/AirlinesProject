using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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
    public class ServiceAdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public ServiceAdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [HttpPut]
        [Route("add-service/{email}")]
        public async Task<IActionResult> AddService(string email, RentACar service)
        {
            List<Admin> admins = new List<Admin>();
            admins = await _context.Admins.ToListAsync();
            List<RentACar> rentacarlist = new List<RentACar>();
            foreach(RentACar rentACar in rentacarlist)
            {
                if(rentACar.Name.Equals(service.Name))
                {
                    return NotFound();
                }
            }

            foreach(Admin admin in admins)
            {
                if(admin.Email.Equals(email))
                {
                    if (admin.Rent_a_Car_ID == null)
                    {
                        _context.RentACars.Add(service);
                        await _context.SaveChangesAsync();

                        admin.Rent_a_Car_ID = service.ID;
                        admin.Rent_a_Car = service;

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
            foreach(Admin admin in admins)
            {
                if(admin.Email.Equals(email))
                {
                    var service = await _context.RentACars.FindAsync(admin.Rent_a_Car_ID);
                    if(service == null)
                    {
                        return null;
                    }
                    return Ok(service);
                }
            }
            return null;
        }

        [HttpGet]
        [Route("branches")]
        public async Task<IEnumerable<Branch>> Branches(int serviceId)
        {
            List<Branch> branches = new List<Branch>();
            List<Branch> branchList = new List<Branch>();
            branches = await _context.Branches.ToListAsync();
            foreach(Branch branch in branches)
            {
                if(branch.Rent_a_Car_ID == serviceId)
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
            branch.Rent_a_Car = service;
            branch.Rent_a_Car_ID = serviceId;
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPut]
        [Route("add-vehicle/{branchId}")]
        public async Task<IActionResult> AddVehicle(int branchId, Vehicle vehicle)
        {
            Branch branch = await _context.Branches.FindAsync(branchId);
            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = await _context.Vehicles.ToListAsync();
            int count = 0;
            foreach(Vehicle vehicle1 in vehicles)
            {
                if(vehicle1.Branch_ID == branchId)
                {
                    count++;
                }
            }

            if(count > branch.Number_Of_Vehicle)
            {
                return BadRequest();
            }

            vehicle.Branch = branch;
            vehicle.Branch_ID = branchId;
            _context.Vehicles.Add(vehicle);
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
            foreach(Vehicle vehicle in vehicles)
            {
                if(vehicle.Branch_ID == branchId)
                {
                    vehicleList.Add(vehicle);
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

        [HttpGet]
        [Route("vehicle/{vehicleId}")]
        public async Task<ActionResult<Vehicle>> GetVehicleInfo(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);

            return vehicle;
        }

        [HttpPut]
        [Route("edit-branch/{branchId}")]
        public async Task<ActionResult<Branch>> EditBranch(int branchId, Branch branch)
        {
            Branch oldBranch = new Branch();
            oldBranch = await _context.Branches.FindAsync(branchId);

            if(!branch.Name.Equals(oldBranch.Name))
            {
                List<Branch> branches = new List<Branch>();
                foreach(Branch b in branches)
                {
                    if(b.Name.Equals(branch.Name))
                    {
                        return BadRequest();
                    }
                }
            }
            oldBranch.Name = branch.Name;
            oldBranch.Address = branch.Address;
            oldBranch.Number_Of_Vehicle = branch.Number_Of_Vehicle;

            _context.Entry(oldBranch).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = await _context.Vehicles.ToListAsync();
            foreach(Vehicle vehicle in vehicles)
            {
                if(vehicle.Branch_ID == branchId)
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
            foreach(Vehicle vehicle in vehicles)
            {
                if(vehicle.Branch_ID == branchId)
                {
                    _context.Vehicles.Remove(vehicle);
                }
            }

            Branch branch = await _context.Branches.FindAsync(branchId);
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("delete-service/{serviceId}")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            List<Branch> branches = new List<Branch>();
            branches = await _context.Branches.ToListAsync();
            foreach(Branch branch in branches)
            {
                if(branch.Rent_a_Car_ID == serviceId)
                {
                    List<Vehicle> vehicles = new List<Vehicle>();
                    vehicles = await _context.Vehicles.ToListAsync();
                    foreach (Vehicle vehicle in vehicles)
                    {
                        if (vehicle.Branch_ID == branch.ID)
                        {
                            _context.Vehicles.Remove(vehicle);
                        }
                    }
                    await _context.Branches.FindAsync(branch.ID);
                }
            }

            RentACar rentACar = await _context.RentACars.FindAsync(serviceId);
            _context.RentACars.Remove(rentACar);
            await _context.SaveChangesAsync();

            return Ok();
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
            Vehicle vehicle = await _context.Vehicles.FindAsync(vehicleId);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("edit-service/{serviceId}")]
        public async Task<ActionResult<Vehicle>> EditService(int serviceId, RentACar service)
        {
            RentACar oldService = await _context.RentACars.FindAsync(serviceId);
            oldService.Name = service.Name;
            oldService.Promo_Description = service.Promo_Description;
            oldService.Address = service.Address;

            _context.Entry(oldService).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
