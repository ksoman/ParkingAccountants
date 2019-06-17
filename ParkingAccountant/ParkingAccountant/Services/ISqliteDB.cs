using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingAccountant.Services
{
    public interface ISqliteDB
    {
        SQLiteAsyncConnection GetAsyncConnection();

        SQLiteConnection GetConnection();
    }
}
