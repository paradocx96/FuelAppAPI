namespace FuelAppAPI.Models.Database;

public interface IFuelDatabaseSettings
{
    public string DatabaseName { get; set; }
    
    public string ConnectionString { get; set; }
}