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
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PromoDescription { get; set; }

        [ForeignKey("FastReservation")]
        public virtual ICollection<FastReservation> FastReservations { get; set; }
        
        [ForeignKey("AirlineDestinationConnections")]
        public virtual ICollection<AirlineDestinationConnection> AirlineDestinationConnections { get; set; }

        [ForeignKey("Price_List")]
        public virtual ICollection<PriceList> Prise_Lists { get; set; }

        public Airline()
        {
            this.FastReservations = new HashSet<FastReservation>();
            this.AirlineDestinationConnections = new HashSet<AirlineDestinationConnection>();
            this.Prise_Lists = new HashSet<PriceList>();
        }
    }
}
