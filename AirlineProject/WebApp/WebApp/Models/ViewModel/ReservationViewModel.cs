using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModel
{
    public class ReservationViewModel
    {
        public DateTime ReservationFrom { get; set; }
        public DateTime ReservationTo { get; set; }
        public string ServiceName { get; set; }
        public string BranchName { get; set; }
        public string UserName { get; set; }
    }
}
