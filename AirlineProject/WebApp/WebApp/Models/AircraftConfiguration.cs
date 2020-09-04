using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    //konfiguracija segmenata i mesta u avionu
    public class AircraftConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public bool Reserved { get; set; }
        public int Number_Of_Seats { get; set; }

        /*[ForeignKey("Flight")]
        public ICollection<Flight> Flights { get; set; }*/

        [ForeignKey("User")]
        public int UserID { get; set; }
        public MyUser User { get; set; }

        public AircraftConfiguration()
        {
            //this.Flights = new HashSet<Flight>();
        }
    }
}
