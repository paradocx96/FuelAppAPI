namespace FuelAppAPI.Models.Database;

public class FuelDatabaseSettings : IFuelDatabaseSettings
{
    public string DatabaseName { get; set; }
    
    public string ConnectionString { get; set; }
    
    public string UserCollectionName { get; set; }
    
    public string NoticeCollectionName { get; set; }
    
    public string FeedbackCollectionName { get; set; }
    
    public string FuelStationsCollectionName { get; set; } // Name for the fuel station collection
}