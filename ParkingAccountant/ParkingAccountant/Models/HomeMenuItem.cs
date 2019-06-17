using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingAccountant.Models
{
    public enum MenuItemType
    {
        //Browse,
        //About,
        ParkingPlan,
        Member
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
