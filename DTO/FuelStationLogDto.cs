using System;

/*
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * 
 * DTO for Fuel Station Log Item
 * 
 * References:
 * https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0
 * 
 */

namespace FuelAppAPI.DTO
{
    public class FuelStationLogDto
    {
        public string? Id { get; set; }
        public string? StationId { get; set; }
        public string? FuelType { get; set; }
        public string? FuelStatus { get; set; }
        public DateTime? DateTime { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? DayNumber { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
        public int? Second { get; set; }
    }
}

