using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration.Attributes;

namespace CSVParser.Models
{
    internal class CarModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Quantity { get; set; }
    }
}
