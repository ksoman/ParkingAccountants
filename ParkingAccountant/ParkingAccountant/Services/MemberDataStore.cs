using ParkingAccountant.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkingAccountant.Services
{
    public class MemberDataStore : IDataStore<Member>
    {
        List<Member> members;
        private SQLiteConnection conn;
        public MemberDataStore()
        {
          conn = DependencyService.Get<ISqliteDB>().GetConnection();
            conn.CreateTable<Member>();
            members = conn.Table<Member>().ToList();

        }


        public async Task<int> AddDataAsync(Member member)
        {
            members.Add(member);

            //  return await Task.FromResult(true);
            return conn.Insert(member);
        }

        public async Task<int> UpdateDataAsync(Member member)
        {
            var oldItem = members.Where((Member arg) => arg.Id == member.Id).FirstOrDefault();
            members.Remove(oldItem);
            members.Add(member);
            foreach (var item in members)
            {
                conn.InsertOrReplace(item);
            }

            return members.Count;
        }

        public async Task<int> DeleteDataAsync(string id)
        {
            var oldItem = members.Where((Member arg) => arg.Id == id).FirstOrDefault();
            return conn.Delete(oldItem);
        }

        public async Task<Member> GetDataAsync(string id)
        {
            return await Task.FromResult(members.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Member>> GetDatasAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(members);
        }

       
    }
}
