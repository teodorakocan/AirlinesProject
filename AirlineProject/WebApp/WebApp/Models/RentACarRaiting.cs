using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RentACarRaiting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Mark { get; set; }

        [ForeignKey("RentACar")]
        public int RentACarID { get; set; }
        public RentACar RentACar { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public MyUser User { get; set; }
    }
}
