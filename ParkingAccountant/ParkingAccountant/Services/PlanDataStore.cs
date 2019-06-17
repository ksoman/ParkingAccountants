using ParkingAccountant.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkingAccountant.Services
{
    public class PlanDataStore : IDataStore<Plan>
    {

        List<Plan> plans;
        private SQLiteConnection conn;
        public PlanDataStore()
        {
            plans = new List<Plan>();
            conn = DependencyService.Get<ISqliteDB>().GetConnection();
            conn.CreateTable<Plan>();
            conn.CreateTable<PlanOwner>();
            conn.CreateTable<PlanParticipant>();

            plans = conn.Table<Plan>().ToList();
            //plans.Add(new Plan { Id = Guid.NewGuid().ToString(), Date = DateTime.Today, Description = "My First Parking Plan" });

        }


        public async Task<int> AddDataAsync(Plan plan)
        {
            var planOwner = new PlanOwner { Id = Guid.NewGuid().ToString(), OwenerID = plan.Participant.Owner.Id, PlanID = plan.Id };
            var planParticipant = new List<PlanParticipant>();
            foreach (var item in plan.Participant.Participants)
            {
                planParticipant.Add(new PlanParticipant { ID = Guid.NewGuid().ToString(), ParticipantID = item.Id, PlanId = plan.Id, Amount = item.Amount });
            }
            conn.Insert(planOwner);
            conn.InsertAll(planParticipant);
            plan.PlanOwnerID = planOwner.Id;
            plans.Add(plan);
            var res = conn.Insert(plan);
            return res;
        }

        public async Task<int> UpdateDataAsync(Plan plan)
        {
            var oldItem = plans.Where((Plan arg) => arg.Id == plan.Id).FirstOrDefault();
            oldItem.Date = plan.Date;
            oldItem.Description = plan.Description;
            oldItem.Participant = plan.Participant;
            return conn.InsertOrReplace(oldItem);

        }

        public async Task<int> DeleteDataAsync(string id)
        {
            var oldItem = plans.Where((Plan arg) => arg.Id == id).FirstOrDefault();
            plans.Remove(oldItem);

            return 0;
        }

        public async Task<Plan> GetDataAsync(string id)
        {

            var plan = conn.Table<Plan>().FirstOrDefault(s => s.Id == id);
            var ownerid = conn.Table<PlanOwner>().First(x => x.PlanID == id).OwenerID;
            var owner = conn.Table<Member>().Single(m => m.Id == ownerid);
            plan.Participant.Owner = owner;
            return plan;
        }

        public async Task<IEnumerable<Plan>> GetDatasAsync(bool forceRefresh = false)
        {
            foreach (var item in plans)
            {
                var ownerid = conn.Table<PlanOwner>().First(x => x.PlanID == item.Id).OwenerID;
                var owner = conn.Table<Member>().Single(m => m.Id == ownerid);
                var participants = conn.Table<PlanParticipant>().Where(p => p.PlanId == item.Id);
                var participantMembers = new ObservableCollection<Member>();
                foreach (var participant in participants)
                {
                    var member = conn.Table<Member>().SingleOrDefault(s => s.Id == participant.ParticipantID);
                    member.Amount = participant.Amount;
                    participantMembers.Add(member);
                }
                item.Participant = new Participant { Owner = owner, Participants =participantMembers };
            }
            return await Task.FromResult(plans);
        }
    }
}

