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
            try
            {
                DateTime starttime = DateTime.Now;
                using var db = new SqlConnection(connectionString);
                db.Query(@" DROP TABLE IF EXISTS [temptable]
                        create table [temptable]
                        ([Date] [date] NULL,
	                     [Make] [nchar](20) NULL,
	                     [Model] [nchar](50) NULL,
	                     [Quantity] [int] NULL)

                        BULK INSERT [temptable]" +
                        $"From '{path}'" +
                        @"with
                        (
                        rowterminator = '\n',
                        fieldterminator = ',',
                        firstrow = 2
                        )

                        insert into Data
                        select * 
                        From [temptable]
                        go

                        DROP TABLE IF EXISTS [temptable]");
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
