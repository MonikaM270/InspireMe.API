using InspireMe.API.Models;

namespace InspireMe.API.Services
{
    public interface IQuoteService
    {
        Task<string> GetRandomQuoteAsync();
        Task<string> GetQuoteByMoodAsync(string mood);
        Task AddQuoteAsync(Quote quote);
        Task<bool> UpdateQuoteAsync(int id, Quote updatedQuote);
        Task<bool> DeleteQuoteAsync(int id);


    }
}
