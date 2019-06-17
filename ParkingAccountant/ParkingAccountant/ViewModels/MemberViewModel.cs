using ParkingAccountant.Models;
using ParkingAccountant.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkingAccountant.ViewModels
{
    public class MemberViewModel : BaseViewModel
    {
        public ObservableCollection<Member> Members { get; set; }
        public Command LoadMembersCommand { get; set; }

        public MemberViewModel()
        {
            Title = "Member";
            Members = new ObservableCollection<Member>();
            LoadMembersCommand = new Command(async () => await ExecuteLoadItemsCommand());

            /*TODO: MessagingCenter to register when implemented AddMember*/

            MessagingCenter.Subscribe<NewMemberPage, Member>(this, "AddMember", async (obj, item) =>
            {
                var newItem = item as Member;
                Members.Add(newItem);
                await MemberStore.AddDataAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Members.Clear();
                var members = await MemberStore.GetDatasAsync(true);
                foreach (var item in members)
                {
                    Members.Add(item);
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
