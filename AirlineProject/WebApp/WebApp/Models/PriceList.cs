using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PriceList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [ForeignKey("Airline")]
        public int AirlineID { get; set; }
        public Airline Airline { get; set; }

        [ForeignKey("PriceListServiceConnections")]
        public ICollection<PriceListServiceConnection> PriceListServiceConnections { get; set; }

        public PriceList()
        {
            this.PriceListServiceConnections = new HashSet<PriceListServiceConnection>();
        }
    }
}
