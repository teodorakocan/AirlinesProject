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

        [ForeignKey("Price_List")]
        public int Price_List_ID { get; set; }
        public PriceList Price_List { get; set; }

        [ForeignKey("Service")]
        public int Service_ID { get; set; }
        public Service Srvice { get; set; }
    }
}
