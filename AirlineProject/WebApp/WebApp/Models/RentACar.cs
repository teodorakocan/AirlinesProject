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
        public string City { get; set; }
        public string State { get; set; }
        public string PromoDescription { get; set; }

        [ForeignKey("Branch")]
        public virtual ICollection<Branch> Branches { get; set; }

        [ForeignKey("RentACarRaitings")]
        public virtual ICollection<RentACarRaiting> RentACarRaitings { get; set; }

        [ForeignKey("Reservations")]
        public virtual ICollection<CarReservation> Reservations { get; set; } 
        /*[ForeignKey("Rent_Price_List")]
        public ICollection<RentPriceList> RentPriceLists { get; set; }*/

        public RentACar()
        {
            //this.RentPriceLists = new HashSet<RentPriceList>();
            this.Branches = new HashSet<Branch>();
            this.Reservations = new HashSet<CarReservation>();
            this.RentACarRaitings = new HashSet<RentACarRaiting>();
        }
    }
}
