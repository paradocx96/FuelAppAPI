using FuelAppAPI.Models;
using FuelAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace FuelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService) => _feedbackService = feedbackService;
        
        // Create Feedback
        [HttpPost]
        public async Task<IActionResult> CreateFeedback(Feedback feedback)
        {
            await _feedbackService.CreateFeedbackAsync(feedback);

            return CreatedAtAction(nameof(GetFeedBackById), new {id = feedback.Id }, feedback);
        }

        // Get Feedback By Id
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

        // Get All Feedback
        [HttpGet]
        public async Task<List<Feedback>> GetAllFeedbacks() => await _feedbackService.GetAllFeedbacksAsync();

        // Update Feedback
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

            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteFeedback(string id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);

            if(feedback is null)
            {
                return NotFound();
            }

            await _feedbackService.DeleteFeedbackAsync(id);

            return Ok("Message : Successfully Deleted");
        }


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

    }
}
