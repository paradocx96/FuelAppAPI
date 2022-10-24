/*
 * EAD - FuelMe APP API
 *
 * Data Transfer Object for Notice
 * 
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
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