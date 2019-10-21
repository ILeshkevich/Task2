using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;

namespace CSVParser.Models
{
    internal class CarMap : ClassMap<CarModel>
    {
        public CarMap()
        {
            Map(m => m.Date).TypeConverterOption.Format("yyyy/MM/dd").Name("Date");
            Map(m => m.Make).Name("Make");
            Map(m => m.Model).Name("Model");
            Map(m => m.Quantity).Name("Quantity");
        }
    }
}
