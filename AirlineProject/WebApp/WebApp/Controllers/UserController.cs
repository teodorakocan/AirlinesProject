using System;
using System.Collections.Generic;
using System.Linq;
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
                    if(u.Email.Equals(email))
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

        [HttpPut]
        [Route("edit-profile/{email}")]
        public async Task<IActionResult> EditProfile(string email, EditModel edit)
        {
            List<Admin> admins = new List<Admin>();
            admins = _context.Admins.ToList();
            List<MyUser> users = new List<MyUser>();
            users = _context.MyUsers.ToList();

            foreach(Admin admin in admins)
            {
                if(admin.Email.Equals(email))
                {
                    admin.Name = edit.Name;
                    admin.Surname = edit.Surname;
                    admin.City = edit.City;
                    admin.Phone_Number = edit.PhoneNumber;

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
                    u.Phone_Number = edit.PhoneNumber;

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

            foreach(Admin admin in admins)
            {
                if(admin.Email.Equals(email))
                {
                    admin.Password = newPassword.NewPassword;

                    _context.Entry(admin).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    user.PasswordHash = userManager.PasswordHasher.HashPassword(user, newPassword.NewPassword);
                    var result = await userManager.UpdateAsync(user);
                    return Ok();
                }
            }

            foreach(MyUser u in users)
            {
                if(u.Email.Equals(email))
                {
                    u.Password = newPassword.NewPassword;

                    _context.Entry(u).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Server faild!" });
        }

    }
}
