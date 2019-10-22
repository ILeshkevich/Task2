using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration.Attributes;

namespace CSVParser.Models
{
    internal class CarModel
    {
        [Index(0)]
        public DateTime Date { get; set; }

        [Index(1)]
        public string Make { get; set; }

        [Index(2)]
        public string Model { get; set; }

        [Index(3)]
        public int Quantity { get; set; }
    }
}
