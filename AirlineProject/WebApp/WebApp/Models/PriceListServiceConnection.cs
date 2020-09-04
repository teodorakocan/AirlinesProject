using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PriceListServiceConnection
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("PriceList")]
        public int PriceListID { get; set; }
        public PriceList PriceList { get; set; }

        [ForeignKey("Service")]
        public int ServiceID { get; set; }
        public Service Srvice { get; set; }
    }
}
