using ParkingAccountant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingAccountant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MembersPage : ContentPage
	{
        MemberViewModel viewModel;
		public MembersPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new MemberViewModel();
		}
        async void AddPlan_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewMemberPage()));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Members.Count == 0)
                viewModel.LoadMembersCommand.Execute(null);
        }
    }
}