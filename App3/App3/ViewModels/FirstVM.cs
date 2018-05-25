using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using App3.Models;
using SQLite;
using SQLite.Net;

namespace App3.ViewModels
{
    public class FirstVM
    {
        string dbPathTemp = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "databaseTemp.db3");


        public void Setup()
        {
        SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

        var conn = new SQLite.Net.SQLiteConnection(plat, dbPathTemp);
        }

        public static string DoSomeDataAccess()
        {
            Console.WriteLine("Creating database, if it doesn't already exist");

         SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

        string dbPath = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                 "ormdemo.db3");

            var db = new SQLite.Net.SQLiteConnection(plat, dbPath);

            db.CreateTable<Stock>();

            if (db.Table<Stock>().Count() == 0)
            {
                // only insert the data if it doesn't already exist
                var newStock = new Stock();
                newStock.Symbol = "AAPL";
                db.Insert(newStock);
                newStock = new Stock();
                newStock.Symbol = "GOOG";
                db.Insert(newStock);
                newStock = new Stock();
                newStock.Symbol = "MSFT";
                db.Insert(newStock);
            }
            Console.WriteLine("Reading data");
            var table = db.Table<Stock>();
            string data = string.Empty;

            foreach (var s in table)
            {
                Console.WriteLine(s.Id + " " + s.Symbol);

                data += s.Id + " " + s.Symbol + " :: ";
            }

            return data;
        }
    }
}
