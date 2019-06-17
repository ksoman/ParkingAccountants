using ParkingAccountant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ParkingAccountant.ViewModels
{
    public class MemberSelectedViewModel :BaseViewModel
    {
        public Member Member{ get; set; }
        public bool Selected { get; set; }

    }
}
