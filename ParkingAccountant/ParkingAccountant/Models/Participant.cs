using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ParkingAccountant.Models
{
    public class Participant
    {
        public Member Owner { get; set; }
        public ObservableCollection<Member> Participants { get; set; }
    }
}
