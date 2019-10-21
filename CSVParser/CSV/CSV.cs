using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CSVParser.Models;

namespace CSVParser.CSV
{
    internal class CSV
    {
        public CarModel[] Read(string path)
        {
            // Fix path to Stream reader after implement of File worker;
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader);
            csv.Configuration.RegisterClassMap<CarMap>();
            return csv.GetRecords<CarModel>().ToArray();
        }
    }
}
