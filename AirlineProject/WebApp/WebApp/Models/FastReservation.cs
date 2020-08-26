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
        public string Start_Destination { get; set; }
        public DateTime Start_DateTime { get; set; } //datum i vreme polaska
        public double Discount { get; set; }
        public int Seat_Number { get; set; }
        public string Destination { get; set; } //odredistna destinacija

        [ForeignKey("Airline")]
        public int Airline_ID { get; set; }
        public Airline Airline { get; set; }

        [ForeignKey("User")]
        public int User_ID { get; set; }
        public User User { get; set; }
        
    }
}
