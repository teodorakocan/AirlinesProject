using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class AirlineDestinationConnection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Airline")]
        public int Airline_ID { get; set; }
        public Airline Airline { get; set; }

        [ForeignKey("Destination")]
        public int Destination_ID { get; set; }
        public Destination Destination { get; set; }
    }
}
