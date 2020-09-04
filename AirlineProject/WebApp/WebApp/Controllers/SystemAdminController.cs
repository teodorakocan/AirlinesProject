using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Authentication;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender _emailSender;

        public SystemAdminController(IEmailSender emailSender, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("register-admin/{role}")]
        public async Task<IActionResult> RegisterAdmin(string role, RegisterModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });
            }

            Admin service_admin = new Admin()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                City = model.City,
                Password = model.Password, // TODO: enkriptovati kasnije 
                PhoneNumber = model.PhoneNumber
            };

            ApplicationUser admin = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserID = null,
                AdminID = service_admin.ID,
                Admin = service_admin,
                UserName = model.Name,
                EmailConfirmed = false
            };

            var result2 = await userManager.CreateAsync(admin, model.Password);
            if (role.Equals("airline"))
            {
                userManager.AddToRoleAsync(admin, "Airline_Admin").Wait();
            }
            else if (role.Equals("rent-a-car"))
            {
                userManager.AddToRoleAsync(admin, "Service_Admin").Wait();
            }
            else
            {
                userManager.AddToRoleAsync(admin, "Admin").Wait();
            }

            string link = "http://localhost:4200/adminConfirmation/" + model.Email;
            string htmlMessage = "<p>Your account was made by admin system. Please go on link below and change your password. Now your password is " + model.Password + "</p><br/><a href = '" + link + "'>Verify email</a>";
            await _emailSender.SendEmailAsync(model.Email, "Wellcome on Fly-buy app", htmlMessage);


            if (!result2.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpGet]
        [Route("discounts")]
        public async Task<IEnumerable<Discounts>> GetDiscounts()
        {
            return await _context.Discounts.ToListAsync();
        }

        [HttpPut]
        [Route("edit-discount/{id}")]
        public async Task<ActionResult> EditDiscount(int id, Discounts newDiscount)
        {
            Discounts discount = new Discounts();
            discount = await _context.Discounts.FindAsync(id);
            if (newDiscount.Discount == 0 || newDiscount.Points == 0)
            {
                _context.Discounts.Remove(discount);
                await _context.SaveChangesAsync();

                return Ok();
            }

            discount.Discount = newDiscount.Discount;
            discount.Points = newDiscount.Points;

            _context.Entry(discount).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("add-discount")]
        public async Task<IActionResult> AddDiscount(Discounts discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("all-users")]
        public async Task<ActionResult<IEnumerable<Object>>> AllUsersAdmins()
        {
            List<Object> allUsers = new List<object>();
            List<MyUser> users = new List<MyUser>();
            users = await _context.MyUsers.ToListAsync();
            List<Admin> admins = new List<Admin>();
            admins = await _context.Admins.ToListAsync();

            foreach(MyUser user in users)
            {
                allUsers.Add(user);
            }    

            foreach(Admin admin in admins)
            {
                allUsers.Add(admin);
            }

            if(allUsers == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Bad Requset", Message = "There are no registered users." });
            }

            return Ok(allUsers);
        }

        [HttpGet]
        [Route("all-companies")]
        public async Task<ActionResult<IEnumerable<Object>>> AllComanies()
        {
            List<Object> allComapnies = new List<Object>();
            List<Airline> airlines = new List<Airline>();
            List<RentACar> rentacars = new List<RentACar>();
            airlines = await _context.Airlines.ToListAsync();
            rentacars = await _context.RentACars.ToListAsync();

            foreach(Airline airline in airlines)
            {
                allComapnies.Add(airline);
            }    
            foreach(RentACar rentacar in rentacars)
            {
                allComapnies.Add(rentacar);
            }

            if(allComapnies == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Bad Requset", Message = "There are no registered companies." });
            }

            return Ok(allComapnies);
        }
    }
}
