using System;
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

        public void CreateView()
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

        public void DeleteTable()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            db.Query(@"DROP TABLE [dbo].[Data];");
        }
    }
}
