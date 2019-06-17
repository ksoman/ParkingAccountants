using ParkingAccountant.Models;
using ParkingAccountant.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkingAccountant.ViewModels
{
    public class ParkingPlanViewModel : BaseViewModel
    {
        public ObservableCollection<Plan> Plans { get; set; }
        public Command LoadPlansCommand { get; set; }

        public ParkingPlanViewModel()
        {
            Title = "Parking Plan";
            Plans = new ObservableCollection<Plan>();
            LoadPlansCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewPlanPage, Plan>(this, "AddPlan", async (obj, item) =>
            {
                var newItem = item as Plan;
                var existPlan = Plans.SingleOrDefault(x => x.Id == newItem.Id);
                if (existPlan != null)
                {
                    Plans.Remove(existPlan);
                    Plans.Add(newItem);
                await PlanStore.UpdateDataAsync(newItem);
                }
                else
                {
                    Plans.Add(newItem);
                await PlanStore.AddDataAsync(newItem);

                }
            });


        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Plans.Clear();
                var plans = await PlanStore.GetDatasAsync(true);
                foreach (var item in plans)
                {
                    Plans.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
