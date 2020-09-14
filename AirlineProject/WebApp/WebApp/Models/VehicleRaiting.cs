using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class VehicleRaiting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Mark { get; set; }
        
        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public MyUser User { get; set; }
    }
}
