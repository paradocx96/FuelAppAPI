/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* Data Transfer Object for Notice
*/

namespace FuelAppAPI.DTO
{
    public class NoticeDto
    {
        public string StationId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string CreateAt { get; set; } = null!;
    }
}