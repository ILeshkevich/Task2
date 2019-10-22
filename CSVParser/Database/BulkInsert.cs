using CsvHelper;
using CSVParser.Models;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System;

namespace CSVParser.Database
{
    internal class BulkInsert : Inserter
    {
        private string path;

        public BulkInsert(string connection, string path)
            : base(connection)
        {
            this.path = path;
        }

        public override void Insert(CarModel[] cars)
        {
            //DateTime starttime = DateTime.Now;
            //using var db = new SqlConnection(connectionString);
            //db.
            //DateTime endtime = DateTime.Now;
            //Console.WriteLine(endtime - starttime);
            //Console.ReadKey();
        }
    }
}
