/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* Model interface for Database configuration
*/

namespace FuelAppAPI.Models.Database
{
    public interface IFuelDatabaseSettings
    {
        public string DatabaseName { get; set; } // Name for the Database

        public string ConnectionString { get; set; } // Name for the Connection String

        public string UserCollectionName { get; set; } // Name for the User collection

        public string NoticeCollectionName { get; set; } // Name for the Notice collection

        public string FeedbackCollectionName { get; set; } // Name for the Feedback collection

        public string FuelStationsCollectionName { get; set; } // Name for the Fuel Station collection

        public string FuelStationArchivesCollectionName { get; set; } // Name for the Fuel Station Archive collection
    }
}