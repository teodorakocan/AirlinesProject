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
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public MyUser User { get; set; }
    }
}
