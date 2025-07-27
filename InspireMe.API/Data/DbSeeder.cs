using InspireMe.API.Models;

namespace InspireMe.API.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Quotes.Any())
            {
                var quotes = new List<Quote>
                {
                    new Quote { Mood = "happy", Text = "Happiness is a choice." },
                    new Quote { Mood = "sad", Text = "This too shall pass." },
                    new Quote { Mood = "motivated", Text = "Push yourself, no one else will." },
                    new Quote { Mood = "angry", Text = "Anger doesn’t solve anything." },
                    new Quote { Mood = "calm", Text = "Silence is a source of great strength." },
                    new Quote { Mood = "excited", Text = "Let the excitement fuel your journey!" },
                };

                context.Quotes.AddRange(quotes);
                context.SaveChanges();
            }
        }
    }
}
