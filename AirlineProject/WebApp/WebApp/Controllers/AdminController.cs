using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authentication;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender _emailSender;

        public AdminController(IEmailSender emailSender, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
                Phone_Number = model.PhoneNumber
            };

            ApplicationUser admin = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                User_ID = null,
                Admin_ID = service_admin.ID,
                Admin = service_admin,
                UserName = model.Name,
                EmailConfirmed = false
            };

            var result2 = await userManager.CreateAsync(admin, model.Password);
            if(role.Equals("airline"))
            {
                userManager.AddToRoleAsync(admin, "Airline_Admin").Wait();
            }
            else
            {
                userManager.AddToRoleAsync(admin, "Service_Admin").Wait();
            }

            string link = "http://localhost:4200/confirmation/" + model.Email;
            string htmlMessage = "<p>Your account was made by admin system. Please go on link below and confirm your email. Your password is " + model.Password + "</p><br/><a href = '" + link + "'>Verify email</a>";
            await _emailSender.SendEmailAsync(model.Email, "Wellcome on Flu-buy app", htmlMessage);


            if (!result2.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpGet]
        [Route("all-admins")]
        public async Task<IEnumerable<Admin>> GetAllAdmins()
        {
            return _context.Admins.ToList();
        }
    }
}
