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

        [ForeignKey("PriceListServiceConnections")]
        public virtual ICollection<PriceListServiceConnection> PriceListServiceConnections { get; set; }

        public Service()
        {
            this.PriceListServiceConnections = new HashSet<PriceListServiceConnection>();
        }
    }
}
