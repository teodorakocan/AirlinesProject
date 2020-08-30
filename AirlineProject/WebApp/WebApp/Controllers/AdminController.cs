using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("find-admin/{id}")]
        public async Task<IEnumerable<Admin>> FindAdmin(int id, string name, string surname)
        {
            List<Admin> admins = new List<Admin>();
            List<Admin> listAdmins = new List<Admin>();
            listAdmins = await _context.Admins.ToListAsync();
            int adminId = 0;
            string adminName = "";
            string adminSurname = "";
            if(id != 0)
            {
                adminId = id;
            }
            if(name != "")
            {
                adminName = name;
            }

            if(surname != "")
            {
                adminSurname = surname;
            }

            foreach(Admin admin in listAdmins)
            {
                if(admin.ID == adminId || admin.Name.Equals(adminName) || admin.Surname.Equals(adminSurname))
                {
                    admins.Add(admin);
                }
            }

            return admins;
        }

        [HttpGet]
        [Route("find-user/{id}")]
        public async Task<IEnumerable<MyUser>> FindUser(int id, string name, string surname)
        {
            List<MyUser> users = new List<MyUser>();
            List<MyUser> listUsers = new List<MyUser>();
            listUsers = await _context.MyUsers.ToListAsync();
            int userId = 0;
            string userName = "";
            string userSurname = "";
            if (id != 0)
            {
                userId = id;
            }
            if (name != "")
            {
                userName = name;
            }

            if (surname != "")
            {
                userSurname = surname;
            }

            foreach (MyUser user in listUsers)
            {
                if (user.ID == userId || user.Name.Equals(userName) || user.Surname.Equals(userSurname))
                {
                    users.Add(user);
                }
            }

            return users;
        }
    }
}
