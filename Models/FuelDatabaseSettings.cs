namespace FuelAppAPI.Models
{
    public class FuelDatabaseSettings : IFuelDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
