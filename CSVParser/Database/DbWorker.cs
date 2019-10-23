using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CSVParser.Models;
using Dapper;

// db dwh - type star
// reorganize dbo.data table to fact table with dims
namespace CSVParser.Database
{
    public class DbWorker
    {
        private readonly string connectionString;

        public DbWorker(string connection)
        {
            connectionString = connection;
        }

        public void CreateTable()
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);

                db.Query(Query.CreateTable());
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        public DataTable Select(DateTime startDate, DateTime finistDate, string format)
        {
            using SqlConnection db = new SqlConnection(connectionString);
            string a = Query.SelectPerMonthes(startDate, finistDate, format);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(a, db);
            DataTable dataSet = new DataTable();
            adapter.Fill(dataSet);
            return dataSet;
        }

        public void CreateView(int num)
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);

                db.Query(@" DROP VIEW IF EXISTS CarQuantitiesPerMonthes;
                        EXECUTE('create View [dbo].[CarQuantitiesPerMonthes] as
                        SELECT [Make],
                        [2016-2] as [feb 2016],
                        [2016-3] as [mar 2016],
                        [2016-4] as [apr 2016],
                        [2016-5] as [may 2016],
                        [2016-6] as [jun 2016],
                        [2016-7] as [jul 2016],
                        [2016-8] as [aug 2016],
                        [2016-9] as [sep 2016],
                        [2016-10] as [oct 2016],
                        [2016-11] as [nov 2016],
                        [2016-12] as [dec 2016],
                        [2017-1] as [jan 2017]
                        FROM (select cast(year([Date]) as nvarchar(10)) + '-' + cast(month(date) as nvarchar(10)) as date, [Quantity], [Make] from Data) as p
                        pivot
                        (sum([Quantity])
                        for [Date]
                        IN ([2016-2], 
	                        [2016-3],
	                        [2016-4],
	                        [2016-5],
	                        [2016-6],
	                        [2016-7],
	                        [2016-8],
	                        [2016-9],
	                        [2016-10],
	                        [2016-11],
	                        [2016-12],
	                        [2017-1])
                        ) as piv;
                       ')");
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        public void DeleteTable()
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);
                db.Query(Query.DeleteTable());
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
