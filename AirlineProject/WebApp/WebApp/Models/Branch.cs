using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int NumberOfVehicle { get; set; }

        [ForeignKey("Vehicle")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }

        [ForeignKey("Resrvations")]
        public virtual ICollection<CarReservation> Resrvations { get; set; }

        [ForeignKey("RentACar")]
        public int RentACarID { get; set; }
        public RentACar RentACar { get;set;}

        public Branch()
        {
            this.Vehicles = new HashSet<Vehicle>();
            this.Resrvations = new HashSet<CarReservation>();
        }
    }
}
