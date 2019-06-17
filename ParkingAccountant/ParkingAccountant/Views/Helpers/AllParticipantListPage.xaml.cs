using ParkingAccountant.Models;
using ParkingAccountant.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingAccountant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllParticipantListPage : ContentPage
	{
        public ObservableCollection<MemberSelectedViewModel> Members { get; set; }

        public AllParticipantListPage (Plan plan,ObservableCollection<Member> members)
		{
			InitializeComponent();
            Members = new ObservableCollection<MemberSelectedViewModel>();
            foreach (var item in members)
            {
                var isSelected = plan.Participant.Participants.Any(x => x.Name == item.Name);
                Members.Add(new MemberSelectedViewModel {  Member = item, Selected = isSelected});
            }
            BindingContext = this;

		}
        async void Save_Clicked(object sender, EventArgs e)
        {
            var selectedMember =  Members.Where(s => s.Selected);
            var memberList = new ObservableCollection<Member>();
            foreach (var item in selectedMember)
            {
                memberList.Add(item.Member);
            }
            MessagingCenter.Send(this, "AddParticipants", memberList);
            await Navigation.PopAsync();
        }

        

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var obj = sender as MemberSelectedViewModel;
            
        }
    }
}