using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.AssignmentModel
{
    public class EngineerFarmerAssignmentViewModel
    {
        public int Id { get; set; }
        public int EngineerId { get; set; }
        public string EngineerName { get; set; }
        public string EngineerSpecialization { get; set; }
        public int FarmerId { get; set; }
        public string FarmerName { get; set; }
        public string FarmLocation { get; set; }
        public DateTime AssignedAt { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
    }
}
