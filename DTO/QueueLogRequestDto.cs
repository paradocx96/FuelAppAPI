using System;

/*
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * 
 * DTO for queue log request
 * 
 * References:
 * https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0
 * 
 * 
 */

namespace FuelAppAPI.DTO
{
    public class QueueLogRequestDto
    {
        public string? CustomerUsername { get; set; }
        public string? StationId { get; set; }
        public string? StationLicense { get; set; }
        public string? StationName { get; set; }
        public string? RefuelStatus { get; set; } //refuled / not-refueled / not-applicable
    }
}

