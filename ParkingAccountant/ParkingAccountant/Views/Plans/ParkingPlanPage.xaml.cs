using ParkingAccountant.Models;
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
	public partial class ParkingPlanPage : ContentPage
	{
        ParkingPlanViewModel viewModel;
		public ParkingPlanPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ParkingPlanViewModel();

		}
        async void AddPlan_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewPlanPage()));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Plans.Count == 0)
                viewModel.LoadPlansCommand.Execute(null);
        }

        private void ItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = sender as ListView;
            var plan = listView.SelectedItem as Plan;
            //MessagingCenter.Send(this, "ViewPlan", plan);
            Navigation.PushModalAsync(new NavigationPage(new NewPlanPage(plan)));
        }
    }
}