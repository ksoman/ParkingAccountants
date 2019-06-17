using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using ParkingAccountant.Droid.Implementation;
using ParkingAccountant.Services;
using SQLite;
[assembly: Xamarin.Forms.Dependency(typeof(SqliteDB))]
namespace ParkingAccountant.Droid.Implementation
{
    public class SqliteDB : ISqliteDB
    {
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var docpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(docpath, "StructureAppDB.db3");
            return new SQLiteAsyncConnection(path);
        }
        public SQLiteConnection GetConnection()
        {
            // var local = System.Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
            var docpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(docpath, "StructureAppDB.db3");
            var sss = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache, true);
            return sss;

        }
    }
}