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
        //public string ImageURL { get; set; }

        [ForeignKey("AirlineDestinationConnections")]
        public virtual ICollection<AirlineDestinationConnection> AirlineDestinationConnections { get; set; }

        [ForeignKey("Flight")]
        public virtual ICollection<Flight> Flights { get; set; }

        public Destination()
        {
            this.AirlineDestinationConnections = new HashSet<AirlineDestinationConnection>();
            this.Flights = new HashSet<Flight>();
        }
    }
}
