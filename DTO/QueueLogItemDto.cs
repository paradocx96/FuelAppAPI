using System;
namespace FuelAppAPI.DTO
{
    public class QueueLogItemDto
    {
        public string? Id { get; set; }

        public string? CustomerUsername { get; set; }
        public string? StationId { get; set; }
        public string? StationLicense { get; set; }
        public string? StationName { get; set; }
        public string? Queue { get; set; }
        public string? Action { get; set; }  //join / leave
        public string? RefuelStatus { get; set; } //refuled / not-refueled / not-applicable
    }
}

