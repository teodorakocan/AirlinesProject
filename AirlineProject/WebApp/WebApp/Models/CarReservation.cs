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

        [ForeignKey("VehicleID")]
        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public MyUser User { get; set; }

        [ForeignKey("RentACarID")]
        public int RentACarID { get; set; }
        public RentACar RentACar { get; set; }

        [ForeignKey("BranchID")]
        public int BranchID { get; set; }
        public Branch Branch { get; set; }
    }
}
