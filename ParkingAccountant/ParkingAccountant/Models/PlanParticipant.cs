using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingAccountant.Models
{
    public class PlanParticipant
    {
        public string ID { get; set; }
        public string PlanId { get; set; }
        public string ParticipantID { get; set; }
        public double  Amount { get; set; }
    }
}
