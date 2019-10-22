using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Create table.\n" +
                    "2. Delete table.\n" +
                    "3. Read File to Database.\n" +
                    "4. Create View\n" +
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
                        new DbWorker(connectionString).Insert(new CSV.CSV().Read(path));
                        break;
                    case "4":
                        new DbWorker(connectionString).CreateView();
                        break;
                }
            }
        }
    }
}
