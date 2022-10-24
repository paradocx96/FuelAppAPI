/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* Model class for Database Configuration
*/

namespace FuelAppAPI.Models.Database
{
    public class FuelDatabaseSettings : IFuelDatabaseSettings
    {
        public string DatabaseName { get; set; } // Name for the Database

        public string ConnectionString { get; set; } // Name for the Connection String

        public string UserCollectionName { get; set; } // Name for the User collection

        public string NoticeCollectionName { get; set; } // Name for the Notice collection

        public string FeedbackCollectionName { get; set; } // Name for the Feedback collection

        public string FuelStationsCollectionName { get; set; } // Name for the fuel station collection

        public string FuelStationArchivesCollectionName { get; set; } // Name for the fuel stations archive collection

        public string QueueLogsCollectionName { get; set; } // Name for the queue log collection

        public string FuelStationLogsCollectionName { get; set; } // Name for the fuel station log collection

        public string FavouriteCollectionName { get; set; } //Name for the favourite collection
    }
}