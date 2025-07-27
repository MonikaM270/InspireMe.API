using Microsoft.AspNetCore.Mvc;
using InspireMe.API.Services;
using InspireMe.API.DTOs;
using InspireMe.API.Validators;
using InspireMe.API.Models;

namespace InspireMe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuote([FromQuery] string? mood)
        {
            if (string.IsNullOrWhiteSpace(mood))
            {
                var quote = await _quoteService.GetRandomQuoteAsync();
                return Ok(new QuoteResponseDto { Quote = quote });
            }

            if (!MoodValidator.IsValid(mood))
            {
                return BadRequest(new { error = "Invalid mood. Try happy, sad, angry, calm, excited, or motivated." });
            }

            var moodQuote = await _quoteService.GetQuoteByMoodAsync(mood);
            return Ok(new QuoteResponseDto { Quote = moodQuote });
        }

        [HttpPost]
        public async Task<IActionResult> AddQuote([FromBody] Quote quote)
        {
            if (string.IsNullOrWhiteSpace(quote.Mood) || string.IsNullOrWhiteSpace(quote.Text))
                return BadRequest("Mood and Text are required");

            await _quoteService.AddQuoteAsync(quote);
            return Ok("Quote added sucessfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuote(int id, [FromBody] Quote updatedQuote)
        {
            if (string.IsNullOrWhiteSpace(updatedQuote.Text) || string.IsNullOrWhiteSpace(updatedQuote.Mood))
                return BadRequest("Mood and Text are required");

            var result = await _quoteService.UpdateQuoteAsync(id, updatedQuote);
            if (!result)
            {
                return NotFound("Quote with ID {id} not found.");
            }

            return Ok("Quote updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var result = await _quoteService.DeleteQuoteAsync(id);

            if (!result)
                return NotFound($"Quote with ID {id} not found");

            return Ok("Quote deleted successfully!");
        }
    }
}
