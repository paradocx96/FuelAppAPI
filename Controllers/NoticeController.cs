using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
        public async Task<IActionResult> CreateNotice(Notice notice)
        {
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
            var notice = await _noticeService.GetAsync(id);

            if (notice is null)
            {
                return NotFound();
            }

            return notice;
        }

        // Update Notice
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateNotice(string id, Notice updatedNotice)
        {
            var notice = await _noticeService.GetAsync(id);

            if (notice is null)
            {
                return NotFound();
            }

            updatedNotice.Id = notice.Id;

            await _noticeService.UpdateAsync(id, updatedNotice);

            return NoContent();
        }

        // Delete Notice
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteNotice(string id)
        {
            var notice = await _noticeService.GetAsync(id);

            if (notice is null)
            {
                return NotFound();
            }

            await _noticeService.RemoveAsync(id);

            return NoContent();
        }
    }
}
