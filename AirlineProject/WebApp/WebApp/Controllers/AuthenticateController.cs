using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApp.Authentication;
using WebApp.Models;

namespace WebApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public AuthenticateController(IEmailSender emailSender, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!user.EmailConfirmed)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });
                }
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
               
                //var _role = token.Claims.Where(claim => claim.Type == "role");
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    role = userRoles[0],
                    expiration = token.ValidTo
                }) ;
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")] //registracija za obicne korisnike
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if(userExists != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });
            }

            MyUser new_user = new MyUser()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                City = model.City,
                Password = model.Password, // TODO: enkriptovati kasnije 
                Phone_Number = model.PhoneNumber,
                Points = 0
            };

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                User_ID = new_user.ID,
                User = new_user,
                Admin_ID = null,
                UserName = model.Name,
                EmailConfirmed = false
            };

            var result2 = await userManager.CreateAsync(user, model.Password);
            userManager.AddToRoleAsync(user, "User").Wait();
            string link = "http://localhost:4200/confirmation/" + model.Email;
            string htmlMessage = "<a href = '" + link + "'>Verify email</a>";
            await _emailSender.SendEmailAsync(model.Email, "Verify your email", htmlMessage);

            if (!result2.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("social-login")]
        public async Task<IActionResult> SocialLogin([FromBody] SocialLoginModel model)
        {
            if (model != null)
            {
                var userExists = await userManager.FindByEmailAsync(model.Email);
                var userRoles = await userManager.GetRolesAsync(userExists);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userExists.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                if (ModelState.IsValid)
                {
                    if (userExists != null)
                    {
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }

                    MyUser user = new MyUser()
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Email = model.Email,
                        Password = null,
                        City = null,
                        Phone_Number = null,
                        Provider = model.Provider,
                        PictureURL = model.PictureUrl,
                        IdToken = model.IdToken,
                        Points = 0
                    };

                    ApplicationUser admin = new ApplicationUser()
                    {
                        Email = model.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        User_ID = user.ID,
                        User = user,
                        Admin_ID = null,
                        UserName = model.Name
                    };

                    var result2 = await userManager.CreateAsync(admin, "");
                    userManager.AddToRoleAsync(admin, "User").Wait();

                    if (!result2.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
                    }

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }
            return Unauthorized();
        }

        [HttpPut]
        [Route("confirm-email/{email}")]
        public async Task<IActionResult> Confirmation(string email)
        {
            if(email == "")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Please create your account" });
            }
            var userExists = await userManager.FindByEmailAsync(email);
            if(userExists.EmailConfirmed)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "You already verified your email" });
            }
            else
            {
                userExists.EmailConfirmed = true;
            }
            
            _context.Entry(userExists).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
