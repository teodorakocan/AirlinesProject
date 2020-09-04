using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime StartDateTime { get; set; } //datum i vreme poletanja
        public DateTime EndDateTime { get; set; } // datum i vreme sletanja
        public double Price { get; set; } // u jednom smeru
        public double Price2 { get; set; } //povratna
        public string FlightNumber { get; set; }
        public int NumberTransfer { get; set; } //broj 
        public string DestinationTransfer { get; set; }//lokacija presedanja
        public TimeSpan TravelLength { get; set; } //duzina putovanja

        [ForeignKey("Destination")]
        public int DestinationID { get; set; }
        public Destination Destination { get; set; }

        /*[ForeignKey("Aircraft_Configuration")]
        public int Aircraft_Configuration_ID { get; set; }
        public AircraftConfiguration Aircraft_Configuration { get; set; }*/
    }
}
