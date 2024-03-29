﻿using System;
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
            try
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader);
                csv.Configuration.RegisterClassMap<CarMap>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HasHeaderRecord = true;
                var result = csv.GetRecords<CarModel>().ToArray();

                return result;
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }

            return new CarModel[0];
        }
    }
}
