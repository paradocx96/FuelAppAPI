using System;

/*
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * 
 * Model for Fuel Statio Log Item
 */

namespace FuelAppAPI.Models
{
    public class FuelStationLogItem
    {
        public string? Id { get; set; }
        public string? StationId { get; set; }
        public string? FuelType { get; set; }
        public string? FuelStatus { get; set; }
        public DateTime? DateTime { get; set; }
    }
}

