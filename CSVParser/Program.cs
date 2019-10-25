using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CsvHelper.Configuration.Attributes;
using CSVParser.Database;
using CSVParser.Models;
using Dapper;

namespace CSVParser
{
    internal class Program
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Cars;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static string path = @"D:\Task2\CSV\CSVParser\bin\Debug\netcoreapp3.0\Cars.csv";

        private static void Main(string[] args)
        {
            Inserter inserter;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Create table.\n" +
                    "2. Delete table.\n" +
                    "3. Slow read file to Database(~24 sec).\n" +
                    "4. Multiple row read file to Database(~0.24 sec)\n" +
                    "5. Bulk insert(~0.042 sec)\n" +
                    "6. Create View\n" +
                    "7. Insert to table..." +
                    "0. Exit.");
                switch (Console.ReadLine())
                {
                    case "0": return;
                    case "1":
                        new DbWorker(connectionString).CreateTable();
                        break;
                    case "2":
                        new DbWorker(connectionString).DeleteTable();
                        break;
                    case "3":
                        inserter = new SingleRowInsert(connectionString);
                        inserter.Insert(new CSV.CSV().Read(path));
                        break;
                    case "4":
                        inserter = new MultipleRowsInsert(connectionString);
                        inserter.Insert(new CSV.CSV().Read(path));
                        break;
                    case "5":
                        inserter = new BulkInsert(connectionString, path);
                        inserter.Insert(new CSV.CSV().Read(path));
                        break;
                    case "6":
                        new DbWorker(connectionString).CreateView(24);
                        break;
                    case "7":
                        Query.Star();
                        break;
                }
            }
        }
    }
}
