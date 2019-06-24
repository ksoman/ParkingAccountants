using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingAccountant.Views.ShoppingList
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewShoppingList : ContentPage
	{
        private ViewModels.ShoppingListViewModel viewModel;
        public NewShoppingList ()
		{
            SubscribeMessagingService();

            this.BindingContext = viewModel = new ViewModels.ShoppingListViewModel();
            InitializeComponent ();
		}

        private void AmountCalculator_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new Calculator.Calculator() ));
                
        }

        public void SubscribeMessagingService()
        {
            MessagingCenter.Subscribe<Calculator.Calculator, string>(this, "AddAmount", async (obj, item) =>
            {
                AmountCalculator.Text = item.ToString();
            });
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {

        }

        private void AddItem_Clicked(object sender, EventArgs e)
        {
            viewModel.ShoppingItemList.Add(new Models.ShoppingItem() { ItemAmount = Convert.ToDouble(AmountCalculator.Text), ItemName = ItemName.Text });
            AmountCalculator.Text="";
            ItemName.Text = "";
          

        }
        protected override void OnDisappearing()
        {
            MessagingCenter.Send(this, "AddShoppingList",viewModel.ShoppingItemList);
            base.OnDisappearing();
        }
    }
}