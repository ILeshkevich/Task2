using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CSVParser.Models;
using Dapper;

namespace CSVParser.Database
{
    internal class SingleRowInsert : Inserter
    {
        public SingleRowInsert(string connaction)
            : base(connaction)
        {
        }

        // Todo: 1. Add option to insert multiple rows once
        // Todo: 2. Add option to insert rows using bulk insert
        // Todo: 3. Make 3 classes and interface
        // Todo: 4. Add factory and user input options
        // Todo: 5. Optionally: calculate execution time
        // Todo: 6. Add error handling - notify user if exception occured

        // Freckly slow method
        public override void Insert(CarModel[] cars)
        {
            try
            {
                DateTime starttime = DateTime.Now;
                using IDbConnection db = new SqlConnection(connectionString);
                foreach (var car in cars)
                {
                    db.Query<CarModel>("INSERT INTO Data (Date, Make, Model, Quantity) VALUES(@Date, @Make, @Model, @Quantity);", car);
                }

                DateTime endtime = DateTime.Now;
                Console.WriteLine(endtime - starttime);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }
    }
}
