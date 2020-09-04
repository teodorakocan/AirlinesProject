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

        [ForeignKey("RentPriceList")]
        public int RentPriceListID { get; set; }
        public RentPriceList RentPriceList { get; set; }

        [ForeignKey("RentService")]
        public int RentServiceID { get; set; }
        public RentService RentService { get; set; }
    }
}
