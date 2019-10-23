using System;
using System.Collections.Generic;
using System.Text;

namespace CSVParser.Database
{
    internal class Query
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

        private static readonly string deleteTable = @"DROP TABLE [dbo].[Data];";

        public static string CreateTable() => createTable;

        public static string DeleteTable() => deleteTable;

        public static string SelectPerMonthes(DateTime startDate, DateTime finishDate, string format)
        {
            StringBuilder query1 = new StringBuilder("SELECT [Make]", 8000);
            StringBuilder query2 = new StringBuilder(
                @"FROM (select cast(year([Date]) as nvarchar(10)) + '-' + cast(month(date) as nvarchar(10)) as date, [Quantity], [Make] from Data) as p
                        pivot
                        (sum([Quantity])
                        for [Date]
                        IN (", 8000);
            do
            {
                query1.Append($", [{startDate.Year}-{startDate.Month}] as [{startDate.ToString(format)}]\n"); // "MMM yyyy"

                query2.Append($"[{startDate.Year}-{startDate.Month}], \n");
                startDate = startDate.AddMonths(1);
            }
            while (startDate < finishDate);
            query1.Append(query2);
            query2.Clear();
            query1.Remove(query1.Length - 3, 2);
            query1.Append(")) as piv;");
            return query1.ToString();
        }
    }
}
