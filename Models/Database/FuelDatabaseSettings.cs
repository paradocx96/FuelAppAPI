namespace FuelAppAPI.Models.Database;

public class FuelDatabaseSettings
{
    public string DatabaseName { get; set; }
    
    public string ConnectionString { get; set; }

    public string FuelStationsCollectionName { get; set; } // Name for the fuel station collection
}