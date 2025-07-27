namespace InspireMe.API.Validators
{
    public static class MoodValidator
    {
        private static readonly HashSet<string> ValidMoods = new()
        {
            "happy", "sad", "motivated", "angry", "calm", "excited"
        };

        public static bool IsValid(string mood)
        {
            return !string.IsNullOrWhiteSpace(mood) && ValidMoods.Contains(mood.ToLower());
        }
    }
}
