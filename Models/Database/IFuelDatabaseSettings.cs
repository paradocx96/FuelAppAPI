/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* Model interface for Database configuration
*/

namespace FuelAppAPI.Models.Database
{
    public interface IFuelDatabaseSettings
    {
        string DatabaseName { get; set; } // Name for the Database

        string ConnectionString { get; set; } // Name for the Connection String

        string UserCollectionName { get; set; } // Name for the User collection

        string NoticeCollectionName { get; set; } // Name for the Notice collection

        string FeedbackCollectionName { get; set; } // Name for the Feedback collection

        string FuelStationsCollectionName { get; set; } // Name for the Fuel Station collection

        string FuelStationArchivesCollectionName { get; set; } // Name for the Fuel Station Archive collection

        string QueueLogsCollectionName { get; set; } // Name for the queue log collection

        string FuelStationLogsCollectionName { get; set; } // Name for the fuel station log collection

        string FavouriteCollectionName { get; set; } //Name for the favourite collection
    }
}