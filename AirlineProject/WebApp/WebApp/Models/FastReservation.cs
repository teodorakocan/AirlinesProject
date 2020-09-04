using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class FastReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string StartDestination { get; set; }
        public DateTime StartDateTime { get; set; } //datum i vreme polaska
        public double Discount { get; set; }
        public int SeatNumber { get; set; }
        public string Destination { get; set; } //odredistna destinacija

        [ForeignKey("Airline")]
        public int AirlineID { get; set; }
        public Airline Airline { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public MyUser User { get; set; }
        
    }
}
