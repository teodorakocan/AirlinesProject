using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApp.Authentication;
using WebApp.Models;
using WebApp.Models.ViewModel;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("user-info/{email}")]
        public async Task<ActionResult<Object>> GetUserInfo(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                List<MyUser> users = new List<MyUser>();
                users = _context.MyUsers.ToList();
                foreach (MyUser u in users)
                {
                    if (u.Email.Equals(email))
                    {
                        return u;
                    }
                }

                List<Admin> admins = new List<Admin>();
                admins = _context.Admins.ToList();
                foreach (Admin admin in admins)
                {
                    if (admin.Email.Equals(email))
                    {
                        return admin;
                    }
                }
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("delete-account/{email}")]
        public async Task<IActionResult> DeleteAccount(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
                List<MyUser> users = new List<MyUser>();
                users = _context.MyUsers.ToList();
                foreach (MyUser u in users)
                {
                    if (u.Email.Equals(email))
                    {
                        _context.MyUsers.Remove(u);
                    }
                }

                List<Admin> admins = new List<Admin>();
                admins = _context.Admins.ToList();
                foreach (Admin admin in admins)
                {
                    if (admin.Email.Equals(email))
                    {
                        _context.Admins.Remove(admin);
                    }
                }
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        [Route("edit-profile/{email}")]
        public async Task<IActionResult> EditProfile(string email, EditModel edit)
        {
            List<Admin> admins = new List<Admin>();
            admins = _context.Admins.ToList();
            List<MyUser> users = new List<MyUser>();
            users = _context.MyUsers.ToList();

            foreach (Admin admin in admins)
            {
                if (admin.Email.Equals(email))
                {
                    admin.Name = edit.Name;
                    admin.Surname = edit.Surname;
                    admin.City = edit.City;
                    admin.PhoneNumber = edit.PhoneNumber;

                    _context.Entry(admin).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            foreach (MyUser u in users)
            {
                if (u.Email.Equals(email))
                {
                    u.Name = edit.Name;
                    u.Surname = edit.Surname;
                    u.City = edit.City;
                    u.PhoneNumber = edit.PhoneNumber;

                    _context.Entry(u).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Server faild!" });
        }

        [HttpPut]
        [Route("change-password/{email}")]
        public async Task<IActionResult> ChangePassword(string email, PasswordModel newPassword, string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            List<Admin> admins = new List<Admin>();
            admins = _context.Admins.ToList();
            List<MyUser> users = new List<MyUser>();
            users = _context.MyUsers.ToList();

            foreach (Admin admin in admins)
            {
                if (admin.Email.Equals(email))
                {
                    admin.Password = newPassword.NewPassword;

                    _context.Entry(admin).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    user.PasswordHash = userManager.PasswordHasher.HashPassword(user, newPassword.NewPassword);
                    var result = await userManager.UpdateAsync(user);
                    return Ok();
                }
            }

            foreach (MyUser u in users)
            {
                if (u.Email.Equals(email))
                {
                    u.Password = newPassword.NewPassword;

                    _context.Entry(u).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Server faild!" });
        }

        [HttpGet]
        [Route("user-reservations/{email}")]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> MyReservations(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            DateTime date = new DateTime();
            date = DateTime.Now;

            List<CarReservation> carReservations = new List<CarReservation>();
            carReservations = await _context.CarReservations.ToListAsync();
            List<ReservationViewModel> listOfReservations = new List<ReservationViewModel>();

            foreach (CarReservation carReservation in carReservations)
            {
                if (carReservation.UserID == user.UserID && carReservation.ReservationTo > date)
                {
                    RentACar service = _context.RentACars.Find(carReservation.RentACarID);
                    MyUser myUser = _context.MyUsers.Find(carReservation.UserID);
                    Branch branch = _context.Branches.Find(carReservation.BranchID);
                    ReservationViewModel reservation = new ReservationViewModel()
                    {
                        ReservationFrom = carReservation.ReservationFrom,
                        ReservationTo = carReservation.ReservationTo,
                        ServiceName = service.Name,
                        BranchName = branch.Name,
                        UserName = myUser.Name + " " + myUser.Surname
                    };
                    listOfReservations.Add(reservation);
                }
            }
            return Ok(listOfReservations);
        }

        [HttpGet]
        [Route("old-reservations/{email}")]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> OldReservations(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            DateTime date = new DateTime();
            date = DateTime.Now;

            List<CarReservation> carReservations = new List<CarReservation>();
            carReservations = await _context.CarReservations.ToListAsync();
            List<ReservationViewModel> listOfReservations = new List<ReservationViewModel>();

            foreach (CarReservation carReservation in carReservations)
            {
                if (carReservation.UserID == user.UserID && carReservation.ReservationTo < date)
                {
                    RentACar service = _context.RentACars.Find(carReservation.RentACarID);
                    MyUser myUser = _context.MyUsers.Find(carReservation.UserID);
                    Branch branch = _context.Branches.Find(carReservation.BranchID);
                    ReservationViewModel reservation = new ReservationViewModel()
                    {
                        ReservationFrom = carReservation.ReservationFrom,
                        ReservationTo = carReservation.ReservationTo,
                        ServiceName = service.Name,
                        BranchName = branch.Name,
                        UserName = myUser.Name + " " + myUser.Surname
                    };
                    listOfReservations.Add(reservation);
                }
            }
            return Ok(listOfReservations);
        }

        [HttpGet]
        [Route("service-for-rating/{email}")]
        public async Task<ActionResult<IEnumerable<RentACar>>> ServiceForRaiting(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            List<CarReservation> reservations = new List<CarReservation>();
            reservations = await _context.CarReservations.ToListAsync();
            List<RentACar> services = new List<RentACar>();
            DateTime date = new DateTime();
            date = DateTime.Now;
           
            foreach(CarReservation reservation in reservations)
            {
                if(reservation.UserID == user.UserID)
                {
                    if (reservation.ReservationFrom <= date)
                    {
                        RentACar rentacar = _context.RentACars.Find(reservation.RentACarID);
                        services.Add(rentacar);
                    }
                }
            }

            return Ok(services);
        }

        [HttpPost]
        [Route("rate-service/{mark}/{serviceId}/{email}")]
        public async Task<ActionResult<IEnumerable<RentACar>>> RateService(int mark, int serviceId, string email)
        {
            List<MyUser> users = await _context.MyUsers.ToListAsync();
            MyUser myUser = new MyUser();
            foreach(MyUser user in users)
            {
                if(user.Email.Equals(email))
                {
                    myUser = user;
                }    
            }

            RentACarRaiting rentACarRaiting = new RentACarRaiting()
            {
                Mark = mark,
                RentACarID = serviceId,
                UserID = myUser.ID
            };

            _context.RentACarRaitings.Add(rentACarRaiting);
            await _context.SaveChangesAsync();

            List<RentACarRaiting> raitings = new List<RentACarRaiting>();
            raitings = await _context.RentACarRaitings.ToListAsync();
            List<RentACar> leftServices = new List<RentACar>();
            foreach(RentACarRaiting rate in raitings)
            {
                if(rate.RentACarID !=serviceId)
                {
                    RentACar rentACar = _context.RentACars.Find(serviceId);
                    leftServices.Add(rentACar);
                }
            }

            return Ok(leftServices);
        }
    }
}
