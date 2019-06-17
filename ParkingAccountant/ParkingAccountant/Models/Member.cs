using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingAccountant.Models
{
    public class Member
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public  string PhoneNumber { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; }
    }
}
