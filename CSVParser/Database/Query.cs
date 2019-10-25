using System;
using System.Text;

namespace CSVParser.Database
{
    public class Query
    {
        private static readonly string createTable = @"
                       DROP TABLE IF EXISTS [Data] 
                       CREATE TABLE [dbo].[Data] (
                       [Id]       INT        IDENTITY,
                       [Date]     DATE       NULL,
                       [Make]     NCHAR (20) NULL,
                       [Model]    NCHAR (50) NULL,
                       [Quantity] INT        NULL,
                       PRIMARY KEY CLUSTERED ([Id] ASC));
                       ";

        private static readonly string deleteTable = @"DROP TABLE IF EXISTS [dbo].[Data];";

        private static readonly string createStar = @" DROP TABLE IF EXISTS [dbo].F_QUANTITIES
                                                        CREATE TABLE [dbo].[F_QUANTITIES](
	                                                        [Make_Id] [int] NOT NULL,
	                                                        [Date_Id] [int] NOT NULL,
	                                                        [Model_Id] [int] NOT NULL,
	                                                        [Quantity] [int] NOT NULL
                                                        ) ON [PRIMARY]

                                                        DROP TABLE IF EXISTS [dbo].D_DATE
                                                        CREATE TABLE [dbo].[D_DATE](
	                                                        [Date_Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                                                        [Date] [date] NOT NULL)

                                                        DROP TABLE IF EXISTS [dbo].D_MAKE
                                                        CREATE TABLE [dbo].[D_MAKE](
	                                                        [Make_Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                                                        Name nchar(20) NOT NULL)

                                                        DROP TABLE IF EXISTS [dbo].D_MODEL
                                                        CREATE TABLE [dbo].[D_MODEL](
	                                                        [Model_Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                                                        Name nchar(50) NOT NULL)

                                                        INSERT INTO D_DATE (Date) Select distinct Date from Data

                                                        INSERT INTO D_MAKE(Name) Select distinct Make from Data

                                                        INSERT INTO D_MODEL(Name) Select distinct Model from Data

                                                        INSERT INTO F_QUANTITIES(Date_Id, Make_Id, Model_Id, Quantity) 
                                                        select D_DATE.Date_Id, D_MAKE.Make_Id, D_MODEL.Model_Id, Data.Quantity 
                                                        from Data
                                                        inner join D_DATE on Data.Date = D_DATE.Date
                                                        inner join D_MAKE on Data.Make = D_MAKE.Name
                                                        inner join D_MODEL on Data.Model = D_MODEL.Name ";

        public static string CreateTable() => createTable;

        public static string Star() => createStar;

        public static string DeleteTable() => deleteTable;

        public static string SelectPerMonthes(DateTime startDate, DateTime finishDate, string format, string dimensionName)
        {
            StringBuilder query1 = new StringBuilder($"SELECT [{dimensionName}]", 8000);
            StringBuilder query2 = new StringBuilder(
                $"FROM (select cast(year([Date]) as nvarchar(10)) + '-' + cast(month(date) as nvarchar(10)) as date, [Quantity], [{dimensionName}] from Data) as p\n" +
                      @"  pivot
                        (sum([Quantity])
                        for [Date]
                        IN (", 8000);
            do
            {
                query1.Append($", [{startDate.Year}-{startDate.Month}] as [{startDate.ToString(format)}]\n"); // "MMM yyyy"

                query2.Append($"[{startDate.Year}-{startDate.Month}], \n");
                startDate = startDate.AddMonths(1);
            }
            while (startDate < finishDate.AddMonths(1));
            query1.Append(query2);
            query2.Clear();
            query1.Remove(query1.Length - 3, 2);
            query1.Append(")) as piv;");
            return query1.ToString().Replace("  ", string.Empty);
        }
    }
}
