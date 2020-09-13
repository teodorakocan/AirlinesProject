using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RentService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Key { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }

        [ForeignKey("Renta_Price_List_Service_Connections")]
        public virtual ICollection<RentaPriceListServiceConnection> Renta_Price_List_Service_Connections { get; set; }

        public RentService()
        {
            this.Renta_Price_List_Service_Connections = new HashSet<RentaPriceListServiceConnection>();
        }
    }
}
