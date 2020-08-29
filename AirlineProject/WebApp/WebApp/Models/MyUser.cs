using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MyUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string City { get; set; }
        public string Phone_Number { get; set; }
        public string Provider { get; set; }
        public string PictureURL { get; set; }
        public string IdToken { get; set; }


        [ForeignKey("Aircraft_Configuration")]
        public ICollection<AircraftConfiguration> Aircraft_Configurations { get; set; }

        [ForeignKey("Fast_Reservation")]
        public ICollection<FastReservation> Fast_Reservations { get; set; }

        [ForeignKey("Car_Reservations")]
        public ICollection<CarReservation> Car_Reservations { get; set; }

        public MyUser()
        {
            this.Aircraft_Configurations = new HashSet<AircraftConfiguration>();
            this.Fast_Reservations = new HashSet<FastReservation>();
            this.Car_Reservations = new HashSet<CarReservation>();
        }
    }
}
