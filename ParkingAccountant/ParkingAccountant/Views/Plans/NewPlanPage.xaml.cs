using ParkingAccountant.Models;
using ParkingAccountant.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingAccountant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPlanPage : ContentPage
    {
        public Plan Plan { get; set; }
        public ObservableCollection<Member> AllMembers { get; set; }

        public NewPlanPage(Plan plan)
        {
            Plan = new Plan()
            {
                Date = plan.Date,
                Description = plan.Description,
                Participant = plan.Participant,
               
                Id = plan.Id
            };
            InitializePage();
            AllMembersPicker.SelectedIndex = AllMembers.IndexOf( AllMembers.Single(s => s.Id == plan.Participant.Owner.Id));
        }

        public NewPlanPage()
        {
            Plan = new Plan
            {
                Id =  Guid.NewGuid().ToString(),
                Description = "New Plan Name",
                Date = DateTime.Today
            };

            InitializePage();

        }

        private void InitializePage()
        {
            SubscribeMessageCentre();
            AllMembers = new ObservableCollection<Member>();
            GetMembersAsync();
            InitializeComponent();
            BindingContext = this;

        }

        async void GetMembersAsync()
        {
            MemberDataStore memberDatas = new MemberDataStore();
            if (Plan.Participant == null)
            {
                Plan.Participant = new Participant() { Participants = new ObservableCollection<Member>() };

            }
            var members = await memberDatas.GetDatasAsync();
            foreach (var item in members)
            {
                AllMembers.Add(item);
            }
        }

        void SubscribeMessageCentre()
        {
            MessagingCenter.Subscribe<AllParticipantListPage, ObservableCollection<Member>>(this, "AddParticipants", async (obj, item) =>
            {
                var newItem = item as ObservableCollection<Member>;
                Plan.Participant.Participants.Clear();
                foreach (var member in newItem)
                {

                    Plan.Participant.Participants.Add(member);
                }
            });

            MessagingCenter.Subscribe<AllParticipantListPage, Plan>(this, "ViewPlan", async (obj, item) =>
            {
                var newItem = item as Plan;
                Plan = newItem;
            });
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddPlan", Plan);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Add_Participant_Clicked(object sender, EventArgs e)
        {
            /*Plan.Participant.Participants.Add(new Member { Name = "member" + Plan.Participant.Participants.Count });
             AllMembers.Add(new Member { Name = "member" + Plan.Participant.Participants.Count }); */

            //go to page where we can select all participent.
            Navigation.PushAsync(new AllParticipantListPage(Plan,AllMembers));

        }

        private void AllMembersPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            var owner = picker.SelectedItem as Member;
            Plan.Participant.Owner = owner;
        }
    }
}