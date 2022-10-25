/**
 * EAD - FuelMe API
 * 
 * @author H.G. Malwatta - IT19240848
 * 
 * @references
 * - https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
 * - https://mongodb.github.io/mongo-csharp-driver/2.18/getting_started/quick_tour/
 * 
 */

using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

/**
 * @author H.G. Malwatta - IT19240848
 * 
 * This controller class is used to manipulate all feedback API related calls
 * 
 */
namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        /**
         * Overloaded constroctor
         * 
         * @param feedbackService
         */
        public FeedbackController(FeedbackService feedbackService) => _feedbackService = feedbackService;

        /**
         * Create new feedback
         * POST: api/Feedback
         * 
         * @param feedback
         * @retun Task<IActionResult>
         */
        [HttpPost]
        public async Task<IActionResult> CreateFeedback(Feedback feedback)
        {
            await _feedbackService.CreateFeedbackAsync(feedback);

            //return created feedback
            return CreatedAtAction(nameof(GetFeedBackById), new {id = feedback.Id }, feedback);
        }

        /**
         * Get feedback by id
         * GET: api/Feedback/{id}
         * 
         * @retun Task<ActionResult<Feedback>>
         */
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Feedback>> GetFeedBackById(string id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);

            if(feedback is null)
            {
                return NotFound();
            }

            return feedback;
        }

        /**
         * Get all feedbacks
         * GET: api/Feedback/{id}
         * 
         * @retun Task<List<Feedback>>
         */
        [HttpGet]
        public async Task<List<Feedback>> GetAllFeedbacks() => await _feedbackService.GetAllFeedbacksAsync();


        /**
         * Update Feedback
         * PUT: api/Feedback/{id}
         * 
         * @param id
         * @param updateFeedback
         * @retun Task<IActionResult>
         */
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateFeedback(string id, Feedback updateFeedback)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);

            if(feedback is null)
            {
                return NotFound();
            }

            updateFeedback.Id = feedback.Id;

            await _feedbackService.UpdateFeedbackAsync(id, updateFeedback);

            return Ok("Successfully Updated!");
        }

        /**
         * Delete Feedback
         * DELETE: api/Feedback/{id}
         * 
         * @param id
         * @retun Task<IActionResult>
         */
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteFeedback(string id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);

            if(feedback is null)
            {
                return NotFound();
            }

            await _feedbackService.DeleteFeedbackAsync(id);

            return Ok("Successfully Deleted!");
        }

        /**
         * Get all station feedback by id
         * GET: api/Feedback/station/{id}
         * 
         * @param id
         * @retun Task<ActionResult<List<Feedback>>>
         */
        [HttpGet("station/{id}")]
        public async Task<ActionResult<List<Feedback>>> GetAllStationFeedbackById(string id)
        {
            var feedback = _feedbackService.GetAllFeedbackByStationId(id);

            if (feedback.Count == 0) 
            {
               return NotFound();
            }

            return feedback;
        }

        /**
         * Delete Feedback by station Id
         * DELETE: api/Feedback/DeleteFeedbacksByStationId/{id}
         * 
         * @param id
         * @retun Task<IActionResult>
         */
        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFeedbacksByStationId(string id)
        {

            var feedback = await _feedbackService.DeleteFeeedbackByStationIdAsync(id);

            if (feedback is null)
            {
                return NoContent();
            }

            return Ok(feedback);
        }

    }
}
