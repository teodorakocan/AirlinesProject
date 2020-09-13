using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RentPriceList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("RentACar")]
        public int RentACarID { get; set; }
        public RentACar RentACar { get; set; }

        [ForeignKey("RentaPriceListServiceConnections")]
        public virtual ICollection<RentaPriceListServiceConnection> RentaPriceListServiceConnections { get; set; }

        public RentPriceList()
        {
            this.RentaPriceListServiceConnections = new HashSet<RentaPriceListServiceConnection>();
        }
    }
}
