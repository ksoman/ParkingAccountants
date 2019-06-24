using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingAccountant.Models
{
    public class Plan
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime Date  { get; set; }
        public string PlanParticipantID { get; set; }
        public string PlanOwnerID { get; set; }
        [Ignore]
        //Related to the Member who owns the Plan
        public Participant Participant { get; set; }
        [Ignore]
        public System.Collections.ObjectModel.ObservableCollection<ShoppingItem> ShoppingListItem { get; set; }
         
    }
}
