using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CSVParser.Models;
using Dapper;

namespace CSVParser.Database
{
    internal class MultipleRowsInsert : Inserter
    {
        public MultipleRowsInsert(string connaction)
            : base(connaction)
        {
        }

        // Todo: 1. Add option to insert multiple rows once
        // Todo: 2. Add option to insert rows using bulk insert
        // Todo: 3. Make 3 classes and interface
        // Todo: 4. Add factory and user input options
        // Todo: 5. Optionally: calculate execution time
        // Todo: 6. Add error handling - notify user if exception occured
        public override void Insert(CarModel[] cars)
        {
            try
            {
            DateTime starttime = DateTime.Now;
            using IDbConnection db = new SqlConnection(connectionString);
            StringBuilder queryString = new StringBuilder("INSERT INTO Data(Date, Make, Model, Quantity) VALUES ", 60000);
            if (cars.Length != 0)
            {
                for (int i = 0, n = 0; i < cars.Length; i++, n++)
                {
                    queryString.Append($"('{cars[i].Date.ToString("yyyy-MM-dd")}', '{cars[i].Make.Trim().Replace('\'', ' ')}', '{cars[i].Model.Trim().Replace('\'', ' ')}', '{cars[i].Quantity}'),\n");
                    if (n == 999)
                    {
                        queryString.Remove(queryString.Length - 2, 1);
                        queryString.Append(";");
                        db.Query(queryString.ToString());
                        queryString.Remove(0, queryString.Length);
                        queryString.Append("INSERT INTO Data(Date, Make, Model, Quantity) VALUES ");
                        n = -1;
                    }
                }

                queryString.Remove(queryString.Length - 2, 2);
                queryString.Append(";");
                db.Query(queryString.ToString());
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