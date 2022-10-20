using FuelAppAPI.DTO;
using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

/*
* IT19180526
* S.A.N.L.D. Chandrasiri
* API Controller for Notice
*/
namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticeController : ControllerBase
    {
        private readonly NoticeService _noticeService;

        public NoticeController(NoticeService noticeService) =>
            _noticeService = noticeService;

        // Create Notice
        [HttpPost]
        public async Task<IActionResult> CreateNotice(NoticeDto noticeDto)
        {
            // Create new Notice object
            Notice notice = new Notice();
            notice.StationId = noticeDto.StationId;
            notice.Title = noticeDto.Title;
            notice.Description = noticeDto.Description;
            notice.Author = noticeDto.Author;
            notice.CreateAt = noticeDto.CreateAt;

            // Calling async function made for create new notice
            await _noticeService.CreateAsync(notice);

            return CreatedAtAction(nameof(GetNoticeById), new { id = notice.Id }, notice);
        }

        // Get All Notices
        [HttpGet]
        public async Task<List<Notice>> GetNotice() =>
            await _noticeService.GetAsync();

        // Get Notice By Id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Notice>> GetNoticeById(string id)
        {
            // Calling async function made for get notice by notice id
            var notice = await _noticeService.GetAsync(id);

            if (notice is null)
            {
                return NotFound();
            }

            return notice;
        }

        // Update Notice
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateNotice(string id, NoticeDto noticeDto)
        {
            // Calling async function made for get notice by notice id
            var noticeCheck = await _noticeService.GetAsync(id);

            // Checking notice availability
            if (noticeCheck is null)
            {
                return NotFound();
            }

            // Create new notice object and assign values for update notice
            Notice updatedNotice = new Notice();
            updatedNotice.Id = id;
            updatedNotice.StationId = noticeDto.StationId;
            updatedNotice.Title = noticeDto.Title;
            updatedNotice.Description = noticeDto.Description;
            updatedNotice.Author = noticeDto.Author;
            updatedNotice.CreateAt = noticeDto.CreateAt;

            // Calling async function made for update notice
            await _noticeService.UpdateAsync(id, updatedNotice);

            return NoContent();
        }

        // Delete Notice
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteNotice(string id)
        {
            // Calling async function made for get notice by notice id
            var notice = await _noticeService.GetAsync(id);

            // Checking notice availability
            if (notice is null)
            {
                return NotFound();
            }

            // Calling async function made for delete notice by notice id
            await _noticeService.RemoveAsync(id);

            return NoContent();
        }

        // Get Notices By Station Id
        [HttpGet("station/{id}")]
        public async Task<ActionResult<List<Notice>>> GetNoticesByStationId(string id)
        {
            // Calling async function made for get notice by station id
            var notices = _noticeService.GetNoticesByStationId(id);

            if (notices.Count == 0)
            {
                return NotFound();
            }

            return notices;
        }

        // Get Notices By Author (username)
        [HttpGet("author/{author}")]
        public async Task<ActionResult<List<Notice>>> GetNoticesByAuthor(string author)
        {
            // Calling async function made for get notice by author (username)
            var notices = _noticeService.GetNoticesByAuthor(author);

            if (notices.Count == 0)
            {
                return NotFound();
            }

            return notices;
        }
    }
}