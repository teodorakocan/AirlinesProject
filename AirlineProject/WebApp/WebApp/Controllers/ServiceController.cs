﻿using System;
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
using WebApp.Models.ViewModel;

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
        public async Task<ActionResult<IEnumerable<ServiceViewModel>>> AllRentACarServices()
        {
            List<ServiceViewModel> services = new List<ServiceViewModel>();
            services = GetServiceViewModel();
            return services;
        }

        private List<ServiceViewModel> GetServiceViewModel()
        {
            List<ServiceViewModel> services = new List<ServiceViewModel>();
            List<RentACar> rentACars = new List<RentACar>();
            rentACars =  _context.RentACars.ToList();
            List<Branch> branches = new List<Branch>();
            branches =  _context.Branches.ToList();

            foreach (RentACar rentACar in rentACars)
            {
                foreach (Branch branch in branches)
                {
                    if (branch.RentACarID == rentACar.ID)
                    {
                        ServiceViewModel service = new ServiceViewModel()
                        {
                            ServiceID = rentACar.ID,
                            ServiceName = rentACar.Name,
                            ServiceAddress = rentACar.Address,
                            ServiceCity = rentACar.City,
                            ServiceState = rentACar.State,
                            ServicePromoDescription = rentACar.PromoDescription,
                            BranchId = branch.ID,
                            BranchName = branch.Name,
                            BranchAddress = branch.Address,
                            BranchCity = branch.City,
                            BranchState = branch.State,
                            NumberOfVehicle = branch.NumberOfVehicle
                        };
                        services.Add(service);
                    }
                }
            }

            return services;
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
                if (service.Name.Trim().Equals(name.Trim()))
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
            service.Branches.Add(branch);
            _context.Entry(service).State = EntityState.Modified;
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
        public async Task<ActionResult<IEnumerable<Vehicle>>> ServiceVehicle(int serviceId)
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

            return Ok(vehicleList);
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

            if (branch.NumberOfVehicle <= count)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "You can not add more vehicle!" });
            }

            vehicle.Branch = branch;
            vehicle.BranchID = branchId;
            _context.Vehicles.Add(vehicle);
            
            branch.Vehicles.Add(vehicle);
            _context.Entry(branch).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var branches = await _context.Branches.ToListAsync();

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
        [Route("search-vehicle")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> SearchVehicles(string brand, string vehicleClass, string numberOfSeats, string vehiclePrice, int id)
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

            var serviceVehicle = new List<Vehicle>();
            serviceVehicle = ServiceVehicles(id);
            var vehicles = from vehicle in serviceVehicle
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

        private List<Vehicle> ServiceVehicles(int serviceId)
        {
            List<Branch> branches = new List<Branch>();
            List<Branch> serviceBranches = new List<Branch>();
            branches = _context.Branches.ToList();
            foreach (Branch branch in branches)
            {
                if (branch.RentACarID == serviceId)
                {
                    serviceBranches.Add(branch);
                }
            }

            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = _context.Vehicles.ToList();
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
        [Route("search-service")]
        public async Task<ActionResult<IEnumerable<ServiceViewModel>>> SearchService(string name, string city, DateTime StartDate, DateTime EndDate)
        {
            List<ServiceViewModel> allServices = new List<ServiceViewModel>();
            List<RentACar> services = new List<RentACar>();
            services = _context.RentACars.ToList();
            List<CarReservation> reservations = new List<CarReservation>(); 
            reservations = await _context.CarReservations.ToListAsync();
            int branchId = 0;
            
            if (name == null && city == null && StartDate.Date.Year == 1 && EndDate.Date.Year == 1)
            {
                allServices = GetServiceViewModel();
                return Ok(allServices);
            }

            if(name != null && city != null && StartDate.Date.Year != 1 && EndDate.Date.Year != 1)
            {
                services = services.FindAll(s => s.Name.Contains(name) && s.City.Contains(city));
                foreach (var service in services)
                {
                    List<Branch> branches = FindBranch(service.ID);
                    foreach (Branch branch in branches)
                    {
                        foreach (CarReservation reservation in reservations)
                        {
                            bool unavailable = true;

                            if (reservation.BranchID == branch.ID)
                            {
                                if (StartDate <= reservation.ReservationFrom && reservation.ReservationTo <= EndDate)
                                {
                                    unavailable = false;
                                }
                            }
                            if (unavailable)
                            {
                                ServiceViewModel svm = new ServiceViewModel()
                                {
                                    ServiceID = service.ID,
                                    ServiceName = service.Name,
                                    ServiceAddress = service.Address,
                                    ServiceCity = service.City,
                                    ServiceState = service.State,
                                    ServicePromoDescription = service.PromoDescription,
                                    BranchId = branch.ID,
                                    BranchName = branch.Name,
                                    BranchAddress = branch.Address,
                                    BranchCity = branch.City,
                                    BranchState = branch.State,
                                    NumberOfVehicle = branch.NumberOfVehicle
                                };
                                allServices.Add(svm);
                            }

                        }
                    }
                }

            }
            else if( name != null && city != null)
            {
                services = services.FindAll(s => s.Name.Contains(name) && s.City.Contains(city));
                foreach(RentACar service in services)
                {
                    List<Branch> branches = FindBranch(service.ID);
                    foreach (Branch branch in branches)
                    {
                        ServiceViewModel svm = new ServiceViewModel()
                        {
                            ServiceID = service.ID,
                            ServiceName = service.Name,
                            ServiceAddress = service.Address,
                            ServiceCity = service.City,
                            ServiceState = service.State,
                            ServicePromoDescription = service.PromoDescription,
                            BranchId = branch.ID,
                            BranchName = branch.Name,
                            BranchAddress = branch.Address,
                            BranchCity = branch.City,
                            BranchState = branch.State,
                            NumberOfVehicle = branch.NumberOfVehicle
                        };
                        allServices.Add(svm);
                    }
                }
            }
            else if (name != null && StartDate.Date.Year != 1 && EndDate.Date.Year != 1)
            {
                services = services.FindAll(s => s.Name.Contains(name));
                foreach (var service in services)
                {
                    List<Branch> branches = FindBranch(service.ID);
                    foreach (Branch branch in branches)
                    {
                        foreach (CarReservation reservation in reservations)
                        {
                            bool unavailable = true;

                            if (reservation.BranchID == branch.ID)
                            {
                                if (StartDate <= reservation.ReservationFrom && reservation.ReservationTo <= EndDate)
                                {
                                    unavailable = false;
                                }
                            }
                            if (unavailable)
                            {
                                ServiceViewModel svm = new ServiceViewModel()
                                {
                                    ServiceID = service.ID,
                                    ServiceName = service.Name,
                                    ServiceAddress = service.Address,
                                    ServiceCity = service.City,
                                    ServiceState = service.State,
                                    ServicePromoDescription = service.PromoDescription,
                                    BranchId = branch.ID,
                                    BranchName = branch.Name,
                                    BranchAddress = branch.Address,
                                    BranchCity = branch.City,
                                    BranchState = branch.State,
                                    NumberOfVehicle = branch.NumberOfVehicle
                                };
                                allServices.Add(svm);
                            }

                        }
                    }
                }
            }
            else if (city != null && StartDate.Date.Year != 1 && EndDate.Date.Year != 1)
            {
                services = services.FindAll(s => s.City.Contains(city));
                foreach (var service in services)
                {
                    List<Branch> branches = FindBranch(service.ID);
                    foreach (Branch branch in branches)
                    {
                        foreach (CarReservation reservation in reservations)
                        {
                            bool unavailable = true;

                            if (reservation.BranchID == branch.ID)
                            {
                                if (StartDate <= reservation.ReservationFrom && reservation.ReservationTo <= EndDate)
                                {
                                    unavailable = false;
                                }
                            }
                            if (unavailable)
                            {
                                ServiceViewModel svm = new ServiceViewModel()
                                {
                                    ServiceID = service.ID,
                                    ServiceName = service.Name,
                                    ServiceAddress = service.Address,
                                    ServiceCity = service.City,
                                    ServiceState = service.State,
                                    ServicePromoDescription = service.PromoDescription,
                                    BranchId = branch.ID,
                                    BranchName = branch.Name,
                                    BranchAddress = branch.Address,
                                    BranchCity = branch.City,
                                    BranchState = branch.State,
                                    NumberOfVehicle = branch.NumberOfVehicle
                                };
                                allServices.Add(svm);
                            }

                        }
                    }
                }
            }
            else if (name != null)
            {
                services = services.FindAll(s => s.Name.Contains(name));
                foreach (RentACar service in services)
                {
                    List<Branch> branches = FindBranch(service.ID);
                    foreach (Branch branch in branches)
                    {
                        ServiceViewModel svm = new ServiceViewModel()
                        {
                            ServiceID = service.ID,
                            ServiceName = service.Name,
                            ServiceAddress = service.Address,
                            ServiceCity = service.City,
                            ServiceState = service.State,
                            ServicePromoDescription = service.PromoDescription,
                            BranchId = branch.ID,
                            BranchName = branch.Name,
                            BranchAddress = branch.Address,
                            BranchCity = branch.City,
                            BranchState = branch.State,
                            NumberOfVehicle = branch.NumberOfVehicle
                        };
                        allServices.Add(svm);
                    }
                }
            }
            else if (city != null)
            {
                services = services.FindAll(s => s.City.Contains(city));
                foreach (RentACar service in services)
                {
                    List<Branch> branches = FindBranch(service.ID);
                    foreach (Branch branch in branches)
                    {
                        ServiceViewModel svm = new ServiceViewModel()
                        {
                            ServiceID = service.ID,
                            ServiceName = service.Name,
                            ServiceAddress = service.Address,
                            ServiceCity = service.City,
                            ServiceState = service.State,
                            ServicePromoDescription = service.PromoDescription,
                            BranchId = branch.ID,
                            BranchName = branch.Name,
                            BranchAddress = branch.Address,
                            BranchCity = branch.City,
                            BranchState = branch.State,
                            NumberOfVehicle = branch.NumberOfVehicle
                        };
                        allServices.Add(svm);
                    }
                }
            }
            else if (StartDate.Date.Year != 1 && EndDate.Date.Year != 1)
            {
                foreach (var service in services)
                {
                    List<Branch> branches = FindBranch(service.ID);
                    foreach (Branch branch in branches)
                    {
                        foreach (CarReservation reservation in reservations)
                        {
                            bool unavailable = true;

                            if (reservation.BranchID == branch.ID)
                            {
                                if (StartDate <= reservation.ReservationFrom && reservation.ReservationTo <= EndDate)
                                {
                                    unavailable = false;
                                }
                            }
                            if (unavailable)
                            {
                                ServiceViewModel svm = new ServiceViewModel()
                                {
                                    ServiceID = service.ID,
                                    ServiceName = service.Name,
                                    ServiceAddress = service.Address,
                                    ServiceCity = service.City,
                                    ServiceState = service.State,
                                    ServicePromoDescription = service.PromoDescription,
                                    BranchId = branch.ID,
                                    BranchName = branch.Name,
                                    BranchAddress = branch.Address,
                                    BranchCity = branch.City,
                                    BranchState = branch.State,
                                    NumberOfVehicle = branch.NumberOfVehicle
                                };
                                allServices.Add(svm);
                            }

                        }
                    }
                }
            }
            return Ok(allServices);
        }

        private List<Branch> FindBranch(int serviceId)
        {
            List<Branch> branches = new List<Branch>();
            List<Branch> serviceBranches = new List<Branch>();
            branches = _context.Branches.ToList();
            foreach(Branch branch in branches)
            {
                if(branch.RentACarID == serviceId)
                {
                    serviceBranches.Add(branch);
                }
            }
            return serviceBranches;
        }

        [HttpGet]
        [Route("search-vehicles")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> SearchAvailableVehicle(int branchId, string brand, string price, string numberOfSeats, DateTime dateFrom, DateTime dateTo)
        {
            double vehiclePrice = 0;
            int nos = 0;
            if (numberOfSeats != null)
            {
                nos = Int32.Parse(numberOfSeats);
            }

            if (price != null)
            {
                vehiclePrice = double.Parse(price);
            }
            List<CarReservation> carReservations = new List<CarReservation>();
            carReservations = await _context.CarReservations.ToListAsync();
            var branchVehicle = new List<Vehicle>();
            branchVehicle = BranchVehicles(branchId); //vozila proslednjene filijale

            List<Vehicle> availableVehicles = new List<Vehicle>();

            if(brand == null && numberOfSeats == null && price == null && dateFrom.Date.Year == 1 && dateTo.Date.Year == 1)
            {
                return Ok(branchVehicle);
            }
            foreach (Vehicle vehicle in branchVehicle)
            {
                if (brand != null && numberOfSeats != null && price != null && dateFrom.Date.Year != 1 && dateTo.Date.Year != 1)
                {
                    if (vehicle.Brand.Contains(brand) && vehicle.Price.Equals(vehiclePrice) && vehicle.NumberOfSeats.Equals(nos))
                    {
                        foreach (CarReservation reservation in carReservations)
                        {
                            bool unavailable = true;
                            if (reservation.VehicleID == vehicle.ID)
                            {
                                if (dateFrom <= reservation.ReservationFrom && reservation.ReservationTo <= dateTo)
                                {
                                    unavailable = false;
                                }
                            }
                            else
                            {
                                if (unavailable)
                                {
                                    TimeSpan totalDateReservation = new TimeSpan();
                                    totalDateReservation = reservation.ReservationTo - reservation.ReservationFrom;
                                    int totalPrice = totalDateReservation.Days * (int)vehicle.Price;
                                    vehicle.Price = totalPrice;
                                    availableVehicles.Add(vehicle);
                                }
                            }
                        }
                    }
                }
                else if (brand != null && price != null && numberOfSeats != null)
                {
                    if (vehicle.Brand.Contains(brand) && vehicle.Price.Equals(vehiclePrice) && vehicle.NumberOfSeats.Equals(nos))
                    {
                        availableVehicles.Add(vehicle);
                    }
                }
                else if (brand != null && numberOfSeats != null && dateFrom.Date.Year != 1 && dateTo.Date.Year != 1)
                {
                    if (vehicle.Brand.Contains(brand) && vehicle.NumberOfSeats.Equals(nos))
                    {
                        foreach (CarReservation reservation in carReservations)
                        {
                            bool unavailable = true;
                            if (reservation.VehicleID == vehicle.ID)
                            {
                                if (dateFrom <= reservation.ReservationFrom && reservation.ReservationTo <= dateTo)
                                {
                                    unavailable = false;
                                }
                            }
                            else
                            {
                                if (unavailable)
                                {
                                    TimeSpan totalDateReservation = new TimeSpan();
                                    totalDateReservation = reservation.ReservationTo - reservation.ReservationFrom;
                                    int totalPrice = totalDateReservation.Days * (int)vehicle.Price;
                                    vehicle.Price = totalPrice;
                                    availableVehicles.Add(vehicle);
                                }
                            }
                        }
                    }
                }
                else if (brand != null && price != null && dateFrom.Date.Year != 1 && dateTo.Date.Year != 1)
                {
                    if (vehicle.Brand.Contains(brand) && vehicle.Price.Equals(vehiclePrice))
                    {
                        foreach (CarReservation reservation in carReservations)
                        {
                            bool unavailable = true;
                            if (reservation.VehicleID == vehicle.ID)
                            {
                                if (dateFrom <= reservation.ReservationFrom && reservation.ReservationTo <= dateTo)
                                {
                                    unavailable = false;
                                }
                            }
                            else
                            {
                                if (unavailable)
                                {
                                    TimeSpan totalDateReservation = new TimeSpan();
                                    totalDateReservation = reservation.ReservationTo - reservation.ReservationFrom;
                                    int totalPrice = totalDateReservation.Days * (int)vehicle.Price;
                                    vehicle.Price = totalPrice;
                                    availableVehicles.Add(vehicle);
                                }
                            }
                        }
                    }
                }
                else if (numberOfSeats != null && price != null && dateFrom.Date.Year != 1 && dateTo.Date.Year != 1)
                {
                    if (vehicle.Price.Equals(vehiclePrice) && vehicle.NumberOfSeats.Equals(nos))
                    {
                        foreach (CarReservation reservation in carReservations)
                        {
                            bool unavailable = true;
                            if (reservation.VehicleID == vehicle.ID)
                            {
                                if (dateFrom <= reservation.ReservationFrom && reservation.ReservationTo <= dateTo)
                                {
                                    unavailable = false;
                                }
                            }
                            else
                            {
                                if (unavailable)
                                {
                                    TimeSpan totalDateReservation = new TimeSpan();
                                    totalDateReservation = reservation.ReservationTo - reservation.ReservationFrom;
                                    int totalPrice = totalDateReservation.Days * (int)vehicle.Price;
                                    vehicle.Price = totalPrice;
                                    availableVehicles.Add(vehicle);
                                }
                            }
                        }
                    }
                }
                else if (price != null && dateFrom.Date.Year != 1 && dateTo.Date.Year != 1)
                {
                    if (vehicle.Price.Equals(vehiclePrice))
                    {
                        foreach (CarReservation reservation in carReservations)
                        {
                            bool unavailable = true;
                            if (reservation.VehicleID == vehicle.ID)
                            {
                                if (dateFrom <= reservation.ReservationFrom && reservation.ReservationTo <= dateTo)
                                {
                                    unavailable = false; ;
                                }
                            }
                            else
                            {
                                if (unavailable)
                                {
                                    TimeSpan totalDateReservation = new TimeSpan();
                                    totalDateReservation = reservation.ReservationTo - reservation.ReservationFrom;
                                    int totalPrice = totalDateReservation.Days * (int)vehicle.Price;
                                    vehicle.Price = totalPrice;
                                    availableVehicles.Add(vehicle);
                                }
                            }
                        }
                    }
                }
                else if (numberOfSeats != null && dateFrom.Date.Year != 1 && dateTo.Date.Year != 1)
                {
                    if (vehicle.NumberOfSeats.Equals(nos))
                    {
                        foreach (CarReservation reservation in carReservations)
                        {
                            bool unavailable = true;
                            if (reservation.VehicleID == vehicle.ID)
                            {
                                if (dateFrom <= reservation.ReservationFrom && reservation.ReservationTo <= dateTo)
                                {
                                    unavailable = false;
                                }
                            }
                            else
                            {
                                if (unavailable)
                                {
                                    TimeSpan totalDateReservation = new TimeSpan();
                                    totalDateReservation = reservation.ReservationTo - reservation.ReservationFrom;
                                    int totalPrice = totalDateReservation.Days * (int)vehicle.Price;
                                    vehicle.Price = totalPrice;
                                    availableVehicles.Add(vehicle);
                                }
                            }
                        }
                    }
                }
                else if (brand != null && dateFrom.Date.Year != 1 && dateTo.Date.Year != 1)
                {
                    if (vehicle.Brand.Contains(brand))
                    {
                        foreach (CarReservation reservation in carReservations)
                        {
                            bool unavailable = true;
                            if (reservation.VehicleID == vehicle.ID)
                            {
                                if (dateFrom <= reservation.ReservationFrom && reservation.ReservationTo <= dateTo)
                                {
                                    unavailable = false;
                                }
                            }
                            else
                            {
                                if (unavailable)
                                {
                                    TimeSpan totalDateReservation = new TimeSpan();
                                    totalDateReservation = reservation.ReservationTo - reservation.ReservationFrom;
                                    int totalPrice = totalDateReservation.Days * (int)vehicle.Price;
                                    vehicle.Price = totalPrice;
                                    availableVehicles.Add(vehicle);
                                }
                            }
                        }
                    }
                }
                else if (brand != null && numberOfSeats != null)
                {
                    if (vehicle.Brand.Contains(brand) && vehicle.NumberOfSeats.Equals(nos))
                    {
                        availableVehicles.Add(vehicle);
                    }
                }
                else if (brand != null && price != null)
                {
                    if (vehicle.Brand.Contains(brand) && vehicle.Price.Equals(vehiclePrice))
                    {
                        availableVehicles.Add(vehicle);
                    }
                }
                else if (numberOfSeats != null && price != null)
                {
                    if (vehicle.NumberOfSeats.Equals(nos) && vehicle.Price.Equals(vehiclePrice))
                    {
                        availableVehicles.Add(vehicle);
                    }
                }
                else if (brand != null)
                {
                    if (vehicle.Brand.Contains(brand))
                    {
                        availableVehicles.Add(vehicle);
                    }
                }
                else if (price != null)
                {
                    if (vehicle.Price.Equals(vehiclePrice))
                    {
                        availableVehicles.Add(vehicle);
                    }
                }
                else if (numberOfSeats != null)
                {
                    if (vehicle.NumberOfSeats.Equals(nos))
                    {
                        availableVehicles.Add(vehicle);
                    }
                }
                else if (dateFrom.Date.Year != 1 && dateTo.Date.Year != 1)
                {
                    foreach (CarReservation reservation in carReservations)
                    {
                        bool unavailable = true;
                        if (reservation.VehicleID == vehicle.ID)
                        {
                            if (dateFrom <= reservation.ReservationFrom && reservation.ReservationTo <= dateTo)
                            {
                                unavailable = false;
                            }
                        }
                        else
                        {
                            if (unavailable)
                            {
                                TimeSpan totalDateReservation = new TimeSpan();
                                totalDateReservation = reservation.ReservationTo - reservation.ReservationFrom;
                                int totalPrice = totalDateReservation.Days * (int)vehicle.Price;
                                vehicle.Price = totalPrice;
                                availableVehicles.Add(vehicle);
                            }
                        }
                    }
                }

            }

            return Ok(availableVehicles);
        }

        private List<Vehicle> BranchVehicles(int branchId)
        {
            List<Branch> allBranches = new List<Branch>();
            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = _context.Vehicles.ToList();
            List<Vehicle> vehicleList = new List<Vehicle>();
            allBranches = _context.Branches.ToList();

            foreach (Branch branch in allBranches)
            {
                if (branch.ID == branchId)
                {
                    foreach(Vehicle vehicle in vehicles)
                    {
                        if(vehicle.BranchID == branch.ID)
                        {
                            vehicleList.Add(vehicle);
                        }
                    }
                }
            }

            return vehicleList;
        }

        [HttpPut]
        [Route("reserve-vehicle/{email}/{branchId}/{vehicleId}")]
        public async Task<ActionResult<int>> ReserveAVehicle(string email, int branchId, int vehicleId, CarReservation reservation)
        {
            if(reservation.ReservationFrom.Date.Year == 1 && reservation.ReservationTo.Date.Year == 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "You have to pick a date!" });
            }

            Branch branch = _context.Branches.Find(branchId);
            RentACar rentacar = _context.RentACars.Find(branch.RentACarID);
            var user = await userManager.FindByEmailAsync(email);
            MyUser myUser = _context.MyUsers.Find(user.UserID);
            Vehicle vehicle = _context.Vehicles.Find(vehicleId);
            myUser.Points += 10;
            _context.Entry(myUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            CarReservation newReservation = new CarReservation()
            {
                ReservationFrom = reservation.ReservationFrom,
                ReservationTo = reservation.ReservationTo,
                BranchID = branch.ID,
                VehicleID = vehicle.ID,
                UserID = myUser.ID,
                RentACarID = rentacar.ID
            };
            _context.CarReservations.Add(newReservation);
            _context.SaveChanges();

            List<Discounts> discounts = new List<Discounts>();
            int totalPrice = 0;
            TimeSpan totalDateReservation = new TimeSpan();
            totalDateReservation = reservation.ReservationTo - reservation.ReservationFrom;

            foreach (Discounts discount in discounts)
            {
                if(myUser.Points == discount.Points)
                {
                    totalPrice = (totalDateReservation.Days * (int)vehicle.Price) * discount.Discount/100;
                }
            }
            
            if(totalPrice == 0)
            {
                totalPrice = totalDateReservation.Days * (int)vehicle.Price;
            }
            
            return Ok(totalPrice);
        }

        [HttpGet]
        [Route("service-reservations/{serviceId}")]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> ServiceReservations(int serviceId)
        {
            List<CarReservation> carReservations = new List<CarReservation>();
            carReservations = await _context.CarReservations.ToListAsync();
            List<ReservationViewModel> listOfReservations = new List<ReservationViewModel>();
            foreach(CarReservation carReservation in carReservations)
            {
                carReservations = carReservations.FindAll(reservation => reservation.RentACarID == serviceId);
            }

            foreach(CarReservation carReservation in carReservations)
            {
                RentACar service = _context.RentACars.Find(carReservation.RentACarID);
                MyUser user = _context.MyUsers.Find(carReservation.UserID);
                Branch branch = _context.Branches.Find(carReservation.BranchID);
                ReservationViewModel reservation = new ReservationViewModel()
                {
                    ReservationFrom = carReservation.ReservationFrom,
                    ReservationTo = carReservation.ReservationTo,
                    ServiceName = service.Name,
                    BranchName = branch.Name,
                    UserName = user.Name + " " + user.Surname
                };
                listOfReservations.Add(reservation);
            }
            
            return Ok(listOfReservations);
        }
    }
}
