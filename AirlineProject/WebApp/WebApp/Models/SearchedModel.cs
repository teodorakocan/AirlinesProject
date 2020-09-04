using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SearchedModel
    {
        public string ServiceName { get; set; }
        public string City { get; set; }
        public string Brand { get; set; }
        public string Class { get; set; }
        public int NumberOfSeats { get; set; }
        public string ReservationFrom { get; set; }
        public string ReservationTo { get; set; }
    }
}
