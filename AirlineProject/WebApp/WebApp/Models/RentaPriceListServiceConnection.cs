using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RentaPriceListServiceConnection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Rent_Price_List")]
        public int Rent_Price_List_ID { get; set; }
        public RentPriceList RentPriceList { get; set; }

        [ForeignKey("Rent_Service")]
        public int Rent_Service_ID { get; set; }
        public RentService RentService { get; set; }
    }
}
