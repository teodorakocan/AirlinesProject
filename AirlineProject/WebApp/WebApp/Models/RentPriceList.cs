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

        [ForeignKey("Rent_a_Car")]
        public int Rent_a_Car_ID { get; set; }
        public RentACar Rent_a_Car { get; set; }

        [ForeignKey("Renta_Price_List_Service_Connections")]
        public ICollection<RentaPriceListServiceConnection> Renta_Price_List_Service_Connections { get; set; }

        public RentPriceList()
        {
            this.Renta_Price_List_Service_Connections = new HashSet<RentaPriceListServiceConnection>();
        }
    }
}
