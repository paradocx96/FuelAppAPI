/*
 * EAD - FuelMe APP API
 *
 * @author IT19180526 - S.A.N.L.D. Chandrasiri
 * @version 1.0
 */

using FuelAppAPI.DTO;
using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

/*
* API Controller for Notice
*
* @author IT19180526 - S.A.N.L.D. Chandrasiri
* @version 1.0
*/
namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticeController : ControllerBase
    {
        // Defined Notice Service
        private readonly NoticeService _noticeService;

        // Constructor
        public NoticeController(NoticeService noticeService) =>
            _noticeService = noticeService;

        /**
         * Create Notice
         * POST: api/Notice
         *
         * @return Task<IActionResult>
         * @see #CreateNotice(NoticeDto noticeDto)
         */
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

        /**
         * Get All Notices
         * GET: api/Notice
         *
         * @return Task<List<Notice>>
         * @see #GetNotice()
         */
        [HttpGet]
        public async Task<List<Notice>> GetNotice() =>
            await _noticeService.GetAsync();

        /**
         * Get Notice By Id
         * GET: api/Notice/{id}
         *
         * @return Task<ActionResult<Notice>>
         * @see #GetNoticeById(string id)
         */
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Notice>> GetNoticeById(string id)
        {
            // Calling async function made for get notice by notice id
            var notice = await _noticeService.GetAsync(id);

            // Checking notice availability
            if (notice is null)
            {
                return NotFound();
            }

            return notice;
        }

        /**
         * Update Notice
         * PUT: api/Notice/{id}
         *
         * @return Task<IActionResult>
         * @see #UpdateNotice(string id, NoticeDto noticeDto)
         */
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

        /**
         * Delete Notice
         * DELETE: api/Notice/{id}
         *
         * @return Task<IActionResult>
         * @see #DeleteNotice(string id)
         */
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

        /**
         * Get Notices By Station Id
         * GET: api/Notice/station/{id}
         *
         * @return Task<ActionResult<List<Notice>>>
         * @see #GetNoticesByStationId(string id)
         */
        [HttpGet("station/{id}")]
        public async Task<ActionResult<List<Notice>>> GetNoticesByStationId(string id)
        {
            // Calling async function made for get notice by station id
            var notices = _noticeService.GetNoticesByStationId(id);

            // Checking notice availability
            if (notices.Count == 0)
            {
                return NotFound();
            }

            return notices;
        }

        /**
         * Get Notices By Author (username)
         * GET: api/Notice/author/{id}
         *
         * @return Task<ActionResult<List<Notice>>>
         * @see #GetNoticesByAuthor(string author)
         */
        [HttpGet("author/{author}")]
        public async Task<ActionResult<List<Notice>>> GetNoticesByAuthor(string author)
        {
            // Calling async function made for get notice by author (username)
            var notices = _noticeService.GetNoticesByAuthor(author);

            // Checking notice availability
            if (notices.Count == 0)
            {
                return NotFound();
            }

            return notices;
        }
    }
}