using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Authentication
{
    public class ApplicationUser: IdentityUser
    {
        [ForeignKey("User")]
        public int? UserID { get; set; }
        public virtual MyUser User { get; set; }

        [ForeignKey("Admin")]
        public int? AdminID { get; set; }
        public virtual Admin Admin { get; set; }
    }
}
