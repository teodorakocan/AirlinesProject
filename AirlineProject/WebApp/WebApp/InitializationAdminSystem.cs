using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Authentication;
using WebApp.Models;

namespace WebApp
{
    public static class InitializationAdminSystem
    {
        public static void SeedAdminSystem(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("teodorakocan@gmail.com").Result == null)
            {
                Admin admin_system = new Admin
                {
                    Name = "Teodora",
                    Surname = "Kocan",
                    Email = "teodorakocan@gmail.com",
                    City = "Ruma",
                    Password = "Teodora_1995",
                    PhoneNumber = "0695334123"
                };

                ApplicationUser admin = new ApplicationUser()
                {
                    Email = "teodorakocan@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserID = null,
                    AdminID = admin_system.ID,
                    Admin = admin_system,
                    UserName = "Teodora",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(admin, "Teodora_1995").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }
            }
        }
    }
}
