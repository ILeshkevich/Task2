using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CSVParser.Models;
using Dapper;

namespace CSVParser.Database
{
    internal class DbWorker
    {
        private readonly string connectionString;

        public DbWorker(string connection)
        {
            connectionString = connection;
        }

        public void CreateTable()
        {
            using IDbConnection db = new SqlConnection(connectionString);

            db.Query(@"
                       DROP TABLE IF EXISTS [Data] 
                       CREATE TABLE [dbo].[Data] (
                       [Id]       INT        IDENTITY,
                       [Date]     DATE       NULL,
                       [Make]     NCHAR (20) NULL,
                       [Model]    NCHAR (50) NULL,
                       [Quantity] INT        NULL,
                       PRIMARY KEY CLUSTERED ([Id] ASC)
                    );");
        }

        public List<CarModel> SelectAll()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<CarModel>("SELECT * FROM Data").ToList();
        }

        // Freckly slow method
        public void Insert(CarModel[] cars)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            foreach (var car in cars)
            {
                db.Query<CarModel>("INSERT INTO Data (Date, Make, Model, Quantity) VALUES(@Date, @Make, @Model, @Quantity);", car);
            }
        }

        public void CreateView()
        {
            using IDbConnection db = new SqlConnection(connectionString);

            db.Query(@" DROP VIEW IF EXISTS CarQuantitiesPerMonthes;
                        EXECUTE('create View CarQuantitiesPerMonthes as
                        SELECT [Make],
                        [2016-02-28] as [feb 2016],
                        [2016-03-28] as [mar 2016],
                        [2016-04-28] as [apr 2016],
                        [2016-05-28] as [may 2016],
                        [2016-06-28] as [jun 2016],
                        [2016-07-28] as [jul 2016],
                        [2016-08-28] as [aug 2016],
                        [2016-09-28] as [sep 2016],
                        [2016-10-28] as [oct 2016],
                        [2016-11-28] as [nov 2016],
                        [2016-12-28] as [dec 2016],
                        [2017-01-28] as [jan 2017]
                        FROM (select [Date], [Quantity], [Make] from Data) as p
                        pivot
                        (sum([Quantity])
                        for [Date]
                        IN ([2016-02-28], 
	                        [2016-03-28],
	                        [2016-04-28],
	                        [2016-05-28],
	                        [2016-06-28],
	                        [2016-07-28],
	                        [2016-08-28],
	                        [2016-09-28],
	                        [2016-10-28],
	                        [2016-11-28],
	                        [2016-12-28],
	                        [2017-01-28])
                        ) as piv;')");
        }

        public void DeleteTable()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            db.Query(@"DROP TABLE [dbo].[Data];");
        }
    }
}
