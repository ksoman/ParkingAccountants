using ParkingAccountant.Models;
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
	public partial class NewMemberPage : ContentPage
	{
        public Member Member{ get; set; }
        public NewMemberPage ()
		{
			InitializeComponent ();
            Member = new Member
            {
                Id =  Guid.NewGuid().ToString(),
                Name = "MyName",
                PhoneNumber = "50000000",
            };
            BindingContext = this;
		}
        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddMember", Member);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}