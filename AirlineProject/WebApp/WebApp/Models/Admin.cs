using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("Airline")]
        public int? Airline_ID { get; set; }
        public Airline Airline { get; set; }

        [ForeignKey("RentACar")]
        public int? RentACarID { get; set; }
        public RentACar RentACar { get; set; }
    }
}
