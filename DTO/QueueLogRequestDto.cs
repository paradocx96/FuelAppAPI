using System;
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

