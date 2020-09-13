using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModel
{
    public class ServiceViewModel
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceCity { get; set; }
        public string ServiceState { get; set; }
        public string ServiceAddress { get; set; }
        public string ServicePromoDescription { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchCity { get; set; }
        public string BranchState { get; set; }
        public string BranchAddress { get; set; }
        public int NumberOfVehicle { get; set; }
    }
}
