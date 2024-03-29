﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MyUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Provider { get; set; }
        public string PictureURL { get; set; }
        public string IdToken { get; set; }
        public int Points { get; set; }


        [ForeignKey("AircraftConfiguration")]
        public virtual ICollection<AircraftConfiguration> AircraftConfigurations { get; set; }

        [ForeignKey("VehicleRaitings")]
        public virtual ICollection<VehicleRaiting> VehicleRaitings { get; set; }

        [ForeignKey("RentACarRaitings")]
        public virtual ICollection<RentACarRaiting> RentACarRaitings { get; set; }

        [ForeignKey("FastReservation")]
        public virtual ICollection<FastReservation> FastReservations { get; set; }

        [ForeignKey("CarReservations")]
        public virtual ICollection<CarReservation> CarReservations { get; set; }

        public MyUser()
        {
            this.AircraftConfigurations = new HashSet<AircraftConfiguration>();
            this.FastReservations = new HashSet<FastReservation>();
            this.CarReservations = new HashSet<CarReservation>();
            this.VehicleRaitings = new HashSet<VehicleRaiting>();
            this.RentACarRaitings = new HashSet<RentACarRaiting>();
        }
    }
}
