using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Airline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address_ { get; set; }
        public string Promo_Description { get; set; }

        [ForeignKey("Fast_Reservation")]
        public ICollection<FastReservation> Fast_Reservations { get; set; }
        
        [ForeignKey("Airline_Destination_Connections")]
        public ICollection<AirlineDestinationConnection> Airline_Destination_Connections { get; set; }

        [ForeignKey("Price_List")]
        public ICollection<PriceList> Prise_Lists { get; set; }

        public Airline()
        {
            this.Fast_Reservations = new HashSet<FastReservation>();
            this.Airline_Destination_Connections = new HashSet<AirlineDestinationConnection>();
            this.Prise_Lists = new HashSet<PriceList>();
        }
    }
}
