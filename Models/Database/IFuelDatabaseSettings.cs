namespace FuelAppAPI.Models.Database;

public interface IFuelDatabaseSettings
{
    public string DatabaseName { get; set; }
    
    public string ConnectionString { get; set; }
    
    public string UserCollectionName { get; set; }
    
    public string NoticeCollectionName { get; set; }
    
    public string FeedbackCollectionName { get; set; }
    
    public string FuelStationsCollectionName { get; set; }
}