using NUnit.Framework;
using System;
using CSVParser.Database;

namespace CSVParserTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {

            DateTime start = new DateTime(2016, 1, 1);
            DateTime finish = new DateTime(2017, 1, 1);
            string format = "MMM yyyy";
            string dimensionName = "Model";
            string expected = "SELECT [Model], [2016-1] as [��� 2016]\n, [2016-2] as [��� 2016]\n, [2016-3] as [��� 2016]\n, [2016-4] as [��� 2016]\n, [2016-5] as [��� 2016]\n, [2016-6] as [��� 2016]\n, [2016-7] as [��� 2016]\n, [2016-8] as [��� 2016]\n, [2016-9] as [��� 2016]\n, [2016-10] as [��� 2016]\n, [2016-11] as [��� 2016]\n, [2016-12] as [��� 2016]\n, [2017-1] as [��� 2017]\nFROM (select cast(year([Date]) as nvarchar(10)) + '-' + cast(month(date) as nvarchar(10)) as date, [Quantity], [Model] from Data) as p\npivot\r\n(sum([Quantity])\r\nfor [Date]\r\nIN ([2016-1], \n[2016-2], \n[2016-3], \n[2016-4], \n[2016-5], \n[2016-6], \n[2016-7], \n[2016-8], \n[2016-9], \n[2016-10], \n[2016-11], \n[2016-12], \n[2017-1]\n)) as piv;";
            
            Assert.AreEqual(expected, Query.SelectPerMonthes(start, finish, format, dimensionName));
        }
    }
}