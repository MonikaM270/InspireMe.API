using InspireMe.API.Data;
using InspireMe.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InspireMe.API.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly ILogger<QuoteService> _logger;
        private readonly AppDbContext _context;

        public QuoteService(ILogger<QuoteService> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        private static readonly Dictionary<string, List<string>> QuotesByMood = new(StringComparer.OrdinalIgnoreCase)
        {
            ["happy"] = new()
            {
                "Happiness is a choice.",
                "Smile, it's a good day!",
                "Joy is contagious—spread it!"
            },
            ["sad"] = new()
            {
                "This too shall pass.",
                "Crying is not a weakness.",
                "Even the darkest night will end and the sun will rise."
            },
            ["motivated"] = new()
            {
                "Push yourself, no one else will.",
                "Discipline beats motivation.",
                "You are stronger than your excuses."
            },
            ["angry"] = new()
            {
                "Take a deep breath. Calm is power.",
                "Anger doesn't solve anything—it builds nothing."
            },
            ["calm"] = new()
            {
                "Silence is a source of great strength.",
                "Peace begins with a smile."
            },
            ["excited"] = new()
            {
                "Let the excitement fuel your journey!",
                "Your energy is infectious—keep going!"
            }
        };

        public async Task<string> GetRandomQuoteAsync()
        {
            _logger.LogInformation("Fetching random quote");

            var allQuotes = await _context.Quotes.ToListAsync();
            return GetRandomFromList(allQuotes.Select(q => q.Text).ToList());
        }


        public async Task<string> GetQuoteByMoodAsync(string mood)
        {
            _logger.LogInformation("Fetching quote for mood: {Mood}", mood);

            var quotes = await _context.Quotes
                .Where(q => q.Mood.ToLower() == mood.ToLower())
                .ToListAsync();

            if (!quotes.Any())
            {
                _logger.LogWarning("No quotes found for mood: {Mood}", mood);
                return $"Sorry, no quotes available for mood: {mood}";
            }

            return GetRandomFromList(quotes.Select(q => q.Text).ToList());
        }


        private static string GetRandomFromList(List<string> quotes)
        {
            var random = new Random();
            int index = random.Next(quotes.Count);
            return quotes[index];
        }

        public async Task AddQuoteAsync(Quote quote)
        {
            _logger.LogInformation("Adding new quote: {Text} ({Mood})", quote.Text, quote.Mood);
            await _context.Quotes.AddAsync(quote);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateQuoteAsync(int id, Quote updatedQuote)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null) return false;

            quote.Text = updatedQuote.Text;
            quote.Mood = updatedQuote.Mood;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteQuoteAsync(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null) return false;

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
