using System;
namespace FuelAppAPI.DTO
{
    /*
     * IT19014128
     * A.M.W.W.R.L. Wataketiya
     The Data Transfer Object for Fuel Station
     */
    public class FuelStationDto
    {
        public string? Id { get; set; }

        public string? License { get; set; }
        public string? OwnerUsername { get; set; }
        public string? StationName { get; set; }
        public string? StationAddress { get; set; }
        public string? StationPhoneNumber { get; set; }
        public string? StationEmail { get; set; }
        public string? StationWebsite { get; set; }

        public string? OpenStatus { get; set; }
        public int? PetrolQueueLength { get; set; }
        public int? DieselQueueLength { get; set; }
        public string? PetrolStatus { get; set; }
        public string? DieselStatus { get; set; }

        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
    }
}

