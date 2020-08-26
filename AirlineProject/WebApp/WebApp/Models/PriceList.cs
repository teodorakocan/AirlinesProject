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
        public int Airline_ID { get; set; }
        public Airline Airline { get; set; }

        [ForeignKey("PriceList_Service_Connections")]
        public ICollection<PriceListServiceConnection> PriceList_Service_Connections { get; set; }

        public PriceList()
        {
            this.PriceList_Service_Connections = new HashSet<PriceListServiceConnection>();
        }
    }
}
