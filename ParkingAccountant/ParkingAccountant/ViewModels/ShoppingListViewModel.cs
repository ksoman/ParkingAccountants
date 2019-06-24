using ParkingAccountant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ParkingAccountant.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        public ShoppingItem ShoppingItem{ get; set; }

        public ObservableCollection<ShoppingItem> ShoppingItemList { get; set; }

        public ShoppingListViewModel()
        {
            ShoppingItem = new ShoppingItem();
            ShoppingItemList = new ObservableCollection<ShoppingItem>();
        }

    }
}
