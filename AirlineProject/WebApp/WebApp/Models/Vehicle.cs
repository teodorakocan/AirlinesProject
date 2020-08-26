﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Desription { get; set; }
        public bool Reserved { get; set; }
        public double Price { get; set; }

        [ForeignKey("Branche")]
        public int Branche_ID { get; set; }
        public Branche Branche { get; set; }

        [ForeignKey("User")]
        public int User_ID { get; set; }
        public User User { get; set; }
}
}