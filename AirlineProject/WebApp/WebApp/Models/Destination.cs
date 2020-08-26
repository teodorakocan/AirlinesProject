using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey("Airline_Destination_Connections")]
        public ICollection<AirlineDestinationConnection> Airline_Destination_Connections { get; set; }

        [ForeignKey("Flight")]
        public ICollection<Flight> Flights { get; set; }

        public Destination()
        {
            this.Airline_Destination_Connections = new HashSet<AirlineDestinationConnection>();
            this.Flights = new HashSet<Flight>();
        }
    }
}
