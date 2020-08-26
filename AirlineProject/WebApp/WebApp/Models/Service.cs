using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }
        public string Information { get; set; }

        [ForeignKey("PriceList_Service_Connections")]
        public ICollection<PriceListServiceConnection> PriceList_Service_Connections { get; set; }

        public Service()
        {
            this.PriceList_Service_Connections = new HashSet<PriceListServiceConnection>();
        }
    }
}
