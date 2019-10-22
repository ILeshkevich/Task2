using CSVParser.Models;

namespace CSVParser.Database
{
    internal abstract class Inserter
    {
        protected readonly string connectionString;

        public Inserter(string connection)
        {
            connectionString = connection;
        }

        public abstract void Insert(CarModel[] cars);
    }
}
