using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Branche
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Address { get; set; }
        public int Number_Of_Vehicle { get; set; }

        [ForeignKey("Vehicle")]
        public ICollection<Vehicle> Vehicles { get; set; }

        [ForeignKey("Rent_a_Car")]
        public int Rent_a_Car_ID { get; set; }
        public RentACar Rent_a_Car { get;set;}

        public Branche()
        {
            this.Vehicles = new HashSet<Vehicle>();
        }
    }
}
