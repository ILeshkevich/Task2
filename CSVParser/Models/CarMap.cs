using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;

namespace CSVParser.Models
{
    internal sealed class CarMap : ClassMap<CarModel>
    {
        public CarMap()
        {
            Map(m => m.Date).Index(0);
            Map(m => m.Make).Index(1);
            Map(m => m.Model).Index(2);
            Map(m => m.Quantity).Index(3);
        }
    }
}
