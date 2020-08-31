using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Brand { get; set; }
        public int NumberOfSeats { get; set; }
        public string Class { get; set; }
        public bool Reserved { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        [ForeignKey("Branch")]
        public int Branch_ID { get; set; }
        public Branch Branch { get; set; }

        [ForeignKey("Car_Reservations")]
        public ICollection<CarReservation> Car_Reservations { get; set; }

        public Vehicle()
        {
            this.Car_Reservations = new HashSet<CarReservation>();
        }

    }
}
