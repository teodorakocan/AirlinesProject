using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authentication;
using WebApp.Models;

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
            if(user != null)
            {
                List<MyUser> users = new List<MyUser>();
                users = _context.MyUsers.ToList();
                foreach(MyUser u in users)
                {
                    if(user.Email.Equals(email))
                    {
                        return u;
                    }
                }

                List<Admin> admins = new List<Admin>();
                admins = _context.Admins.ToList();
                foreach(Admin admin in admins)
                {
                    if(admin.Email.Equals(email))
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
    }
}
