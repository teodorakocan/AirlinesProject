using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CarReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Reservation_Period { get; set; }

        [ForeignKey("Vehicle")]
        public int Vehicle_ID { get; set; }
        public Vehicle Vehicle { get; set; }

        [ForeignKey("User")]
        public int User_ID { get; set; }
        public MyUser User { get; set; }
    }
}
