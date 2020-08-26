using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RentACar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Promo_Description { get; set; }

        [ForeignKey("Branche")]
        public ICollection<Branche> Branches { get; set; }

        [ForeignKey("Rent_Price_List")]
        public ICollection<RentPriceList> RentPriceLists { get; set; }

        public RentACar()
        {
            this.RentPriceLists = new HashSet<RentPriceList>();
            this.Branches = new HashSet<Branche>();
        }
    }
}
